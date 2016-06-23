using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject toFollow;
	public float speed = 3f;
	public Vector2 minPoint = new Vector2(-1, -1);
	public Vector2 maxPoint = new Vector2(1, 1);

	private float lastTime;
	private float journeyLength;
	// if nothing to follow, follow the player
	void Start () {
		lastTime = Time.time;
		if(toFollow == null)
			toFollow = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//float distCovered = (Time.time - lastTime) * speed;
		//float fracJourney = distCovered / journeyLength;
		//if(isOut())
		//transform.position = Vector3.Lerp(transform.position, toFollow.transform.position, fracJourney);
		transform.position = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y, -10);
		//lastTime = Time.time;
	}

	bool isOut(){
		float yDiff =  (toFollow.transform.position.y - transform.position.y);
		float xDiff =  (toFollow.transform.position.x - transform.position.x);
		return (xDiff > maxPoint.x) || (xDiff < minPoint.x) || (yDiff > maxPoint.y) || (yDiff < minPoint.y);
	}
}
