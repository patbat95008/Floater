using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour {
	public float moveSpeed = 10f; //The moveSpeed of the traveling object
	public float rotateSpeed = 2f;
	public bool side2side = false; //only move on the x axis
	public float yConst = .25f;//NOTE: only use with side2side == true

	private Vector2 rand_vector;
	private float rand_rotation;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		
		//Pick a random direction
		rand_vector.x = Random.Range(-moveSpeed, moveSpeed);

		if(!side2side){ //If no constant y speed, make a random value
			rand_vector.y = Random.Range(-moveSpeed, moveSpeed);
		}else
			rand_vector.y = yConst;
		
		rand_rotation = Random.Range(-rand_rotation, rand_rotation);

		//Set the random variables
		rb.velocity = rand_vector;
		rb.angularVelocity = rand_rotation;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
