using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text

public class TimeManager : MonoBehaviour {
    Text timeText;
    
    // Use this for initialization
    void Start () {
        timeText = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        timeText.text = "Time: " + Mathf.Ceil(Time.timeSinceLevelLoad);
    }
}
