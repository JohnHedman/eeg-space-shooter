using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMove : MonoBehaviour {

	public GameObject backgroundPrefab;
	public float backgroundSpeed = -3.0f;
	public float spawnZone = -0.5f;
	public float deleteZone = -22.0f;
	public Vector3 spawnPoint = new Vector3(0, 22, 0);
	private bool hasMadeCopy;
	private int backgroundLayer;


	// Use this for initialization
	void Start () {
		hasMadeCopy = false;
		backgroundLayer = gameObject.layer;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 backgroundPos = transform.position;
		backgroundPos.y += backgroundSpeed * Time.deltaTime;


		// Delete bullets once they are off the screne
		if(!hasMadeCopy && backgroundPos.y < spawnZone)
		{
			hasMadeCopy = true;
			GameObject backgroundSpawn = (GameObject)Instantiate(backgroundPrefab, spawnPoint, transform.rotation);
			backgroundSpawn.layer = backgroundLayer;
		}
		if(backgroundPos.y < deleteZone)
		{
			Destroy(gameObject);
		}


		// Set the position to the calculated value.
		transform.position = backgroundPos;
		
	}
}
