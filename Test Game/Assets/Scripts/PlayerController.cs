using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private int count;

    public Text countText;

    public Text winText;

    public GameObject projectile;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        //Initialize count to zero.
        count = 0;
        //Initialze winText to a blank string since we haven't won yet at beginning.
        winText.text = "";
        SetCountText();

    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");
        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);

        // If the player tries to rotate
        float rotate = Input.GetAxis("Rotate");
        rb2d.AddTorque(rotate * speed);

        // If the player tries to shoot
        bool shoot = Input.GetKeyDown(KeyCode.Space);
        if (shoot)
        {
            var proj = Instantiate(projectile, transform.position, Quaternion.Euler(transform.eulerAngles));
            proj.GetComponent<Rigidbody>().velocity = proj.transform.forward * 6;
            Destroy(proj, 2.0f);
        }
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            winText.text = "You lose!";
            gameObject.SetActive(false);
        }
    }

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    void SetCountText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        countText.text = "Count: " + count.ToString();

        //Check if we've collected all 12 pickups. If we have...
        if (count >= 9)
            //... then set the text property of our winText object to "You win!"
            winText.text = "You win!";
    }
}
