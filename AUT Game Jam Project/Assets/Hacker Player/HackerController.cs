using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterMovement))]
public class HackerController : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField] private Transform shootTransform;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float shootSpeed = 10;
    [SerializeField] private string ignoreShootTag = "enter";

    [Header("Reload")]
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private bool reloading = false;
    [SerializeField] private Image reloadImage;

    [Space]

    public AnimationPlayer animPlayer;
    
    [HideInInspector] public bool bypass = false;

    private Health health;
    private CharacterMovement movement;
    private HackerInventory inventory;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<HackerInventory>();
        health = GetComponent<Health>();
        reloadImage.gameObject.SetActive(false);
        
        if (!mainCam)
            mainCam = Camera.main;
    }

    private void Update()
    {
        if(health.isDead)
        {
            if (movement)
                movement.SetVelocity(Vector2.zero);
            return;
        }

        if (bypass || movement == null)
        {
            return;
        }
        if(DialogueManager.ShowingDialogue == true || PauseMenu.IsPaused
            || GameManager.Instance.GameOver
            || BossAbilityManager.DesigningBoss)
        {
            movement.SetVelocity(Vector2.zero);
            return;
        }

        float h = Input.GetAxisRaw(HORIZONTAL);
        float v = Input.GetAxisRaw(VERTICAL);
        Vector2 input = new Vector2(h, v);
        movement.SetVelocity(input);

        movement.HandleFacingDirection();

        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Reload()
    {
        if(inventory.CanReload() == false || reloading == true)
        {
            return;
        }

        StartCoroutine(DelayReload(reloadTime));
    }

    private IEnumerator DelayReload(float duration)
    {
        reloading = true;
        float t = 0;
        reloadImage.gameObject.SetActive(true);
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            reloadImage.fillAmount = 1 - t;
            yield return null;
        }
        reloadImage.gameObject.SetActive(false);
        inventory.Reload();
        reloading = false;
    }

    public void StopReloading()
    {
        if(reloading == true)
        {
            StopCoroutine(DelayReload(reloadTime));
            reloadImage.gameObject.SetActive(false);
            reloading = false;
        }
    }

    private void Shoot()
    {
        if(inventory.CanShoot() == false || reloading == true)
        {
            return;
        }

        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (movement.facingRight)
        {
            if (mousePos.x <= transform.position.x)
            {
                return;
            }
        }
        else
        {
            if (mousePos.x >= transform.position.x)
            {
                return;
            }
        }

        inventory.DecreaseBulletsInMag();
        SpawnBullet(mousePos);
    }

    private void SpawnBullet(Vector2 pos)
    {
        //instantiating
        Rigidbody2D rb = Instantiate(bulletPrefab, shootTransform.position, Quaternion.identity);
        
        //adding the force to the bullet
        Vector3 relative = pos - (Vector2)shootTransform.position;
        rb.velocity = relative.normalized * shootSpeed;

        //looking at the target
        Transform bT = rb.transform;
        var angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        bT.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bT.Rotate(Vector3.forward * -90);

        rb.GetComponent<GeneralBullet>().ignoreTags.Add(ignoreShootTag);
    }

    public void Die()
    {
        if (movement)
            movement.enabled = false;
        animPlayer.PlayAnim(AnimationType.Die);

        Invoke(nameof(DelayGameOver), 1.5f);
    }

    private void DelayGameOver()
    {
        GameManager.Instance.OpenGameOver();
    }
}