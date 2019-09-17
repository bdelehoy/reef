using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneScript : MonoBehaviour {
	public GameManager gameManager;
	public UnityEngine.UI.Text fbText;

	void OnEnable() {
		// when the player touches the door
		gameManager.restrictMovement();
		fbText.text = "You got " + gameManager.getCollected() + "/2 pearls!";
	}

    public void continueExploring() {
		gameManager.allowMovement();
		transform.root.gameObject.SetActive(false);
	}

	public void restartLevel() {
		gameManager.resetCollected();
		gameManager.allowMovement();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void quitGame() {
		Application.Quit();
	}
}
