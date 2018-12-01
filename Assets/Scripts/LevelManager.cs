using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckPoint;
    private PlayerController player;

    public GameObject deathParticle;
    public GameObject respawnParticle;
    public float respawnDelay;

    public int pointPenaltyOnDeath;

    private CameraController camera;

    private float gravityStore;

    public HealthManager healthManager;


    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        camera = FindObjectOfType<CameraController>();
        healthManager = FindObjectOfType<HealthManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        camera.isFollowing = false;
        gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;
        //player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ScoreManager.AddPoints(-pointPenaltyOnDeath);
        Debug.Log("Player Respawn");
        yield return new WaitForSeconds(respawnDelay);
        player.GetComponent<Rigidbody2D>().gravityScale = gravityStore;
        player.transform.position = currentCheckPoint.transform.position;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        healthManager.FullHealth();
        healthManager.isDead = false;
        camera.isFollowing = true;
        Instantiate(respawnParticle, currentCheckPoint.transform.position, currentCheckPoint.transform.rotation);
    }
}

