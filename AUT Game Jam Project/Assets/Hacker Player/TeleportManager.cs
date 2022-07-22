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
    public float slowMotionScale = 0.05f;
    [SerializeField] private Transform mousePosTransform;
    [Space]
    [SerializeField] private float teleportCoolDown = 2;

    [Header("UI")]
    [SerializeField] private Image coolDownImage;

    [Header("Damaging")]
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private int damage = 10;

    [Header("Teleport Effect")]
    public TrailRenderer teleportTrail;

    [Header("Animations")]
    public AnimationPlayer animPlayer;
    public string teleportStartState = "TStart";
    public string teleportFinishState = "TFinish";

    private float remainingTime = 0;

    [HideInInspector] public Vector2 dropPoint;

    HackerController controller;
    CharacterMovement movement;
    bool hasButtonDown = false;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        controller = GetComponent<HackerController>();
        teleportCircle.gameObject.SetActive(false);
        teleportTrail.gameObject.SetActive(false);
        mousePosTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (DialogueManager.ShowingDialogue == true
            ||PauseMenu.IsPaused
            ||GameManager.Instance.GameOver
            ||BossAbilityManager.DesigningBoss)
        {
            return;
        }

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
        movement.autoAnimation = false;
        animPlayer.PlayState(teleportStartState);

        hasButtonDown = true;

        controller.bypass = true;
        mousePosTransform.gameObject.SetActive(true);

        teleportTrail.gameObject.SetActive(true);

        GetComponent<CharacterMovement>().SetVelocity(Vector2.zero);
        teleportCircle.gameObject.SetActive(true);
        teleportCircle.position = transform.position;
        teleportCircle.localScale = Vector3.one * maxTeleportRange / transform.localScale.x;

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

        mousePosTransform.position = dropPoint;
    }

    void Up()
    {
        if (!hasButtonDown)
            return;
        
        animPlayer.PlayState(teleportFinishState);

        hasButtonDown = false;

        Time.timeScale = 1;

        controller.bypass = false;
        teleportCircle.gameObject.SetActive(false);
        mousePosTransform.gameObject.SetActive(false);

        Invoke(nameof(TurnOffTrail), teleportTrail.time);

        //damaging anything along the way

        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, dropPoint, damageMask);
        foreach (var hit in hits)
        {
            if (hit.collider == GetComponent<Collider2D>())
                break;

            if(hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage, transform);
            }
        }

        controller.transform.position = dropPoint;
        remainingTime = teleportCoolDown;
        coolDownImage.gameObject.SetActive(true);
        coolDownImage.fillAmount = 1;
    }

    void TurnOffTrail()
    {
        teleportTrail.gameObject.SetActive(false);
    }
}
