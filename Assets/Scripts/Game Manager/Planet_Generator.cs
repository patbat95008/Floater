using UnityEngine;
using System.Collections;

public class Planet_Generator : MonoBehaviour {
	public GameObject[] grounds, cliffs, treasures;
	public int space_distance, hole_density;

	private float managerPOS;
	private GameObject[] ground_tiles;
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

		//Carve holes into the ground randomly (2 cliffs replace 2 ground tiles)
		for(int i = 0; i < hole_density; i++){
			int randG = (int)Random.Range(1, ground_tiles.Length - 1);
			//int randC = (int)Random.Range(0, cliffs.Length);

			if(ground_tiles[randG].tag.Equals("Ground")){
				Vector3 g_pos1 = new Vector3(ground_tiles[randG].transform.position.x, 
								ground_tiles[randG].transform.position.y, 
								0);
				Vector3 g_pos2 = new Vector3(ground_tiles[randG + 1].transform.position.x, 
					ground_tiles[randG + 1].transform.position.y, 
					0);

				Object cliff1 = Instantiate(cliffs[0], //Left cliff
								g_pos1,
								new Quaternion());
				Object cliff2 = Instantiate(cliffs[1],
								g_pos2,
								new Quaternion());

				Destroy(ground_tiles[randG]);
				Destroy(ground_tiles[randG + 1]);
				ground_tiles[randG] = (GameObject)cliff1;
				ground_tiles[randG + 1] = (GameObject)cliff2;
			}
		}
		//Add treasures

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
