using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
	public float horizontalSpeed;
	public int jumpFactor;
	public float maxVerticalVelocity;
	public float maxHorizontalVelocity;

	private bool paused = false;
	public GameObject pauseMenu;

    private Rigidbody2D rb;
	private Vector2 movementVector;	// so we only have to call AddForce once

	private int collected = 0;
	public GameObject[] endings;	// boilerplate

	private AudioSource auso;
	public AudioClip[] sfx;	// probably a bad design but this is small scale so ehhh


	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		auso = GetComponent<AudioSource>();
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		paused = false;
	}

	private void Update() {
		CheckInput();
	}

	private void FixedUpdate () {
		rb.AddForce(movementVector);
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalVelocity, maxHorizontalVelocity),
								  Mathf.Clamp(rb.velocity.y, -maxVerticalVelocity,   maxVerticalVelocity) );	// just in case
		movementVector.x = movementVector.y = 0;
	}

	public void OnTriggerEnter2D(Collider2D other) {
		CollideWithCollectible(other);
		CollideWithDoor(other);
	}

	private void CheckInput() {
		if (Input.GetButtonDown("Pause")){
			if (!paused) {	// if the game isn't paused, make it paused
				pauseMenu.SetActive(true);
				Time.timeScale = 0f;
				paused = true;
			}
			else {	// the game is currently paused and it shouldn't be anymore
				pauseMenu.SetActive(false);
				Time.timeScale = 1f;
				paused = false;
			}
		}
		if (Input.GetAxisRaw("Horizontal") > 0f){
				movementVector.x = horizontalSpeed*Time.deltaTime;
		}
		if (Input.GetAxisRaw("Horizontal") < 0f){
				movementVector.x = -1*horizontalSpeed*Time.deltaTime;
		}
		if (Input.GetAxisRaw("Jump") > 0f){
				movementVector.y = jumpFactor*Time.deltaTime;
		}
	}

	private void CollideWithCollectible(Collider2D other) {
		if(other.gameObject.tag == "Collectible") {
			Destroy(other.gameObject);
			auso.PlayOneShot(sfx[collected++]);
		}
	}

	private void CollideWithDoor(Collider2D other) {
		// ew.
		if(other.gameObject.tag == "Door") {
			if(collected == 2) {
				endings[2].SetActive(true);
				endings[1].SetActive(false);
				endings[0].SetActive(false);
				endings[3].SetActive(false);
			}
			else if(collected == 1) {
				endings[1].SetActive(true);
				endings[0].SetActive(false);
				endings[3].SetActive(true);
			}
			else {
				endings[0].SetActive(true);
				endings[3].SetActive(true);
			}
		}
	}
}
