using UnityEngine;
using System.Collections;

public class Floater_Gen : MonoBehaviour {
	public GameObject[] floaters;
	public int floater_density;

	// Use this for initialization
	void Start () {
		//Create floating astroids randomly according to density
		for(int i = 0; i < floater_density; i++){
			int rand = (int)Random.Range(0,floaters.Length);
			Instantiate(floaters[rand], 
				new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0),
				new Quaternion());
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
