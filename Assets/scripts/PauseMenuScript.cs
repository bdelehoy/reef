using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {
	public GameManager gameManager;

	public void restartLevel() {
		gameManager.resetCollected();
		// don't worry about allowMovement() here because timescale is set to 0 in the player script
		gameManager.resetGrounded();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void quitGame() {
		Application.Quit();
	}
}
