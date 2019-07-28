using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrippleShooting : MonoBehaviour
{

    public Vector3 bullet1Offset = new Vector3(0, 0.5f, 0);
    public Vector3 bullet2Offset = new Vector3(0, 0.5f, 0);
    public Vector3 bullet3Offset = new Vector3(0, 0.5f, 0);
    public Vector3 bullet1AngleOffset = new Vector3(0, 0, -10);
    public Vector3 bullet2AngleOffset = new Vector3(0, 0, 0);
    public Vector3 bullet3AngleOffset = new Vector3(0, 0, 10);
	public GameObject playerBullet;
    public float shootingRate = 0.5f;
	public float shootingSensitivity = 0.5f;


	private float cooldownTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if( Input.GetAxis("Fire1") > 0.5 && cooldownTimer <= 0){
			cooldownTimer = shootingRate;

            Quaternion bullet1Quaternion = Quaternion.Euler(transform.rotation.eulerAngles + bullet1AngleOffset);
            Quaternion bullet2Quaternion = Quaternion.Euler(transform.rotation.eulerAngles + bullet2AngleOffset);
            Quaternion bullet3Quaternion = Quaternion.Euler(transform.rotation.eulerAngles + bullet3AngleOffset);

			Vector3 offset1 = bullet1Quaternion * bullet1Offset;
            Vector3 offset2 = bullet2Quaternion * bullet2Offset;
            Vector3 offset3 = bullet3Quaternion * bullet3Offset;

			GameObject bullet1Start = Instantiate(playerBullet, transform.position + offset1, bullet1Quaternion);
            GameObject bullet2Start = Instantiate(playerBullet, transform.position + offset2, bullet2Quaternion);
            GameObject bullet3Start = Instantiate(playerBullet, transform.position + offset3, bullet3Quaternion);
		}
    }
}
