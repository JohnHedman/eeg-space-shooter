using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarControl : MonoBehaviour {

	public GameObject playerShip;

	private Slider healthSlider;

	// Use this for initialization
	void Start () {
		playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
		healthSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerShip == null)
		{
			healthSlider.value = 0;
			playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
		}
		else{
			healthSlider.value = playerShip.GetComponent<playerDamage>().getCurrentHeath();
		}
	}
}
