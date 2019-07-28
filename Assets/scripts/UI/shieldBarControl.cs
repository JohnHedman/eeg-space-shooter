using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shieldBarControl : MonoBehaviour {

	public GameObject playerShip;

	private Slider shieldSlider;

	// Use this for initialization
	void Start () {
		playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
		shieldSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerShip == null){
			shieldSlider.value = 0;
			playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
		}
		else{
			shieldSlider.value = playerShip.GetComponent<playerDamage>().getCurrentShield();
		}
	}
}
