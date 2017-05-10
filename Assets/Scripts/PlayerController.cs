using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    private int count;
    public Text countText;
    public Text timeText;
    public Text moveText;
    float timer;
    bool moveTextDisplay;
    int room;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        SetTimeText();
        timer = 240f;
        moveText.text = "";
        moveTextDisplay = false;
        room = 0;
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
//        Vector3 movement = new Vector3(Input.acceleration.x, 0, -Input.acceleration.z);

        rb.AddForce(movement * speed);
    }
    private void Update()
    {
        SetTimeText();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            if (room == 0)
            {
                gameObject.transform.localScale -= new Vector3(.05f, .05f, .05f);
                if (count == 12) moveTextDisplay = true;
            }
            if (room == 1)
            {
                gameObject.transform.localScale -= new Vector3(.035f, .035f, .035f);
                if (count == 32) moveTextDisplay = true;
            }
            if (room == 2)
            {
                gameObject.transform.localScale -= new Vector3(.021f, .021f, .021f);
                if (count == 61) moveTextDisplay = true;
            }
            if (room == 3)
            {
                gameObject.transform.localScale -= new Vector3(.017f, .017f, .017f);
                if (count == 97) moveTextDisplay = true;
            }
            count++;           
        }

        if(other.gameObject.CompareTag("reset button"))
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            other.gameObject.SetActive(false);
            moveTextDisplay = false;
            room++;
        }
        SetCountText();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (((count == 13)|| (count == 33) || (count == 62)) && moveTextDisplay == true)
        {
            moveText.text = "Move to next room.";
        }
        else if(count == 98 && timer>0)
        {
            moveText.text = "You win!";
        }
        else if (timer == 0) moveText.text = "YOU LOSE!";
        else moveText.text = "";
    }
    void SetTimeText()
    {
        timer -= Time.deltaTime;
        timeText.text = "Time left: " + timer;
        if(timer == 0)
        {
            moveText.text = "YOU LOSE!";
        }
    }

}
