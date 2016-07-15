using UnityEngine;
using System.Collections;

public class BG_Copy : MonoBehaviour {
	public GameObject background;
	// Use this for initialization
	void Start () {
		for(int i = -5; i < 5; i++){
			for(int j = -5; j < 5; j++){
				Instantiate(background, 
					new Vector3( (float)(i)*25f, (float)(j)*25f, 10), 
					new Quaternion());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
