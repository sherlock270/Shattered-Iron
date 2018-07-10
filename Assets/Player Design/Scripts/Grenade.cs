using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public float radius = 50.0F;
    public float power = 25000.0F;
    public GameObject explosionPrefab;

    private void Start() {
      Invoke("Explode", 2);
    }

    void Explode() {
        Vector3 explosionPos = gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Debug.Log("collider hit!", hit);
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Debug.Log("rigidbody!", rb);

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 30.0F);
                // hit.GetComponent< EnemyController >().currentHealth -= 30;
            }
        }
        GameObject explosionGO = Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(explosionGO, 6f);
    }
}
