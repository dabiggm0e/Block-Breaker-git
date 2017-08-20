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
	public ParticleSystem smoke;		
	
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
			blowSmoke();
			Destroy(gameObject);
		}
		else {
			LoadSprites();
		}
	}
	
	void LoadSprites() {
		int spriteIndex = timesHit-1;
		if(hitSprites[spriteIndex] != null) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else { // Log error in case a sprite was missing
			Debug.LogError("Brick sprite missing");
		}
	}
	
	public static void resetBricksCount() {
		breakableCount = 0;
	}
	
	private void blowSmoke() {
		ParticleSystem _smoke;
		_smoke = Instantiate(smoke, transform.position, Quaternion.identity) as ParticleSystem;
		_smoke.startColor = gameObject.GetComponent<SpriteRenderer>().color;
		_smoke.particleSystem.Play();	
		//Instantiate(smoke, transform.position, Quaternion.identity) as ParticleSystem;
	}
	
}
