using UnityEngine;
using System.Collections;
using System.Collections;

public class Player_Status : MonoBehaviour {
	public int playerHealth = 100;
	public float landing_tolerance = .75f;
	public bool touchdown = false; //Landing was within tolerance
	public bool crashed = false; //Landing was not.
	public int impact_hurt = 30; //damage from non-vertical impacts

	private Rigidbody2D rb;
	private Player_Controls p_c;
	private float y_speed, x_speed;
	private bool gameover = false;
	private float landed = .1f;
	private GameObject g_o;
	private GameObject victoryUI;
	private bool isGodMode = false; //gets god mode from Player_Controls

	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody2D>();
		p_c = gameObject.GetComponent<Player_Controls>();

		g_o = GameObject.FindGameObjectWithTag("GameOver");
		g_o.SetActive(false);

		victoryUI = GameObject.FindGameObjectWithTag("Victory");
		victoryUI.SetActive(false);

		isGodMode = gameObject.GetComponent<Player_Controls>().godMode;
	}

	// update the speed, check the player's status
	void Update () {
		CheckStatus();
		//Debug.Log("Downward Speed = " + y_speed);
	}

	void CheckStatus(){
		y_speed = rb.velocity.y;
		x_speed = rb.velocity.x;
		// If the player's ship has stopped moving and has landed
		// end the play session
		if( (y_speed <= landed && touchdown && Mathf.Abs(x_speed) < 0.25f) && !isGodMode){ 
			p_c.enabled = false;
			TouchDown();
		} else if ( (crashed || playerHealth <= 1) && !isGodMode){
			KillPlayer();
		}
	}

	void KillPlayer(){
		p_c.enabled = false;
		g_o.SetActive(true);
	}

	void TouchDown(){
		victoryUI.SetActive(true);
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag.Equals("Ground")){
			Collider2D collider = col.collider;

			//Determine where the collision happened
			Vector3 contactPoint = col.contacts[0].point;
			Vector3 center = collider.bounds.center;
			float colWidth = collider.bounds.size.x;
			float colHeight = collider.bounds.size.y;

			//If the collision is the side, hurt the player1
			bool hitRight = contactPoint.x >= (center.x + colWidth/2);
			bool hitLeft = contactPoint.x <= (center.x - colWidth/2);

			if( (hitRight || hitLeft) ){
				playerHealth -= impact_hurt;
			}

			//If it's from the top, check the landing
			bool hitTop = (contactPoint.y >= (center.y + colHeight/2) )
				&& (contactPoint.x <= (center.x + colWidth/2) )
				&& (contactPoint.x >= (center.x - colWidth/2) );

			if(hitTop && Mathf.Abs(y_speed) > landing_tolerance){ //crashed
				crashed = true;
				touchdown = false;
			}else if(hitTop && Mathf.Abs(y_speed) < landing_tolerance){ //landed
				crashed = false;
				touchdown = true;
			}
		}
	}
}
