using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HackerController))]
public class TeleportManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxTeleportRange = 3;
    [SerializeField] private Transform teleportCircle;
    [SerializeField] private float slowMotionScale = 0.05f;

    [Space]
    [SerializeField] private float teleportCoolDown = 2;

    [Header("UI")]
    [SerializeField] private Image coolDownImage;

    [Header("Damaging")]
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private int damage = 10;

    private float remainingTime = 0;

    [HideInInspector] public Vector2 dropPoint;

    HackerController controller;

    private void Awake()
    {
        controller = GetComponent<HackerController>();
        teleportCircle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            coolDownImage.fillAmount = remainingTime / teleportCoolDown;
            return;
        }

        coolDownImage.gameObject.SetActive(false);

        if (Input.GetMouseButtonDown(1))
        {
            Down();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Up();
        }

        if (Input.GetMouseButton(1))
        {
            Hold();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxTeleportRange);
    }

    void Down()
    {
        controller.bypass = true;
        GetComponent<CharacterMovement>().SetVelocity(Vector2.zero);
        teleportCircle.gameObject.SetActive(true);
        teleportCircle.position = transform.position;
        teleportCircle.localScale = Vector3.one * maxTeleportRange / transform.localScale.x;
        Time.timeScale = slowMotionScale;
    }

    void Hold()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 relative = mousePos - (Vector2)transform.position;
        if (relative.sqrMagnitude > maxTeleportRange * maxTeleportRange)
        {
            dropPoint = (Vector2)transform.position + relative.normalized * maxTeleportRange;
        }
        else
        {
            dropPoint = mousePos;
        }
    }

    void Up()
    {
        controller.bypass = false;
        teleportCircle.gameObject.SetActive(false);

        //damaging anything along the way

        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, dropPoint, damageMask);
        foreach (var hit in hits)
        {
            if (hit.collider == GetComponent<Collider2D>())
                break;

            if(hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }
        }

        controller.transform.position = dropPoint;
        Time.timeScale = 1;
        remainingTime = teleportCoolDown;
        coolDownImage.gameObject.SetActive(true);
        coolDownImage.fillAmount = 1;
    }
}
