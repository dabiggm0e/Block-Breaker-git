using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Paddle paddle;
	private Vector3 paddleToBallVector;
	private bool ballMoving = false;
	private Vector2 ballVelocity;
	private float maxBallSpeed = 7f;
	private float speedAccel = 1.3f;

	
	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;		
		ballVelocity = new Vector2(0f, maxBallSpeed);			
	}
	
	// Update is called once per frame
	void Update () {
			
			//Debug.Log("Ball speed " + rigidbody2D.velocity);
			if(!isBallMoving()) {
				this.transform.position = paddle.transform.position + paddleToBallVector;
				
				if( Input.GetMouseButtonDown(0) ) {	
					//print ("Left mouse button clicked");
					this.rigidbody2D.velocity = ballVelocity;
					ballMoving = true;	
				}
			}
			
			
			
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		Vector2 tweak = new Vector2( Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
		if(isBallMoving()) {							
			maintainVelocity();	   			
			this.rigidbody2D.velocity += tweak;			
			audio.Play();												
		}
	}
	
	void maintainVelocity() {
	
		rigidbody2D.velocity = rigidbody2D.velocity * speedAccel;				
		
		Vector2 maxVelocity = new Vector2( Mathf.Clamp(rigidbody2D.velocity.x, -maxBallSpeed, maxBallSpeed), 
		                                  Mathf.Clamp(rigidbody2D.velocity.y, -maxBallSpeed, maxBallSpeed));
		rigidbody2D.velocity = maxVelocity;					
	}
	
	public bool isBallMoving() {
		return ballMoving;
	}
}
