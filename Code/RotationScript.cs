using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO THE MISSILE SPAWNERS - IT IS A DUPLICATE OF THE HOMING SCRIPT, EXCEPT IT ONLY ROTATES THE MISSILE SPAWNERS
    private Rigidbody2D rb;
    Transform target;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector2 direction = (Vector2)target.position - rb.position; //subtracts the vector of the shooter from the vector of the target to get a direction vector
        direction.Normalize(); //Normalises the vector so it always has a magnitude of 1
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        //calculates how much the shooter has to rotate to target the player, by cross multiplying the vectors and producing an orthogonal vector in the third dimension. If the third vector is inwards, it rotates right, and vice versa
        rb.angularVelocity = -rotateAmount * 1000; //rotates the missile spawners to make it look like they're aiming too.

    }
}
