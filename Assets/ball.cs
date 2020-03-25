using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball : MonoBehaviour
{
    //current time and bestTime 
    float time, bestTime;
    public Rigidbody2D body;
    Vector2 moveDirection;

    //for textlabel style
    GUIStyle style = new GUIStyle();
    //speed of ball

    float speed = 70f;

    // Start is called before the first frame update
    void Start()
    {
        //get bestTime from storage on start
        bestTime = PlayerPrefs.GetFloat("bestTime", 0);
        style.normal.textColor = Color.white;
        style.fontSize = 20;
    }

    private void OnGUI()
    {
        //create labels for current time and best time
        GUI.Label(new Rect(50, 10, 200, 50), "Time : " + time, style);
        GUI.Label(new Rect(50, 30, 200, 50), "Best : " + bestTime, style);
    }

    // Update is called once per frame
    void Update()
    {
        //start current time
        time += Time.deltaTime;

        //get Input for horizontal and vertical movement and set it in moveDirection
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection.x = horizontal;
        moveDirection.y = vertical;

        //for movement, direction into speed
        transform.Translate(moveDirection * speed * Time.deltaTime);

        //Change gravity on both x and y axis randomly
        Physics2D.gravity = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if collided by MainCamera
        if(collision.tag == "MainCamera")
        {
            //check if current time is better than previous bestTime
            if(time > bestTime)
            {
                PlayerPrefs.SetFloat("bestTime", time);
            }

            //Load the scene again to restart
            SceneManager.LoadScene("SampleScene");
        }
    }
}
