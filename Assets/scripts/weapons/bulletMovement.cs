using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour {

	public float bulletSpeed = 2.0f;
	public float bulletBoundary = 20.0f;



	private Animator animationController;

	// Use this for initialization
	void Start () {
		animationController = GetComponent<Animator>();
		InvokeRepeating("CheckOutOfBounds", 0.5f, 0.5f);
	}

	void OnTriggerEnter2D() {
		GetComponent<bulletMovement>().enabled = false;
		animationController.SetBool("Hit", true);
		Destroy(gameObject, .2f);
	}
	
	// Update is called once per frame
	void Update () {
		
		// Basic Bullet Movement
		Vector3 bulletPos = transform.position;

		Vector3 bulletVelocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);

		bulletPos = bulletPos + (transform.rotation * bulletVelocity);
		// Set the position to the calculated value.
		transform.position = bulletPos;
	}

	void CheckOutOfBounds()
	{
		Vector3 bulletPos = transform.position;

		// Delete bullets once they are off the screne
		if(bulletPos.y > bulletBoundary)
		{
			Destroy(gameObject);
		}
		if(bulletPos.y < -bulletBoundary)
		{
			Destroy(gameObject);
		}
		if(bulletPos.x > bulletBoundary)
		{
			Destroy(gameObject);
		}
		if(bulletPos.x < -bulletBoundary)
		{
			Destroy(gameObject);
		}
	}
}
