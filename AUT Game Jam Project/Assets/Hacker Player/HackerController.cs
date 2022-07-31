using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CharacterMovement))]
public class HackerController : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField] private Transform shootTransform;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float shootSpeed = 10;
    [SerializeField] private string ignoreShootTag = "enter";
    [SerializeField] private bool shootRounded = false;
    [ShowIf(nameof(shootRounded))] [SerializeField] private float shootDistance = 1.5f;
    [ShowIf(nameof(shootRounded))] [SerializeField] private Vector2 playerCenterOffset;

    [Header("Reload")]
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private bool reloading = false;
    [SerializeField] private TextMeshProUGUI reloadText;
    [SerializeField] private string reloadPishvand = "Reloading ";

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
        reloadText.gameObject.SetActive(false);
        
        if (!mainCam)
            mainCam = Camera.main;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere((Vector2)transform.position + playerCenterOffset, 0.2f);
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
        reloadText.gameObject.SetActive(true);
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            
            if(t < 0.3f)
            {
                reloadText.SetText(reloadPishvand + ".");
            }else if (t > 0.3f && t < 0.6f)
            {
                reloadText.SetText(reloadPishvand + "..");
            }
            else
            {
                reloadText.SetText(reloadPishvand + "...");
            }

            yield return null;
        }
        reloadText.gameObject.SetActive(false);
        inventory.Reload();
        reloading = false;
    }

    public void StopReloading()
    {
        if(reloading == true)
        {
            StopCoroutine(DelayReload(reloadTime));
            reloadText.gameObject.SetActive(false);
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

        if (!IsMouseInFront(mousePos)) return;

        inventory.DecreaseBulletsInMag();
        SpawnBullet(mousePos);

        //play attack anim
        movement.autoAnimation = false;
        animPlayer.PlayAnim(AnimationType.Attack);
    }

    bool IsMouseInFront(Vector2 mousePos)
    {
        //old way
        /*
        if (movement.facingRight)
        {
            if (mousePos.x <= transform.position.x)
            {
                return false;
            }
        }
        else
        {
            if (mousePos.x >= transform.position.x)
            {
                return false;
            }
        }

        return true;
        */

        Vector2 playerRightDirection = GetrightDirection();
        Vector2 mouseDirection = mousePos - (Vector2)transform.position;
        mouseDirection.Normalize();
        float threshold = -0.7f;

        float dot = Vector2.Dot(playerRightDirection, mouseDirection);
        return dot > threshold;
    }

    Vector2 GetrightDirection()
    {
        return movement.facingRight ? Vector2.right : Vector2.left;
    }

    private void SpawnBullet(Vector2 mousePos)
    {
        Rigidbody2D rb;

        if (shootRounded == false)
            rb = Instantiate(bulletPrefab, shootTransform.position, Quaternion.identity);
        else
        {
            Vector2 playerPos = (Vector2)transform.position + playerCenterOffset;
            Vector2 shootDir = (mousePos - playerPos).normalized;
            Vector2 spawnPos = playerPos + shootDir * shootDistance;

            rb = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        }

        //adding the force to the bullet
        Vector2 startPos = shootRounded ? (Vector2)transform.position + playerCenterOffset + (mousePos - (Vector2)transform.position + playerCenterOffset).normalized * shootDistance : (Vector2)shootTransform.position;
        Vector3 relative = mousePos - startPos;
        rb.velocity = relative.normalized * shootSpeed;

        //looking at the target
        Transform bT = rb.transform;
        var angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        bT.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bT.Rotate(Vector3.forward * -90);

        GeneralBullet bullet = rb.GetComponent<GeneralBullet>();
        bullet.ignoreTags.Add(ignoreShootTag);
        bullet.shooter = transform;
    }

    public void Die()
    {
        if (movement)
        {
            movement.SetVelocity(Vector2.zero);
            movement.autoAnimation = false;
            movement.enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
        }

        animPlayer.PlayAnim(AnimationType.Die);

        Invoke(nameof(DelayGameOver), 1.5f);
    }

    private void DelayGameOver()
    {
        GameManager.Instance.OpenGameOver();
    }
}