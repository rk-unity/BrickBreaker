using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public static int scoreCount = 0;
    static int highscoreCount = 0;

    Text highscoreText;
    Text scoreText;

    float iteration = 0;
    enum iterateColor { red = 0, green, blue };
    int colorState;
    float frequency;
    float red;
    float green;
    float blue;
    float alpha;

    // Use this for initialization
    void Start () {

        if (GameObject.FindGameObjectWithTag("Highscore")) 
            highscoreText = GameObject.FindGameObjectWithTag("Highscore").GetComponent<Text>();

        if (GameObject.FindGameObjectWithTag("Score"))
            scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();

        colorState = (int)iterateColor.red;
        frequency = Mathf.PI * 2;
        red = Mathf.Sin(frequency * iteration + 0) * (127) + 128;
        green = Mathf.Sin(frequency * iteration + 1) * (127) + 128;
        blue = Mathf.Sin(frequency * iteration + 3) * (127) + 128;
        alpha = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (scoreText) 
            scoreText.text = "Score: " + scoreCount.ToString();
    }

    void ColorCycle()
    {
        switch (colorState)
        {
            case (int)iterateColor.red:
                //Debug.Log("Changing RGBA: R");
                red = Mathf.Sin(frequency * iteration + 1);

                if (Mathf.Abs(red) >= 0.9f)
                {
                    colorState = (int)iterateColor.green;
                }
                break;

            case (int)iterateColor.green:
                //Debug.Log("Changing RGBA: G");
                green = Mathf.Sin(frequency * iteration + 3);

                if (Mathf.Abs(green) >= 0.9f)
                {
                    colorState = (int)iterateColor.blue;
                }
                break;

            case (int)iterateColor.blue:
                //Debug.Log("Changing RGBA: B");
                blue = Mathf.Sin(frequency * iteration + 5);

                if (Mathf.Abs(blue) >= 0.9f)
                {
                    colorState = (int)iterateColor.red;
                }
                break;
        }
    }

    public void FixedUpdate()
    {
        if (scoreCount > highscoreCount)
        {
            highscoreCount = scoreCount;
        }

        iteration = iteration + .0025f;
        ColorCycle();

        if (highscoreText)
        {
            highscoreText.text = "Highscore: " + highscoreCount.ToString();
            highscoreText.color = new Color(Mathf.Abs(red), Mathf.Abs(green), Mathf.Abs(blue), alpha);
            Debug.Log(highscoreText.color.ToString());
        }

        //Debug.Log(SceneManager.GetActiveScene().name.ToString());
        if (scoreText && SceneManager.GetActiveScene().name == "GameOver Screen" || SceneManager.GetActiveScene().name == "Win Screen")
        {
            scoreText.text = "Score: " + highscoreCount.ToString();
            scoreText.color = new Color(Mathf.Abs(red), Mathf.Abs(green), Mathf.Abs(blue), alpha);
            Debug.Log(scoreText.color.ToString());
        }

    }
}
