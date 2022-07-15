using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterMovement))]
public class HackerController : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField] private Transform shootTransform;
    [SerializeField] private new Camera camera;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float shootSpeed = 10;

    [Header("Reload")]
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private bool reloading = false;
    [SerializeField] private Slider reloadSlider;

    private CharacterMovement movement;
    private HackerInventory inventory;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<HackerInventory>();
        reloadSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        float h = Input.GetAxisRaw(HORIZONTAL);
        float v = Input.GetAxisRaw(VERTICAL);
        Vector2 input = new Vector2(h, v);
        movement.SetVelocity(input);

        movement.HandleFacingDirection();

        if (Input.GetMouseButtonDown(0))
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
        reloadSlider.gameObject.SetActive(true);
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            reloadSlider.value = t;
            yield return null;
        }
        reloadSlider.gameObject.SetActive(false);
        inventory.Reload();
        reloading = false;
    }

    public void StopReloading()
    {
        if(reloading == true)
        {
            StopCoroutine(DelayReload(reloadTime));
            reloadSlider.gameObject.SetActive(false);
            reloading = false;
        }
    }

    private void Shoot()
    {
        if(inventory.CanShoot() == false || reloading == true)
        {
            return;
        }

        inventory.DecreaseBulletsInMag();
        SpawnBullet();

        print("Shoot");
    }

    private void SpawnBullet()
    {
        //getting mouse pos in the world
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        
        //instantiating
        Rigidbody2D rb = Instantiate(bulletPrefab, shootTransform.position, Quaternion.identity);
        
        //adding the force to the bullet
        Vector3 relative = mousePos - shootTransform.position;
        rb.velocity = relative.normalized * shootSpeed;

        //looking at the target
        Transform bT = rb.transform;
        var angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        bT.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bT.Rotate(Vector3.forward * -90);
    }
}