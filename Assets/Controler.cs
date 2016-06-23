using UnityEngine;
using System.Collections;

public class Controler : MonoBehaviour {
	public float landing_tolerance = 100;

	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){
		
	}
}
