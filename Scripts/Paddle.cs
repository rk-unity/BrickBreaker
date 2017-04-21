using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public bool autoPlay = false;

    Ball ball;

    // Use this for initialization
    void Start() {
        ball = (Ball)GameObject.FindObjectOfType(typeof(Ball));
    }

    void Update()
    {
        if (!Pause.paused)
        {
            if (!autoPlay)
            {
                MoveWithMouse();
            }
            else
            {
                AutoPlay();
            }
        }
    }

    // Update is called once per frame
    void MoveWithMouse () {
        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, this.transform.position.z);

        float mousePosBlocks = Input.mousePosition.x / Screen.width * 16;

        // Clamps the 1st value between 2nd and 3rd arguments.
        //Debug.Log(Mathf.Clamp(mousePosBlocks, 0.5f, 15.5f));
        paddlePos.x = Mathf.Clamp(mousePosBlocks, 0.5f, 15.5f);

        this.transform.position = paddlePos;
    }

    void AutoPlay()
    {
        //Debug.Log("AutoPlay is Enabled");
        Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0.0f);
        Vector3 ballPos = ball.transform.position;
        paddlePos.x = Mathf.Clamp(ballPos.x, 0.5f, 15.5f);
        this.transform.position = paddlePos;
    }

}
