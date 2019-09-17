using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int collected;
    public bool movementAllowed;
    public bool touchedGround;
    public bool crowned;


    public void Awake() {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
        }
		DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        collected = 0;
        movementAllowed = true;
        touchedGround = false;
    }

    //////// MOVEMENT ////////
    public bool getMovementState() {
        return movementAllowed;
    }

    public bool allowMovement() {
        return movementAllowed = true;
    }

    public bool restrictMovement() {
        return movementAllowed = false;
    }

    //////// PEARLS COLLECTED ////////
    public int getCollected() {
        return collected;
    }

    public int incrementCollected() {
        return collected++;
    }

    public void resetCollected() {
        collected = 0;
    }

    //////// PLAYER GROUNDING ////////
    public void playerTouchedGround() {
        touchedGround = true;
        //Debug.Log("this should be true: "+touchedGround);
        // editor glitch: on resetting the scene, touchedGround still shows it's false, even though it's true....
    }

    public void resetGrounded() {
        touchedGround = false;
    }
}
