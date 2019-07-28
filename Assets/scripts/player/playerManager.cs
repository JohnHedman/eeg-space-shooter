using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{


    private bool hasTrippleShot = false;
    private bool hasInvulnerability = false;
    private float trippleShotDuration = 0;
    private float invulnerabilityDuration = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(hasTrippleShot)
        {
            trippleShotDuration -= Time.deltaTime;

            if(trippleShotDuration <= 0)
            {
                DeactivateTrippleShotPowerup();
            }
        }

        if(hasInvulnerability)
        {
            invulnerabilityDuration -= Time.deltaTime;

            if(invulnerabilityDuration <= 0)
            {
                DeactivateInvulnerability();
            }
        }
        
    }

    public void ActivateInvulnerability(float duration)
    {
        hasInvulnerability = true;
        invulnerabilityDuration = duration;

        this.GetComponent<PolygonCollider2D>().enabled = false;
        this.GetComponent<Animator>().SetBool("Invulnerable", true);
    }

    public void DeactivateInvulnerability()
    {
        hasInvulnerability = false;
        
        this.GetComponent<PolygonCollider2D>().enabled = true;
        this.GetComponent<Animator>().SetBool("Invulnerable", false);
    }

    public void ActivateTrippleShotPowerup(float duration)
    {
        hasTrippleShot = true;
        trippleShotDuration = duration;

        this.GetComponent<playerNormalShooting>().enabled = false;
        this.GetComponent<playerTrippleShooting>().enabled = true;
    }

    public void DeactivateTrippleShotPowerup()
    {
        hasTrippleShot = false;

        this.GetComponent<playerNormalShooting>().enabled = true;
        this.GetComponent<playerTrippleShooting>().enabled = false;
    }
}
