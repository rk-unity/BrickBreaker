using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text

public class Pause : MonoBehaviour {

    public static bool paused;

    //public Text livesText;
    GameObject[] livesText;
    GameObject pauseMenu;
    Ball ball; // need to reset velocity
    LevelManager levelManager;

    void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        livesText = GameObject.FindGameObjectsWithTag("Lives");

        if (pauseMenu)
            pauseMenu.SetActive(false);

        ball = (Ball)FindObjectOfType(typeof(Ball));
    }

    public void unPause()
    {
        paused = false;
        pauseMenu.SetActive(false);
        ball.Reset();
    }

    // Use this for initialization
    void Start () {
        levelManager = (LevelManager)FindObjectOfType(typeof(LevelManager));
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log(levelManager.lives);
        Debug.Log(livesText);
    
        // if atleast 1
        if (livesText[0])
        {
            foreach (GameObject element in livesText)
            {
                element.GetComponent<Text>().text = "Lives: " + levelManager.lives.ToString();
            }
        } 

        // throws error on start menu I should consider making a Pause script
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = true;
            pauseMenu.SetActive(true);
        }
    }
}
