using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public Text logo;
    LevelManager levelManager;

    IEnumerator fade()
    {
        Debug.Log("OnCoroutine: Entered");
        while (true)
        {
            Debug.Log("OnCoroutine: " + (int)Time.time);

            Color modified = logo.color;
            modified.r = logo.color.r - .1f;
            modified.g = logo.color.g - .05f;

            if (modified.g <= 0)
            {
                levelManager.LoadLevel("Start Menu");
            }

            logo.color = new Color(Mathf.Abs(modified.r), Mathf.Abs(modified.g), Mathf.Abs(modified.b), modified.a);
            yield return new WaitForSeconds(1.0f/60.0f);
        }
        //Debug.Log("OnCoroutine: Exited");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Fader starting..");
        logo = (Text)GameObject.FindObjectOfType(typeof(Text));
        levelManager = (LevelManager)FindObjectOfType(typeof(LevelManager));
        StartCoroutine(fade());
    }

}
