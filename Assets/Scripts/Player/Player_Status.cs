using UnityEngine;
using System.Collections;

public class Player_Status : MonoBehaviour {
	public int playerHealth = 100;
	public float landing_tolerance = .75f;
	public bool touchdown = false;
	public bool crashed = false;
	public int impact_hurt = 30;

	private Rigidbody2D rb;
	private float z_speed;
	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		z_speed = rb.velocity.y;
		Debug.Log("Downward Speed = " + z_speed);
	}



	void OnCollisionEnter2D(Collision2D col){
		Collider2D collider = col.collider;
		Vector3 contactPoint = col.contacts[0].point;
		Vector3 center = collider.bounds.center;
		float colWidth = collider.bounds.size.x;
		float colHeight = collider.bounds.size.y;
		//If the collision is the side, hurt the player1
		bool hitRight = contactPoint.x >= (center.x + colWidth/2);
		bool hitLeft = contactPoint.x <= (center.x - colWidth/2);

		if(hitRight || hitLeft){
			playerHealth -= impact_hurt;
		}

		//If it's from the top, check the landing
		bool hitTop = (contactPoint.y >= (center.y + colHeight/2) )
			&& (contactPoint.x <= (center.x + colWidth/2) )
			&& (contactPoint.x >= (center.x - colWidth/2) );

		if(hitTop && Mathf.Abs(z_speed) > landing_tolerance){ //crashed
			crashed = true;
			touchdown = false;
		}else if(hitTop && Mathf.Abs(z_speed) < landing_tolerance){ //landed
			crashed = false;
			touchdown = true;
		}
	}
}
