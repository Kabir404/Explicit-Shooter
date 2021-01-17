using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//reqired conponents


public class Gun : MonoBehaviour
{
    [Header("Gun Properties")]
    public float damage = 15f;
    public float range = 150f;
    public float fireRate = 15f;
    public float impactForce = 50f;
    public bool isAuto = true;

    [Header("Gun Ammo Info And Reloading")]
    public int loadedAmmo = 30;
    public int hasAmmoInInventory = 90;
    public int currentAmmo;
    public float reloadTime = 1f;
    

    [Header("Needed Items")]
    public Camera playerCamera = null;
    public Transform playerCameraLoc = null;

    public Transform muzzle;

    public GameObject muzzleFlash;
    public GameObject impactEffect;

    public TMP_Text ammoCounter;
    public TMP_Text magazineCounter;

    public Animator gunAnimator;

    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip fireSound;


    //private variables
    public float nextTimeToFire = 0f;



    public bool isReloading = false;


    private void Start()
    {
        //sets the ammo
        currentAmmo = loadedAmmo;
    }

    private void OnEnable()
    {
        //disables the reloading when switched
        isReloading = false;
        gunAnimator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        //dont do anything when reloading
        if (isReloading) { return; }

        //update the ui
        ammoCounter.text = currentAmmo.ToString();
        magazineCounter.text = hasAmmoInInventory.ToString();

        //checks for the "R" key or the ammo is empty or not
        if (currentAmmo <= 0) { StartCoroutine(Reload()); return; }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            StartCoroutine(Reload()); 
            return;
        }

        //fires if user presses the mouse button no 0
        //Firing for Auto
        if (Input.GetMouseButton(0) && isAuto && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }
        //Firing for Non Auto
        if (Input.GetMouseButtonDown(0) && !isAuto && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            Fire();
        }
    }

    //The firing logic
    private void Fire()
    {
        Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);

        audioSource.clip = fireSound;
        audioSource.Play();

        RaycastHit hit;

        currentAmmo--;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            if (hit.collider.GetComponent<Collider>().GetType() == typeof(SphereCollider)) { return; }
            if (hit.collider.GetComponent<Collider>().GetType() != typeof(SphereCollider))
            {
                Debug.Log(hit.transform.name);
                Health health = hit.transform.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            } else
            {
                return;
            }
        }

        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }

        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
    //Reloading Logic
    IEnumerator Reload()
    {
        Debug.Log("attempting to reload");
        if (hasAmmoInInventory != 0)
        {
            //notifies that the gun is reloading and play the reload animation
            isReloading = true;
            gunAnimator.SetBool("Reloading", true);

            Debug.Log("Reloading..");
            //play the reloading sound
            audioSource.clip = reloadSound;
            audioSource.Play();
            //wait for it to reload
            yield return new WaitForSeconds(reloadTime);


            currentAmmo += hasAmmoInInventory;
            if (currentAmmo > loadedAmmo)
            {
                Debug.Log("Reload 1 Is Executed");
                hasAmmoInInventory = currentAmmo - loadedAmmo;
                currentAmmo = loadedAmmo;
            }
            else 
            {
                Debug.Log("Reload 2 Is Executed");
                currentAmmo += hasAmmoInInventory;
                hasAmmoInInventory = 0;
            }

            isReloading = false;
            gunAnimator.SetBool("Reloading", false);
            Debug.Log("Gun Reloaded");
        }

    }
}
