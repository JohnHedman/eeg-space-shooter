using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletSound : MonoBehaviour {

	public AudioSource bulletCreationSound;

	// Use this for initialization
	void Start () {
		bulletCreationSound.Play(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
