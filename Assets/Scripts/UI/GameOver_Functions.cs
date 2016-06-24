using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver_Functions : MonoBehaviour {
	public void restart(){
		string currentScene = SceneManager.GetActiveScene().ToString();
		SceneManager.LoadScene(currentScene);
	}

	public void loadLevel(string level){SceneManager.LoadScene(level);}

}
