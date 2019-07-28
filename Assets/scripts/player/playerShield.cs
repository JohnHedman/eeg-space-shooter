using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShield : MonoBehaviour
{
    public GameObject playerShip;
    public Sprite playerShieldSprite;
    public Vector3 shieldOffset = new Vector3(0, 0.2f, 0);



    private int shieldValue;


    void OnTriggerEnter2D() {
        shieldValue = playerShip.GetComponent<playerDamage>().getCurrentShield();
		shieldValue -= 25;

        if(shieldValue <= 0){
            shieldValue = 0;
            shieldDown();
        }

        playerShip.GetComponent<playerDamage>().setCurrentShield(shieldValue);
	}



    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
        shieldValue = playerShip.GetComponent<playerDamage>().getCurrentShield();

        if(shieldValue == 0)
        {
            shieldDown();
        }
        else{
            shieldUp();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerShip == null){
            playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
        }
        else{
            shieldValue = playerShip.GetComponent<playerDamage>().getCurrentShield();

            if(shieldValue == 0)
            {
                shieldDown();
            }
            else{
                shieldUp();
            }
            
            Vector3 playerLocation = playerShip.transform.position;

            this.transform.position = playerLocation + shieldOffset;
        }
    }



    private void shieldUp()
    {
        this.GetComponent<PolygonCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().sprite = playerShieldSprite;
    }

    private void shieldDown()
    {
        this.GetComponent<PolygonCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().sprite = null;
    }



}
