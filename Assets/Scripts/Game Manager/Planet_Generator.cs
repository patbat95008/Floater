using UnityEngine;
using System.Collections;

public class Planet_Generator : MonoBehaviour {
	public GameObject[] grounds; //surface pieces
	public GameObject[] cliffs; //surface hole pieces
	public GameObject[] ground_fill; //underground fill in pieces
	public int space_distance, hole_density, fill_length = 50;

	private float managerPOS;
	private GameObject[] ground_tiles;
	private Vector3[] hole_locations;
	private GameObject[,] underGround;
	// Use this for initialization
	void Start () {
		//Create level ground
		for(int i = -50; i <= 50; i+= 2){
			int rand = (int)Random.Range(0,grounds.Length);
			Instantiate(grounds[rand],
				new Vector3(i, -space_distance, 0),
				new Quaternion());
		}
		ground_tiles = GameObject.FindGameObjectsWithTag("Ground"); //Populate ground list
		hole_locations = new Vector3[hole_density + 1];
		underGround = new GameObject[50,fill_length];
		//Carve holes into the ground randomly (2 cliffs replace 2 ground tiles, remainder is empty space)
		carve_holes();
		//Add caverns
		carve_caverns();
	}

	//Carve holes in a flat plane of ground
	//Mark where the holes are dug for cavern creation
	void carve_holes(){
		for(int i = 0; i <= hole_density; i++){
			int randG = (int)Random.Range(4, ground_tiles.Length - 6);
			int h_size = (int)Random.Range(2, 4);
			//keep looking if there isn't a valid hole yet
			while(ground_tiles[randG] == null || ground_tiles[randG+h_size] ==null){
				randG = (int)Random.Range(4, ground_tiles.Length - 6);
			}
			//simplify cliff positions to each end of hole
			Vector3 g_pos1 = new Vector3(ground_tiles[randG].transform.position.x, 
				ground_tiles[randG].transform.position.y, 
				0);
			Vector3 g_pos2 = new Vector3(ground_tiles[randG + h_size].transform.position.x, 
				ground_tiles[randG + h_size].transform.position.y, 
				0);

			Object cliff1 = Instantiate(cliffs[0], //Set left cliff
				g_pos1,
				new Quaternion());
			Object cliff2 = Instantiate(cliffs[1], //Set right cliff
				g_pos2,
				new Quaternion());

			//hole_locations[i] = g_pos1;


			for(int j = 0; j <= h_size; j++){
				Destroy(ground_tiles[randG + j]);
				ground_tiles[randG + j] = null;
			}

			ground_tiles[randG] = (GameObject)cliff1;
			ground_tiles[randG + h_size] = (GameObject)cliff2;
		}
	}

	void carve_caverns(){
		//initialize a 2d array of tiled ground objects
		for(int i = 0; i < 50; i++){
			for(int j = 0; j < fill_length; j++){
				Vector3 g_pos = new Vector3(-50+i*2, -j*2 - (space_distance + 2 ), 0);
				Object g = Instantiate(ground_fill[0],
							g_pos, new Quaternion());
				underGround[i,j] = (GameObject) g;
			}
		}

		//Carve out a continious hole for each hole_location

	}

	// Update is called once per frame
	void Update () {
	
	}
}
