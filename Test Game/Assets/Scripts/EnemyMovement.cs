using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public GameObject target;           //Store the player sprite, which the enemy will chase

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>(); // gets the rigidbody2d of the enemy
    }
	
	// FixedUpdate in order to calculate right before movement
	void FixedUpdate () {
        Vector3 targetCenter = target.transform.position;
        Vector3 selfCenter = transform.position; // gets the center of the object
        Vector3 movement = (targetCenter - selfCenter).normalized;
        rb2d.AddForce(movement * speed);
    }
}
