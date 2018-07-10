using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private AudioSource source;
    public AudioClip machineGun;
    public AudioClip grenadeLauncher;

    public Camera playerCam;
    public GameObject missilePrefab;
    public Transform missileSpawn;
    public GameObject missile2Prefab;
    public Transform missile2Spawn;
    public float missileSpeed = 40f;
    private float nextTimeToFireMissile = 0f;
    public float missileFireRate = 1f;
    bool reloadingMissile = false;

    public GameObject grenadePrefab;
    public Transform grenadeSpawn;
    public float grenadeSpeed = 20f;
    private float nextTimeToFireGrenade = 0f;
    public float grenadeFireRate = 1f;
    public int grenadeAmmo = 5;
    public int missileAmmo = 2;
    bool reloading = false;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform shotOrigin;
    public float range = 100f;
    public float impactForce = 30f;
    private float nextTimeToFireBullet = 0f;
    public float gunFireRate = 5f;

    public float startingHealth = 300f;
    public float currentHealth;


    private void Start() {
        currentHealth = startingHealth;
        Cursor.visible = false;
    }

    void Awake () {
		source = GetComponent<AudioSource>();
	}

    
	

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextTimeToFireMissile) {
            nextTimeToFireMissile = Time.time + 1f / missileFireRate;
            if (missileAmmo > 0) {
            Launch();
            missileAmmo--;
            }
            if (reloadingMissile == false) {
                reloadingMissile = true;
                Invoke("ReloadMissile", 2);  
            }
        }

        if (Input.GetKey(KeyCode.E) && Time.time >= nextTimeToFireGrenade) {
            nextTimeToFireGrenade = Time.time + 1f / grenadeFireRate;
            if (grenadeAmmo > 0) {
            GrenadeLaunch();
            grenadeAmmo--;
            }
            if (reloading == false) {
                reloading = true;
                Invoke("Reload", 2);  
            }
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFireBullet) {
            nextTimeToFireBullet = Time.time + 1f / gunFireRate;
            Fire();
        }

		if (Input.GetKeyDown("escape")) {
            Screen.lockCursor = false;
        }
        if (Input.GetMouseButtonDown(0)) {
            Screen.lockCursor = true;
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Missile(Clone)") {
            currentHealth -= 50;
      }
    }

    void Reload() {
        grenadeAmmo = 5;
        reloading = false;
    }

    void ReloadMissile() {
        missileAmmo = 2;
        reloadingMissile = false;
    }


    void Launch()
    {
        // Create the missile from the missile Prefab
        var missile = (GameObject)Instantiate(
            missilePrefab,
            missileSpawn.position,
            missileSpawn.rotation);

        // Add velocity to the missile
        missile.GetComponent<Rigidbody>().velocity = missile.transform.forward * missileSpeed;

        // Destroy the missile after 4 seconds
        Destroy(missile, 4.0f);
        // Create the missile from the missile Prefab
        var missile2 = (GameObject)Instantiate(
            missile2Prefab,
            missile2Spawn.position,
            missile2Spawn.rotation);

        // Add velocity to the missile2
        missile2.GetComponent<Rigidbody>().velocity = missile2.transform.forward * missileSpeed;

        // Destroy the missile after 4 seconds
        Destroy(missile, 4.0f);
    }
    void GrenadeLaunch()
    {
        source.PlayOneShot(grenadeLauncher);
        // Create the missile from the missile Prefab
        var grenade = (GameObject)Instantiate(
            grenadePrefab,
            grenadeSpawn.position,
            grenadeSpawn.rotation);

        // Add velocity to the grenade
        grenade.GetComponent<Rigidbody>().velocity = grenade.transform.forward * grenadeSpeed;

        // Destroy the grenade after 5 seconds
        Destroy(grenade, 2.1f);
    }

    void Fire() {
        source.PlayOneShot(machineGun);
        // play particle effect
        muzzleFlash.Stop();
        muzzleFlash.Play();
        // create variable to store raycast info
        RaycastHit hit;
        // if a raycast hits something
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range )) {
            // log what it hit
            Debug.Log(hit.transform.name);
            if (hit.transform.name == "Vehicle") {
                hit.transform.gameObject.gameObject.GetComponent< EnemyController >().currentHealth -= 10;

            }

            //if it has a rigidbody component attached
            if (hit.rigidbody != null) {
                // add some force to the rigidbody on its normal in the direction of the shot
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            // instantiate a particle effect
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // destroy the particle effect
            Destroy(impactGO, 1f);
        }
    }
}
