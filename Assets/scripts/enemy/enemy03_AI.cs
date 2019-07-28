using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy03_AI : MonoBehaviour
{
    public float approachSpeed = -10.0f;
    public float kamikazeSpeed = -10.0f;
    public float kamikazeDelayLow = 5.0f;
    public float kamikazeDelayHigh = 10.0f;
    public Vector3 kamikazeBoundry = new Vector3(0, 0, 0);
    public GameObject bulletPrefab;
    public GameObject target;
    private float cooldownTimer;
    private bool hasDirected = false;
    private Animator animationController;




    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerShip");
        cooldownTimer = Random.Range(kamikazeDelayLow, kamikazeDelayHigh);
        animationController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is dead and the kamikaze ship has not set its direction, then wait for the player to respawn
        if(!hasDirected && target == null)
        {
            target = GameObject.FindGameObjectWithTag("PlayerShip");
        }
        // If the player is dead and the kamikaze ship has set its direction, then continue on.
        else if(hasDirected && target == null)
        {
            Vector3 pos = transform.position;

            // Find the position needed to go towards the player
            Vector3 shipVelocity = new Vector3(0, kamikazeSpeed * Time.deltaTime, 0);
            pos += transform.rotation * shipVelocity;
            this.transform.position = pos;
        }
        // If the player is alive, then operate normaly
        else
        {
            cooldownTimer -= Time.deltaTime;
            if(!animationController.GetBool("Death") && cooldownTimer <= 0)
            {
                Vector3 pos = transform.position;
                Vector3 shipRotation;

                if(pos.y <= kamikazeBoundry.y)
                {
                    if(!hasDirected)
                    {
                        hasDirected = true;

                        // Find the rotation needed to face the player
                        Vector3 targetPos = target.transform.position;
                        targetPos.x = targetPos.x - pos.x;
                        targetPos.y = targetPos.y - pos.y;
                        float targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                        shipRotation = new Vector3(0, 0, targetAngle + 90.0f);
                        this.transform.rotation = Quaternion.Euler(shipRotation);
                    }

                    // Find the position needed to go towards the player
                    Vector3 shipVelocity = new Vector3(0, kamikazeSpeed * Time.deltaTime, 0);
                    pos += transform.rotation * shipVelocity;

                }
                else
                {
                    pos.y += approachSpeed * Time.deltaTime;
                }

                this.transform.position = pos;              
            }
        }
    }

    public void DisableEnemyManager()
	{
		this.GetComponent<enemy03Manager>().enabled = false;
	}
}
