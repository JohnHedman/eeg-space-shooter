using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy01_AI : MonoBehaviour {
	public float shootDelayLow = 1.0f;
	public float shootDelayHigh = 3.5f;
	public Vector3 bulletOffset = new Vector3(0, -1.0f, 0);
	public Quaternion bulletRotation = new Quaternion(0, 0, 180, 0);
	public GameObject bulletPrefab;
	


	// This enemy fires randomly shootDelayLow to ShootDelayHigh seconds.
	private float cooldownTimer;

	// Use this for initialization
	void Start () {
		cooldownTimer = Random.Range(shootDelayLow, shootDelayHigh);
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;

		

		if(cooldownTimer <= 0){
			cooldownTimer = Random.Range(shootDelayLow, shootDelayHigh);

			Vector3 offset = transform.rotation * bulletOffset;


			GameObject enemyBullet = (GameObject)Instantiate(bulletPrefab, transform.position + offset, bulletRotation);
		}
	}

	public void DisableEnemyManager()
	{
		this.GetComponent<enemy01Manager>().enabled = false;
	}
}
