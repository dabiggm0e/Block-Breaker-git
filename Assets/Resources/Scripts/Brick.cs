using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public static int breakableCount = 0;
	public Sprite[] hitSprites;
	public AudioClip breakSound;
	public AudioClip unbreakableHit;
	
	private int maxHits;
	private int timesHit;	
	private LevelManager levelManager;
	private bool isBreakable, isUnbreakable;
	
		
	
	
	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		isUnbreakable = (this.tag == "Unbreakable");		
		timesHit = 0;
		maxHits = hitSprites.Length+1;		
		
		levelManager = GameObject.FindObjectOfType<LevelManager>();		
		increaseBricksCount();		
	}
	
	void increaseBricksCount() {
		if(isBreakable) 
			++breakableCount;
	}
	
	void OnCollisionEnter2D(Collision2D brickCollision) {
		//Debug.Log(gameObject.tag + " - " + this.tag);	
		
		
		if(isBreakable) {			
			HandleHits();				
		}	
		else if (isUnbreakable) {
			AudioSource.PlayClipAtPoint(unbreakableHit, transform.position);
		}			
	}
	
	
	void HandleHits() {
		++timesHit;
		
		if(timesHit>=maxHits) {						
			--breakableCount;
			AudioSource.PlayClipAtPoint(breakSound, transform.position);	
			levelManager.BrickDestroyedMessage();		
			Destroy(gameObject);
		}
		else {
			LoadSprites();
		}
	}
	
	void LoadSprites() {
		int spriteIndex = timesHit-1;
		if(hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
	}
	
	
}
