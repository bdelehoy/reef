using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public float horizontalSpeed;
	public int jumpFactor;
	public float maxVerticalVelocity;
	public float maxHorizontalVelocity;

	private bool paused;
	public GameObject pauseMenu;

    private Rigidbody2D rb;
	private Vector2 movementVector;	// so we only have to call AddForce once

	private int collected = 0;
	public GameObject endingScreen;
	private bool gameOver;

	private AudioSource auso;
	public AudioClip[] collectedSFX;


	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		auso = GetComponent<AudioSource>();
		pauseMenu.SetActive(false);
		endingScreen.SetActive(false);
		Time.timeScale = 1f;
		paused = false;
		gameOver = false;
	}

	private void Update() {
		CheckInput();
	}

	private void FixedUpdate () {
		rb.AddForce(movementVector);
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalVelocity, maxHorizontalVelocity),
								  Mathf.Clamp(rb.velocity.y, -maxVerticalVelocity,   maxVerticalVelocity) );
		movementVector.x = movementVector.y = 0;
	}

	private void CheckInput() {
		if (!gameOver) {
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
					movementVector.x = horizontalSpeed * Time.deltaTime;
			}
			if (Input.GetAxisRaw("Horizontal") < 0f){
					movementVector.x = -1 * horizontalSpeed * Time.deltaTime;
			}
			if (Input.GetAxisRaw("Jump") > 0f){
					movementVector.y = jumpFactor * Time.deltaTime;
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Collectible") {
			Destroy(other.gameObject);
			// multitasking! index into SFX array at the same time and increment collected:
			auso.PlayOneShot(collectedSFX[collected++]);
		}
		if(other.gameObject.tag == "Door") {
			// original door coords: X 150, Y -22.55 (saving for restoring after testing)
			gameOver = true;
			endingScreen.SetActive(true);
		}
	}
}
