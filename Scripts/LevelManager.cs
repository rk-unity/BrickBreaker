using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // SceneManager
using UnityEngine.UI; // Text


public class LevelManager : MonoBehaviour {

    public int lives = 5;

    public void LoadLevel(string name){

        // special case for new game and supporting quit screen
        if (name == "Start Menu")
        {
            ScoreManager.scoreCount = 0;
        }

		Debug.Log ("New Level load: " + name);
        SceneManager.LoadScene(name);

    }

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();

        // this code isn't ran?
        Brick.breakableCount = 0;
        LoadLevel("Start Menu");
        Pause.paused = false;

    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BrickDestroyed()
    {
        // if last brick is destroyed...
        if (Brick.breakableCount <= 0)
        {          
            LoadNextLevel();
        }
    }

}
