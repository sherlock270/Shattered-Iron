using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour {
	public float radius = 50.0F;
  public float power = 25000.0F;
	public GameObject explosionPrefab;

	void OnCollisionEnter(Collision collision)
	{
		Vector3 explosionPos = collision.transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders)
		{
				Debug.Log("collider hit!", hit);
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				Debug.Log("rigidbody!", rb);

				if (rb != null) {
						rb.AddExplosionForce(power, explosionPos, radius, 30.0F);
				}
		}
		Instantiate(explosionPrefab, collision.transform.position, collision.transform.rotation);
		Destroy(gameObject);
	}

}
