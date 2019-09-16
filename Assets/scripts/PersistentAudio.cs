using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAudio : MonoBehaviour {
	private static PersistentAudio instance;

	public static PersistentAudio GetInstance(){
		return instance;
	}
	
	public void Awake() {
		// Fade in audio at the very beginning
		StartCoroutine(FadeIn(GetComponent<AudioSource>(), 1f));

		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		}
		else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1) {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
