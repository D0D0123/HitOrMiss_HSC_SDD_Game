using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingScript : MonoBehaviour { //tHIS SCRIPT IS ATTACHED TO THE MISSILES - IT ALLOWS THEM TO AIM TOWARDS AND MOVE TOWARDS THE PLAYER

    public Rigidbody2D rb;
    //public Transform target;
    Transform target;
    public float missileSpeed = 10f;
    public float missileRotationSpeed = 180f;


    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform; //sets the target as the player
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject); //destroys the missile gameobject on contact with the player
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("powerup") != null) //checks if there is a powerup in the level
        {
            if (GameObject.FindGameObjectWithTag("powerup").GetComponent<PowerUpScript>().missilespeedbool == true) //if the slowtime powerup is activated from PowerUpScript (so missilebool is true), then the velocity of the missiles is lowered
            {
                missileSpeed = 2f;
            }
            else
            {
                missileSpeed = 10f;
            }

            if (GameObject.FindGameObjectWithTag("powerup").GetComponent<PowerUpScript>().decoybool == true && GameObject.FindGameObjectsWithTag("decoy") != null) //if the decoy powerup is activated, the missiles target the decoy instead
            {
                target = GameObject.FindGameObjectWithTag("decoy").transform;
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
        
    }

    void FixedUpdate () {
        Vector2 direction = (Vector2)target.position - rb.position; //subtracts the vector of the missile from the vector of the target to get a direction vector
        direction.Normalize(); //normalises the vector so it always has a magnitude of 1
        float rotateAmount = Vector3.Cross(direction, transform.up).z; 
        //calculates how much the missile has to rotate to target the player, by cross multiplying the vectors and producing an orthogonal vector in the third dimension. If the third vector is inwards, it rotates right, and vice versa.
        rb.angularVelocity = -rotateAmount * missileRotationSpeed; //adds angular velocity to the missile to make it rotate
        rb.velocity = transform.up * missileSpeed; //adds linear velocity to the missile so it actually moves in that direction
    }
}
