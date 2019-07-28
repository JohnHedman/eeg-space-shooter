using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNormalShooting : MonoBehaviour {

	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
	public GameObject playerBullet;
    public float shootingRate = 0.1f;
	public float shootingSensitivity = 0.5f;

	private float cooldownTimer = 0;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;

		// If they are holding down fire and it has been the required time since last shooting
		if(Input.GetAxis("Fire1") > 0.5 && cooldownTimer <= 0){

			cooldownTimer = shootingRate;

			Vector3 offset = transform.rotation * bulletOffset;

			GameObject bulletStart = Instantiate(playerBullet, transform.position + offset, transform.rotation);
		}
	}
}
