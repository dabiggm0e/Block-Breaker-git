using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	
	public static bool autoPlay = false;
	
	private const int WORLD_UNITS = 16;
	private Vector3 paddlePos;	
	private Ball ball; 
	private LevelManager levelManager;
	private float keyboardMoveOffset = 0.05f;
	
	
	
	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>(); 
		paddlePos.y = transform.position.y;
	}
	
	
	// Update is called once per frame
	void Update () {		
	
		checkCheatCodes();
			
		if(autoPlay) 
			AutoPlay();
		else {
			MoveWithMouse();
			//MoveWithKeyboard();
		}
	}
	
	void AutoPlay() {	
		Vector3 ballPos = ball.transform.position;			
		paddlePos.x = Mathf.Clamp(ballPos.x, 0.5f, 15.5f);
		transform.position = paddlePos;
	}
	
	void MoveWithMouse() {
	
		float mousePosInBlocks;
		paddlePos = new Vector3( this.transform.position.x,
		                        this.transform.position.y,
		                        this.transform.position.z);
		                        
		mousePosInBlocks = Input.mousePosition.x / Screen.width * WORLD_UNITS; // 16 world units
		paddlePos.x = Mathf.Clamp(mousePosInBlocks, 0.5f, 15.5f);
		this.transform.position = paddlePos;	
	}
	
	void MoveWithKeyboard() {
		paddlePos = new Vector3( this.transform.position.x,
		                        this.transform.position.y,
		                        this.transform.position.z);
		
		if(Input.GetKey(KeyCode.LeftArrow)) {
			paddlePos.x = Mathf.Clamp(paddlePos.x-keyboardMoveOffset, 0.5f, 15.5f);			
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			paddlePos.x = Mathf.Clamp(paddlePos.x+keyboardMoveOffset, 0.5f, 15.5f);			
		}
		
		this.transform.position = paddlePos;
	}
	
	void checkCheatCodes() {					
		if(Input.GetKeyDown( KeyCode.A )) {
			//Debug.Log("KeyCode A");
			autoPlay = !autoPlay;
		}	
	}
}
