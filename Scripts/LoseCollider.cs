using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoseCollider : MonoBehaviour {

    LevelManager levelManager;
    Ball ball;

    void Start()
    {
        levelManager = (LevelManager)FindObjectOfType(typeof(LevelManager));
        ball = (Ball)FindObjectOfType(typeof(Ball));
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        levelManager.lives--;
        Debug.Log("Lives Left: " + levelManager.lives);

        if (levelManager.lives <= 0)
        {
            levelManager.LoadLevel("GameOver Screen");
            // Cleanup
            Brick.breakableCount = 0; // otherwise bricks carryover
        }
        else
        {
            ball.Restart();
        }
    }

}
