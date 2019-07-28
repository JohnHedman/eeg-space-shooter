using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {

	public int startingHealth = 25;
	public int startShield = 0;

	
	private int currentShield;
	private int currentHealth;
	private Animator animationController;
	private GameObject sceneManager;

	// Triggered when objects hit this object
	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.layer == 8)
		{
			// If the enemy hits the player, then kill the enemy
			currentHealth = 0;
			enemyDeath("EnemyHit");
			return;
		}
		else if(other.gameObject.layer == 10)
		{
			// If the enemy gets hit by the player's bullet, then decrease health.
			currentHealth -= 25;
		}

		// If the enemy has health less than or equal to zero, then trigger the enemyDeath function.
		if(currentHealth <= 0)
		{
			enemyDeath(this.tag);
		}
	}

	private void enemyDeath(string tag)
	{
		updatePlayerScore(tag);
		animationController.SetBool("Death", true);
		Destroy(gameObject, .45f);
	}

	private void updatePlayerScore(string tag)
	{
		switch (tag)
		{
			case "EnemyShip1" :
				sceneManager.GetComponent<scoreControl>().AddScore(50);
				break;

			case "EnemyShip2" :
				sceneManager.GetComponent<scoreControl>().AddScore(75);
				break;

			case "EnemyShip3" :
				sceneManager.GetComponent<scoreControl>().AddScore(200);
				break;

			case "EnemyShip4" :
				sceneManager.GetComponent<scoreControl>().AddScore(500);
				break; 

			case "EnemyHit" :
				break;
			
			default:
				break;
		}
	}



	// Use this for initialization
	void Start () {
		sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
		currentHealth = startingHealth;
		currentShield = startShield;
		animationController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
