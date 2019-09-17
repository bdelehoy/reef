using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public float horizontalSpeed;
	public int jumpFactor;
	public float maxVerticalVelocity;
	public float maxHorizontalVelocity;

	public GameManager gameManager;
	private bool paused;
	public GameObject pauseMenu;

    private Rigidbody2D rb;
	private Vector2 movementVector;	// so we only have to call AddForce once

	public GameObject endingScreen;

	private AudioSource auso;
	public AudioClip[] collectedSFX;
	public AudioClip exitSound;


	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		auso = GetComponent<AudioSource>();
		pauseMenu.SetActive(false);
		endingScreen.SetActive(false);
		Time.timeScale = 1f;
		paused = false;
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
		if (gameManager.getMovementState()) {
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
			// collision with a pearl
			Destroy(other.gameObject);
			auso.PlayOneShot(collectedSFX[gameManager.incrementCollected()]);
		}
		if(other.gameObject.tag == "Door") {
			// collision with the exit
			// original door coords: X 150.5, Y -22.5 (saving for restoring after testing)
			gameManager.restrictMovement();
			auso.PlayOneShot(exitSound);
			endingScreen.SetActive(true);
		}
	}

	public void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Ground" && !gameManager.touchedGround) {
			gameManager.playerTouchedGround();
		}
	}

}
