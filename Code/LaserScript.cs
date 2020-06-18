using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO THE LASER LINE SPRITE

    SpriteRenderer laser_r; //This is the actual sprite of the laser
    BoxCollider2D laser_b; //This is the actual collider of the laser
    GameObject player;
    public AudioSource lasercannon;
    

	// Use this for initialization
	void Start () {
        //the laser is not activated at the start
        laser_r = gameObject.GetComponent<SpriteRenderer>();
        laser_r.enabled = false;
        laser_b = gameObject.GetComponent<BoxCollider2D>();
        laser_b.enabled = false;

        InvokeRepeating("LaserOnOff", 3, 3); //every 3 seconds, the LaserOnOff function executes

        player = GameObject.FindGameObjectWithTag("Player");
        lasercannon = gameObject.GetComponent<AudioSource>();
        
	}


    void LaserOnOff() //This function alternates the laser between active and inactive every three seconds
    {
        laser_r.enabled = !laser_r.enabled;
        laser_b.enabled = !laser_b.enabled;

        if (laser_r.enabled == true && laser_b.enabled == true)
        {
            lasercannon.Play();
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "missile") //the laser destroys all missiles it touches - the player can use this to their advantage if they are good enough
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().hits += 0.05f; //the longer the player stays in the laser, the more hits they will incur
        }
    }


}
