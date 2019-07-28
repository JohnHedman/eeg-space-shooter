using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy02_AI : MonoBehaviour {

	public float shootDelayLow = 1.0f;
	public float shootDelayHigh = 3.0f;
	public float shipMoveSpeed = 3.0f;
	public Vector3 bulletOffset = new Vector3(0, -1.0f, 0);
	public GameObject bulletPrefab;
	public GameObject target;
	public float minX = -18;
	public float maxX = 18;

	private float cooldownTimer;
	private float rotationTarget;
	private float timeSinceShoot;
	private bool movePositive = true;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("PlayerShip");
		cooldownTimer = Random.Range(shootDelayLow, shootDelayHigh);
		Random.seed = (int)System.DateTime.Now.Ticks;
		int temp1 = Random.Range(1, 2);
		if(temp1 == 1)
		{
			movePositive = true;
		}
		else
		{
			movePositive = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceShoot += Time.deltaTime;

		Vector3 pos = transform.position;

		if(movePositive)
		{
			pos.x += (shipMoveSpeed * Time.deltaTime);
		}
		else
		{
			pos.x -= (shipMoveSpeed * Time.deltaTime);
		}

		if(pos.x <= minX || pos.x >= maxX)
		{
			movePositive = !movePositive;
		}

		transform.position = pos;

		if(timeSinceShoot > 0.5)
		{
			transform.rotation = Quaternion.Euler(0,0,0);
		}

		if(target == null){
			target = GameObject.FindGameObjectWithTag("PlayerShip");
		}
		else{
			cooldownTimer -= Time.deltaTime;

			if(cooldownTimer <= 0){
				cooldownTimer = Random.Range(shootDelayLow, shootDelayHigh);
				Vector3 playerPosition = target.transform.position;

				rotationTarget = findTargetRotation(this.transform.position, playerPosition);
				Quaternion shipRotation = this.transform.rotation;
				rotateShip(rotationTarget, shipRotation);
				Vector3 offset = transform.rotation * bulletOffset;

				Quaternion bulletRotation = this.transform.rotation;
				bulletRotation = Quaternion.Euler(0, 0, rotationTarget + 180);
				GameObject enemyBullet = (GameObject)Instantiate(bulletPrefab, transform.position + offset, bulletRotation);

				timeSinceShoot = 0;
			}
		}
	}

	public void DisableEnemyManager()
	{
		this.GetComponent<enemy02Manager>().enabled = false;
	}

	private float findTargetRotation(Vector3 shipPosition, Vector3 targetPosition)
	{
		float diffY = shipPosition.y - targetPosition.y;
		float diffX = shipPosition.x - targetPosition.x;
		// Convert radians to degrees
		return (-180 * Mathf.Atan(diffX/diffY)) / Mathf.PI;
	}

	private void rotateShip(float rotationTarget, Quaternion shipRotation)
	{
		float diffRotation = rotationTarget - shipRotation.eulerAngles.z;
		shipRotation = Quaternion.Euler(0, 0, shipRotation.eulerAngles.z + diffRotation);
		this.transform.rotation = shipRotation;
	}



}
