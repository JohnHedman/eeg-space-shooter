using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy04_AI : MonoBehaviour
{

    public float shootDelayLow = 1.0f;
	public float shootDelayHigh = 3.0f;
	public float shipMoveSpeed = 3.0f;
    public Vector3 bullet1Offset = new Vector3(0, -0.5f, 0);
    public Vector3 bullet2Offset = new Vector3(0, -0.5f, 0);
    public Vector3 bullet3Offset = new Vector3(0, -0.5f, 0);
	public Vector3 bullet1AngleOffset = new Vector3(0, 0, -10);
    public Vector3 bullet2AngleOffset = new Vector3(0, 0, 0);
    public Vector3 bullet3AngleOffset = new Vector3(0, 0, 10);
	public GameObject bulletPrefab;
	public GameObject target;
	private float cooldownTimer;
	private float rotationTarget;
	private float timeSinceShoot;
	private bool movePositive;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerShip");
		cooldownTimer = Random.Range(shootDelayLow, shootDelayHigh);
		movePositive = true;
    }

    // Update is called once per frame
    void Update()
    {
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

		if(pos.x > 18 || pos.x < -18)
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

				Quaternion bullet1Quaternion = Quaternion.Euler(0, 0, rotationTarget + bullet1AngleOffset.z + 180);
                Quaternion bullet2Quaternion = Quaternion.Euler(0, 0, rotationTarget + bullet2AngleOffset.z + 180);
                Quaternion bullet3Quaternion = Quaternion.Euler(0, 0, rotationTarget + bullet3AngleOffset.z + 180);

                Vector3 offset1 = bullet1Quaternion * bullet1Offset;
                Vector3 offset2 = bullet2Quaternion * bullet2Offset;
                Vector3 offset3 = bullet3Quaternion * bullet3Offset;

				GameObject bullet1Start = Instantiate(bulletPrefab, transform.position + offset1, bullet1Quaternion);
                GameObject bullet2Start = Instantiate(bulletPrefab, transform.position + offset2, bullet2Quaternion);
                GameObject bullet3Start = Instantiate(bulletPrefab, transform.position + offset3, bullet3Quaternion);

				timeSinceShoot = 0;
			}
		}
    }

	public void DisableEnemyManager()
	{
		this.GetComponent<enemy04Manager>().enabled = false;
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
