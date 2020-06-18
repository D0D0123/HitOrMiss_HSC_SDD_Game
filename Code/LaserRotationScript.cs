using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotationScript : MonoBehaviour {

    //public Transform target;
    private Rigidbody2D rb;
    Transform target;
    public int rotatespeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 direction = (Vector2)target.position - rb.position; //subtracts the vector of the laser from the vector of the target to get a direction vector
        direction.Normalize(); //normalises the vector so it always has a magnitude of 1
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        //calculates how much the laser has to rotate to target the player, by cross multiplying the vectors and producing an orthogonal vector in the third dimension. If the third vector is inwards, it rotates right, and vice versa.
        rb.angularVelocity = -rotateAmount * rotatespeed; //rotates the laser

        if (GameObject.FindGameObjectWithTag("powerup") != null) //checks if there is a powerup in the level
        {
            if (GameObject.FindGameObjectWithTag("powerup").GetComponent<PowerUpScript>().decoybool == true) //if the decoy powerup is active, the laser will aim at the decoy
            {
                target = GameObject.FindGameObjectWithTag("decoy").transform;
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }

        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform; //if the decoy powerup isn't active, the laser will aim at the player
        }

    }
}
