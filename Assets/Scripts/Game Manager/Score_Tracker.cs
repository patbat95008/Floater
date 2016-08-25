using UnityEngine;
using System.Collections;

public class Score_Tracker : MonoBehaviour {
	public int score = 0;

	private GameObject player;

	void Start (){
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate (){
		score = (int)Mathf.Abs( Mathf.Abs(gameObject.transform.position.y) - Mathf.Abs(player.transform.position.y) );
	}
}
