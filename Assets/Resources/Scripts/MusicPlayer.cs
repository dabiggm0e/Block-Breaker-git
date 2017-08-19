using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer instance;
	public  AudioClip winClip;
	public  AudioClip loseClip;
	
	void Awake() {
		if(instance != null) {
			GameObject.DestroyObject(gameObject);	
		}
		else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
	
	public void setVolume(float vol) {
		gameObject.audio.volume = vol;
	}
	
	public void stopMusic() {
		gameObject.audio.volume = 0f;
	}
	
	public void resumeMusic() {
		gameObject.audio.volume = 0.24f;
		gameObject.audio.Play();
	}	
	
	public void playGameOverSound() {
		AudioSource.PlayClipAtPoint(loseClip, transform.position);
	}
	
	public void playGameWinSound() {
		AudioSource.PlayClipAtPoint(winClip, transform.position);
	}
}
