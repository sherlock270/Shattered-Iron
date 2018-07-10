using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject enemiesPrefab;
	public GameObject[] respawns;
	public GameObject[] spawnPoints;

	[SerializeField]
	private GameObject enemies;
	public static int enemyCount = 0;
	public int maxEnemies = 7;
	private float	nextTimeToSpawn = 0f;
	private float spawnRate = 1f;
	private int randomNumber;

	// Use this for initialization
	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		respawns = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject spawnPoint in spawnPoints)
		{
				Instantiate(enemiesPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
		}
		Spawn();

	}

	void Update() {
		respawns = GameObject.FindGameObjectsWithTag("Enemy");
		// Debug.Log("Update: there are " + respawns.Length + " enemies");
		if (respawns.Length <= maxEnemies) {
			if (Time.time >= nextTimeToSpawn) {
					nextTimeToSpawn = Time.time + 5f / spawnRate;
					Spawn();
			}
		}
	}

	private void Spawn() {
		respawns = GameObject.FindGameObjectsWithTag("Enemy");
		randomNumber = Random.Range(0, spawnPoints.Length);
		// Debug.Log("there are " + respawns.Length + " enemies");
		if(respawns.Length < maxEnemies) {
				// Debug.Log(respawns.Length);
				// yield return new WaitForSeconds(1);
				Instantiate(enemies, spawnPoints[randomNumber].transform.position, spawnPoints[randomNumber].transform.rotation);

				Spawn();
		}
	}
}
