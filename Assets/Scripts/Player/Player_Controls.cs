using UnityEngine;
using System.Collections;

public class Player_Controls : MonoBehaviour {
	public bool godMode = false; //makes fuel infinite - Can't land or die
	public float force_side, force_up;
	public float fuel_Left = 100f, fuel_Right = 100f;
	public float fuel_Cap = 100f, fuel_drain = 100f;
	public bool cool_left = false, cool_right = false;

	private Rigidbody2D rb;
	private Vector2 direction; //direction the player moves
	private float left_cool_timer, right_cool_timer;
	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		//Check for player input - If they can move, do it
		if(CanLeftThrust()){
			//Debug.Log("Left");
			direction = Vector2.left * force_side;
			rb.AddForce(direction);
			fuel_Left -= Time.deltaTime * fuel_drain;
		}else if( CanRightThrust()){
			//Debug.Log("Right");
			direction = Vector2.right * force_side;
			rb.AddForce(direction);
			fuel_Right -= Time.deltaTime * fuel_drain;
		}else if(CanBothThrust()){
			//Debug.Log("Up");
			direction = Vector2.up * force_up;
			rb.AddForce(direction);
			fuel_Left -= Time.deltaTime * fuel_drain;
			fuel_Right -= Time.deltaTime * fuel_drain;
		}else{
			if(fuel_Left <= fuel_Cap) fuel_Left += Time.deltaTime * fuel_drain/2;
			if(fuel_Right <= fuel_Cap) fuel_Right += Time.deltaTime * fuel_drain/2;
		}

		//Check fuel levels and cool off if too hot
		if(fuel_Left <= 10 && !cool_left) {
			cool_left = true;
			left_cool_timer = 1;
		}
		if(fuel_Right <= 10 && !cool_right) {
			cool_right = true;
			right_cool_timer = 1;
		}

		//Count down any cool down timers
		if(cool_left && left_cool_timer >= 0){
			left_cool_timer -= Time.deltaTime;
		}else{
			cool_left = false;
		}
		if(cool_right && right_cool_timer >= 0){
			right_cool_timer -= Time.deltaTime;
		}else{
			cool_right = false;
		}
	}

	//Checks for fuel level, cooldown, input, and god mode
	public bool CanLeftThrust(){
		return ( (Input.GetButton("Left_in") && !Input.GetButton("Right_in") )
			&& ( (fuel_Left >= 0 && !cool_left) || godMode) ); 
	}
	public bool CanRightThrust(){
		return ( (Input.GetButton("Right_in") && !Input.GetButton("Left_in") )
			&& ( (fuel_Right >= 0 && !cool_right) || godMode) ); 
	}
	public bool CanBothThrust(){
		return ( (Input.GetButton("Left_in") && Input.GetButton("Right_in") )
			&& ( (fuel_Left >= 0 && fuel_Right >= 0 && !cool_left && !cool_right) || godMode) );
	}
}
