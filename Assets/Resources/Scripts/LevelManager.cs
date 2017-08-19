using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private MusicPlayer musicPlayer;
	
	void Start() {
		musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();	
		
		if(isStartLevel()) 
			musicPlayer.resumeMusic();		
		else
			musicPlayer.stopMusic();
			
		if(isGameOver()) 
			musicPlayer.playGameOverSound();
		else if(isGameWon()) 
			musicPlayer.playGameWinSound();	
	}

	
	public void LoadLevel(string name) {	
		Application.LoadLevel(name);
	}
	
	public void LoadLevel(int index) {		
		Application.LoadLevel(index);
	}
	
	public void QuitGame() {
		Application.Quit();
	}
	
	public void LoadNextLevel() {	
		musicPlayer.stopMusic();
		LoadLevel( Application.loadedLevel + 1);		
	}
	
	public void BrickDestroyedMessage() {	
		//Debug.Log("Breakable bricks left are " + Brick.breakableCount);	
		if( Brick.breakableCount <= 0) {
			LoadNextLevel();
		}
	}
			
	private bool isStartLevel() {
		return (Application.loadedLevel==0)?true:false;
	}
	
	public void gameOver() {
		LoadLevel( Application.levelCount -1 );
	}
	
	private bool isGameOver() {	
		Brick.resetBricksCount();	
		return (Application.loadedLevel==Application.levelCount -1)?true:false;
	}
	
	private bool isGameWon() {
		return (Application.loadedLevel==Application.levelCount -2)?true:false;
	}
	
}