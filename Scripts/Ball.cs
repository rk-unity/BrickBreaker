using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour {
    public Paddle paddle;

    Vector3 paddleToBallVector;
    bool canStart;
    bool hasStarted;
    Vector2 lastVelocity;

    Text countDown;

    IEnumerator onCoroutine(int cycles)
    {
        
        while (cycles > 0)
        {
            Debug.Log("OnCoroutine: " + (int)Time.time);
            AudioSource audio = GameObject.FindGameObjectWithTag("CountDown").GetComponent<AudioSource>();
            countDown.text = cycles.ToString();
            audio.Play();
            cycles--;
            yield return new WaitForSeconds(1.5f);
        }
        canStart = true;
        countDown.text = "GO!";
    }

    public void Start ()
    {
        countDown = GameObject.FindGameObjectWithTag("CountDown").GetComponent<Text>();
        StartCoroutine(onCoroutine(3));
        canStart = false;
    }

    public void Restart()
    {
        hasStarted = false;
    }

    public void Reset()
    {
        if (hasStarted)
            GetComponent<Rigidbody2D>().velocity = lastVelocity;
    }

	// Use this for initialization
	void Awake () {
        paddle = (Paddle)FindObjectOfType(typeof(Paddle));
        paddleToBallVector = this.transform.position - paddle.transform.position;
        hasStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.paused)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
        else
        {
            if (!hasStarted)
            {
                this.transform.position = paddle.transform.position + paddleToBallVector;

                if (canStart)
                {
                    if (Input.GetMouseButton(0))
                    {
                        Debug.Log("Mouse Down, launching ball");
                        hasStarted = true;
                        this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);
                        countDown.text = "";
                    }
                }
            }

            // rotate our sawblade based on time
            transform.Rotate(Vector3.forward * -90 * Time.deltaTime);
            lastVelocity = this.GetComponent<Rigidbody2D>().velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 tweak = new Vector2(Random.Range(0f, 0.3f), Random.Range(0f, 0.3f)); // this will move the ball increasingly faster...
        Debug.Log("Tweak: " + tweak.ToString());
        if (hasStarted)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.volume = 0.1f;
            audio.Play();

            // basic tweak, good for human players (or not trying to get boring loop)
            this.GetComponent<Rigidbody2D>().velocity += tweak;

            // Fix velocity for "weird bounces"
            // should only take affect when collision is with paddle, better prevention of boring loops
            // note, autoPlay still hits boring loop
            if (col.gameObject.tag == "Player")
            {
                Vector2 extraTweak;
                if (this.GetComponent<Rigidbody2D>().velocity.y < 8.0f && this.GetComponent<Rigidbody2D>().velocity.y > 0.5f)
                {
                    extraTweak = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, this.GetComponent<Rigidbody2D>().velocity.y + 2.0f);
                    this.GetComponent<Rigidbody2D>().velocity = extraTweak;
                    Debug.Log("Extra Tweak <Y>: " + extraTweak.ToString());
                }
                else if (this.GetComponent<Rigidbody2D>().velocity.y < 5.0f && this.GetComponent<Rigidbody2D>().velocity.y > 0.2f)
                {
                    extraTweak = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, this.GetComponent<Rigidbody2D>().velocity.y + 5.0f);
                    this.GetComponent<Rigidbody2D>().velocity = extraTweak;
                    Debug.Log("Extra Tweak <Y>: " + extraTweak.ToString());
                }
                else if (this.GetComponent<Rigidbody2D>().velocity.y < 2.0f)
                {
                    extraTweak = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, this.GetComponent<Rigidbody2D>().velocity.y + 8.0f);
                    this.GetComponent<Rigidbody2D>().velocity = extraTweak;
                    Debug.Log("Extra Tweak <Y>: " + extraTweak.ToString());
                }
            }
        }
    }

}
