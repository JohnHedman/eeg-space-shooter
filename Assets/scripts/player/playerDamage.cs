using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamage : MonoBehaviour {

	public int startingHealth = 100;
	public int startShield = 0;
	private int currentShield = 0;
	private int currentHealth = 100;

	private bool isDead;
	private Animator animationController;


	void OnTriggerEnter2D() {
		if(currentShield <= 0)
		{
			currentHealth -= 25;
		}
		else{
			return;
		}
		if(currentHealth <= 0)
		{
			currentHealth = 0;
			playerDeath();
		}
	}

	// Use this for initialization
	void Start () {
		animationController = GetComponent<Animator>();
		currentHealth = startingHealth;
		currentShield = startShield;
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {

		
	}


	private void playerDeath()
	{
		GetComponent<playerMovement>().enabled = false;
		GetComponent<playerNormalShooting>().enabled = false;
		GetComponent<PolygonCollider2D>().enabled = false;
		GameObject.FindGameObjectWithTag("SceneManager").GetComponent<scoreControl>().LossScore(200);
		GameObject.FindGameObjectWithTag("SceneManager").GetComponent<playerRespawn>().respawnPlayer(3);
		animationController.SetBool("Death", true);
		Destroy(gameObject, .45f);		
	}

	public int getCurrentHeath()
	{
		return this.currentHealth;
	}

	public int getCurrentShield()
	{
		return this.currentShield;
	}

	public void setCurrentShield(int value)
	{
		if(value > 100){
			currentShield = 100;
		}
		else if(value < 0){
			currentShield = 0;
		}
		else{
			this.currentShield = value;
		}
	}

	public void setCurrentHealth(int value)
	{
		if(value > 100){
			this.currentHealth = 100;
		}
		else if(value < 0){
			this.currentHealth = 0;
			playerDeath();
		}
		else{
			this.currentHealth = value;
		}
	}
}
