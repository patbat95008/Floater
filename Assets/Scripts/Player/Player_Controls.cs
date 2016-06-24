using UnityEngine;
using System.Collections;

public class Player_Controls : MonoBehaviour {
	public float max_speedSide, max_speedUP;

	private Rigidbody2D rb;
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Left_in") && !Input.GetButton("Right_in")){
			//Debug.Log("Left");
			direction = Vector2.left * max_speedSide;
			rb.AddForce(direction);
		}else if(Input.GetButton("Right_in") && !Input.GetButton("Left_in")){
			//Debug.Log("Right");
			direction = Vector2.right * max_speedSide;
			rb.AddForce(direction);
		}else if(Input.GetButton("Right_in") && Input.GetButton("Left_in")){
			//Debug.Log("Up");
			direction = Vector2.up * max_speedUP;
			rb.AddForce(direction);
		}
	}
}
