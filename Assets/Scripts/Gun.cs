using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//reqired conponents


public class Gun : MonoBehaviour
{

    public float damage = 15f;
    public float range = 150f;
    public float fireRate = 15f;
    public float impactForce = 50f;

    public int maxAmmo = 30;
    public float reloadTime = 1f;
    
    public bool isAuto = true;

    public Camera playerCamera = null;

    public Transform muzzle;

    public GameObject muzzleFlash;
    public GameObject impactEffect;

    public Text ammoCounter;

    public Animator gunAnimator;

    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip fireSound;
    //private variables
    private float nextTimeToFire = 0f;

    private int currentAmmo;

    private bool isReloading = false;
    private void Start()
    {
        currentAmmo = maxAmmo;  
    }

    private void OnEnable()
    {
        isReloading = false;
        gunAnimator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) { return; }

        ammoCounter.text = currentAmmo.ToString();

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && isAuto && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }
        if (Input.GetMouseButtonDown(0) && !isAuto && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);

        audioSource.clip = fireSound;
        audioSource.Play();

        RaycastHit hit;

        currentAmmo--;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Health health = hit.transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }

        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }

    IEnumerator Reload()
    {
        isReloading = true;
        gunAnimator.SetBool("Reloading", true);

        Debug.Log("Reloading..");

        yield return new WaitForSeconds(reloadTime);

        audioSource.clip = reloadSound;
        audioSource.Play();

        currentAmmo = maxAmmo;

        isReloading = false;
        gunAnimator.SetBool("Reloading", false);

    }
}
