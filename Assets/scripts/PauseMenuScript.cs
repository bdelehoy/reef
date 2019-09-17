using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {
	public GameManager gameManager;

	public void restartLevel() {
		gameManager.resetCollected();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void quitGame() {
		Application.Quit();
	}
}
