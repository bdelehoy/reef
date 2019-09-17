using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int collected;
    public bool movementAllowed;

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

}
