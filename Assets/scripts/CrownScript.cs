using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrownScript : MonoBehaviour
{
    public GameManager gameManager;
    private SpriteRenderer sr;
    // private GameObject player;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log("the player should have a crown: "+gameManager.crowned);
        if(gameManager.crowned) {
            Debug.Log("the crown is here....");
            MakeVisible();
        }
        // if (GameObject.FindWithTag("Player") != null) {
        //     player = GameObject.FindWithTag("Player");
        // }
    }

    public void MakeVisible() {
        if(gameManager.crowned) {
            Color tmp = sr.color;
            tmp.a = 1f;
            sr.color = tmp;
        }
    }


    // public void Update() {
    //     transform.position = player.transform.position + new Vector3(0,1.2f,0);
    // }

}
