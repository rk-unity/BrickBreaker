using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public static int breakableCount = 0;

    public GameObject smoke;
    public int timesHit;
    public Sprite[] hitSprites;
        
    LevelManager levelManager;
    bool isBreakable;

    // Use this for initialization
    void Start () {
        isBreakable = (this.tag == "Breakable");
        if (isBreakable)
        {
            breakableCount++;
        }

        timesHit = 0;
        levelManager = (LevelManager)GameObject.FindObjectOfType(typeof(LevelManager));

	}
         
    void OnCollisionEnter2D (Collision2D col)
    {
        if (isBreakable) {
            HandleHits();
        }
    }

    void HandleHits()
    {
        // This was a neat example, but I'd rather just hear the 'bounce'
        // AudioSource.PlayClipAtPoint(crack, transform.position);
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            breakableCount--;
            Debug.Log("BreakableBricks: " + breakableCount.ToString());
            Destroy(gameObject);
            levelManager.BrickDestroyed();
            ScoreManager.scoreCount = ScoreManager.scoreCount + (hitSprites.Length + 1) * 100;
            SpawnSmoke();
        }
        else
        {
            ScoreManager.scoreCount = ScoreManager.scoreCount + 100;
            LoadSprites();
        }
    }

    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex]) {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else
        {
            Debug.Log("Trying to load non-existant sprite!!");
        }
    }

    void SpawnSmoke()
    {
        GameObject newSmoke = Instantiate(smoke, this.transform.position, Quaternion.identity);
        var nsMain = newSmoke.GetComponent<ParticleSystem>().main;
        nsMain.startColor = this.GetComponent<SpriteRenderer>().color;
    }
}
