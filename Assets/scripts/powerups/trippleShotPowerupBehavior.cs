using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trippleShotPowerupBehavior : MonoBehaviour
{

    public AudioClip powerupSound;
    public float timeAllowed = 15.0f;

    private GameObject playerShip;
    private AudioSource objectAudio;
    private float timeRemaining;
    private bool isFlashing;

    void OnTriggerEnter2D() {
        
        // Play the powerup sound
        objectAudio.PlayOneShot(powerupSound);

        // Give the player tripple shot for 15 seconds.
        playerShip.GetComponent<playerManager>().ActivateTrippleShotPowerup(15.0f);

        Destroy(gameObject, 0.3f);
	}


    // Start is called before the first frame update
    void Start()
    {
        isFlashing = false;
        timeRemaining = timeAllowed;
        objectAudio = GetComponent<AudioSource>();
        playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if(playerShip == null){
            playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
        }

        // If the player is running out of time, warn them by flashing powerup.
        if(!isFlashing && timeRemaining <= 7.5f)
        {
            isFlashing = true;
            this.GetComponent<Animator>().SetBool("Flashing", true);
        }

        // If the player ran out of time, destroy the powerup
        if(timeRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }
}
