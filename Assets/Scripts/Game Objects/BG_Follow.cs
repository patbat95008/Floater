using UnityEngine;
using System.Collections;

public class BG_Follow : MonoBehaviour {
	public bool canFollow = true;
	public bool follow_vertical = false;

	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(canFollow && !follow_vertical) 
			gameObject.transform.position = player.transform.position;
		else if(canFollow && follow_vertical)
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y, gameObject.transform.position.z);
	}
}
