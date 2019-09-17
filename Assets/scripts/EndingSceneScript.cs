using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneScript : MonoBehaviour {
	public GameManager gameManager;
	public UnityEngine.UI.Text fbText;
	public GameObject playerCrown;

	void OnEnable() {
		// when the player touches the door
		fbText.text = "You got " + gameManager.getCollected() + "/2 pearls!";
		if(!gameManager.touchedGround) {
			fbText.text += "\nDidn't touch the walls either! Nice!!";
			Debug.Log("the crown will be awarded");
			gameManager.crowned = true;
			playerCrown.GetComponent<CrownScript>().MakeVisible();
		}
	}

    public void continueExploring() {
		// do NOT resetCollected() - the player might need to collect more pearls
		gameManager.allowMovement();
		transform.root.gameObject.SetActive(false);
	}

	public void restartLevel() {
		gameManager.resetCollected();
		gameManager.allowMovement();
		gameManager.resetGrounded();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void quitGame() {
		Application.Quit();
	}
}
