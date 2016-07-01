﻿using UnityEngine;
using System.Collections;

public class Planet_Generator : MonoBehaviour {
	public GameObject[] floaters, grounds, cliffs, treasures;
	public int floater_density, space_distance, hole_density;

	private float managerPOS;
	private GameObject[] ground_game;
	// Use this for initialization
	void Start () {
		//Create floating astroids randomly according to density
		for(int i = 0; i < floater_density; i++){
			int rand = (int)Random.Range(0,floaters.Length);
			Instantiate(floaters[rand], 
				new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0),
				new Quaternion());
		}
		//Create level ground
		for(int i = -50; i < 50; i+= 2){
			int rand = (int)Random.Range(0,grounds.Length);
			Instantiate(grounds[rand],
				new Vector3(i, -space_distance, 0),
				new Quaternion());
		}


		//Carve holes into the ground of size x and y
		//Add treasures
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
