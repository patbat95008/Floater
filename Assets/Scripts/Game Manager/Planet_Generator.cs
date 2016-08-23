using UnityEngine;
using System.Collections;

public class Planet_Generator : MonoBehaviour {
	public GameObject[] grounds; //surface pieces
	public GameObject[] cliffs; //surface hole pieces
	public GameObject[] ground_fill; //underground fill in pieces
	public Sprite[] ground_sprites;
	public int space_distance, hole_density, fill_length = 50;

	private float managerPOS;
	private GameObject[] ground_tiles;
	private int[] hole_locations;
	private GameObject[,] underGround;
	// Use this for initialization
	void Start () {
		underGround = new GameObject[fill_length, 51];
		//Create level ground + underground layer 1
		for(int i = 0; i <= 100; i+= 2){
			int rand = (int)Random.Range(0,grounds.Length);
			//Create flat plane of dirt
			Instantiate(grounds[rand],
				new Vector3(i-50, -space_distance, 0),
				new Quaternion());

			//Create top underground layer (To remove chunks during carve_holes)
			underGround[0,i/2] = (GameObject) Instantiate(ground_fill[0],
			new Vector3(i-50, -space_distance-2, 0),
			new Quaternion());
		}
		ground_tiles = GameObject.FindGameObjectsWithTag("Ground"); //Populate ground list
		hole_locations = new int[hole_density + 1];

		//Carve holes into the ground randomly (2 cliffs replace 2 ground tiles, remainder is empty space)
		carve_holes();
		//Add caverns
		carve_caverns();
	}

	//Carve holes in a flat plane of ground
	//Mark where the holes are dug for cavern creation
	void carve_holes(){
		for(int i = 0; i <= hole_density; i++){
			int randG = (int)Random.Range(25, ground_tiles.Length - 25);
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
			/*
			Destroy(underGround[0, randG + (h_size/2)]);
			underGround[0, randG + (h_size/2)] = null;
			*/
			int layerWidth = (int)Random.Range(1, 3);
			for(int k = 0; k < layerWidth; k++){
				Destroy(underGround[0, randG + k]);
				underGround[0, randG + k] = null;
			}

			hole_locations[i] = randG;

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
			for(int j = 0; j <= fill_length; j++){
				Vector3 g_pos = new Vector3(-50+i*2, -j*2 - (space_distance + 4 ), 0);
				Object g = Instantiate(ground_fill[0],
							g_pos, 
							new Quaternion());
				
				underGround[i,j] = (GameObject) g;
			}
		}

		for(int i = 0; i < hole_locations.Length; i++){
			carve(hole_locations[i], 0);
		}
	}

	//carves in a downward direction until it runs into an edge
	void carve(int x, int y){
		//check for overflow, if so, stop
		if((x < 0 || x > 50) || (y < 0 || y > fill_length))
			return;

		//Set the surrounding tiles to face the hole
		if(x-1 >= 0 && underGround[x-1,y] != null)
			underGround[x-1,y].GetComponent<SpriteRenderer>().sprite = ground_sprites[1];
		if(x+1 <= 50 && underGround[x+1,y] != null)
			underGround[x+1,y].GetComponent<SpriteRenderer>().sprite = ground_sprites[2];
		if(y-1 >= 0 && underGround[x,y-1] != null)
			underGround[x,y-1].GetComponent<SpriteRenderer>().sprite = ground_sprites[3];
		if(y+1 < fill_length && underGround[x,y+1] != null)
			underGround[x,y+1].GetComponent<SpriteRenderer>().sprite = ground_sprites[0];
		//destroy current x,y location
		Destroy(underGround[x, y]);
		underGround[x, y] = null;

		//determine next move
		int dir = (int) Random.Range(0, 10);
		if(dir < 5){
			//carve down
			carve(x, y+1);
		}else if(dir <= 7){
			if(x-1 > 1)
				carve(x-1, y);//carve left
			else
				carve(x,y+1);//Unless you hit a wall.
		}else if(dir <= 9){
			if(x+1 > 49)
				carve(x+1, y);//carve right
			else
				carve(x, y+1);//Unless you hit a wall.
		}else{
			//carve up
			carve(x, y-1);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
