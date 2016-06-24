using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Start_Screen_Func : MonoBehaviour {
	public void loadLevel(string level){
		SceneManager.LoadScene(level);
	}
}
