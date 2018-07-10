using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyController : MonoBehaviour {

    public GameObject[] waypoints;
    public GameObject missiles;
		private GameObject player;
		public int startingHealth = 100;
		public int currentHealth;
    private int waypointIndex;
    private NavMeshAgent agent;
    public float chaseDist = 40.0f;
    public float fireDist = 30.0f;

    public GameObject misslePrefab;
    public Transform missleSpawn;
    public float missleSpeed = 40f;
    private float nextTimeToFireMissle = 0f;
    public float missleFireRate = 1f;
    public GameObject explosionPrefab;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
				currentHealth = startingHealth;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointIndex = Random.Range(0, waypoints.Length);
        player = GameObject.FindWithTag("Player");
        GotoNextPoint();
    }


    void GotoNextPoint() {
        waypointIndex = Random.Range(0, waypoints.Length);
        agent.destination = waypoints[waypointIndex].transform.position;
    }


    void Update () {
			if(currentHealth <= 0){
				carDeath();
			} else {
        float playerDist = Vector3.Distance(transform.position, player.transform.position);
  			if(playerDist > chaseDist){
          if (!agent.pathPending && agent.remainingDistance < 1.0f)
              GotoNextPoint();
  			} else {
    				agent.destination = player.transform.position;
            if (Time.time >= nextTimeToFireMissle && playerDist < fireDist) {
                nextTimeToFireMissle = Time.time + 1f / missleFireRate;
                Launch();
            }
  			 }
      }
		}

		void carDeath() {
            GameObject explosionGO = Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
			Destroy(transform.parent.gameObject);
            Destroy(explosionGO, 5f);
		}

    void OnCollisionEnter(Collision col) {
      if (col.gameObject.name == "Missile(Clone)") {
        currentHealth -= 50;
      }
      if (col.gameObject.name == "Grenade(Clone)") {
        currentHealth -= 30;
      }
    }

    void Launch()
    {
        // Create the missle from the missle Prefab
        var missle = (GameObject)Instantiate(
            misslePrefab,
            missleSpawn.position,
            missleSpawn.rotation);

        // Add velocity to the missle
        missle.GetComponent<Rigidbody>().velocity = missle.transform.forward * missleSpeed;

        // Destroy the missle after 2 seconds
        Destroy(missle, 2.0f);
    }
}
