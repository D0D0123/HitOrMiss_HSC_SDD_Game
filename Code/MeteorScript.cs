using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO THE METEOR

    
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 3; //sets gravityscale for the meteors, which unity uses to automatically add a force in the downward direction (so the meteors fall)
        Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if a meteor collides with the player they incur a hit 
        {
            collision.gameObject.GetComponent<PlayerMovement>().hits += 1;
            StartCoroutine(Camera.main.GetComponent<CameraController>().CamShake(0.2f, 0.2f)); //camera shake
        }
    }
}
