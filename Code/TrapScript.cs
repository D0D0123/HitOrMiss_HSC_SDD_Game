using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO THE TRAP

    GameObject player;
    float timer = 0;
    bool fadebool = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Timer(7)); //Sets a timer for 7 seconds, after which the trap visually fades away
        Destroy(gameObject, 10); //Destroys the trap after 10 seconds 
	}
	
	// Update is called once per frame
	void Update () {
        if (fadebool == true)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color -= new Color(0, 0, 0, 0.3f * Time.deltaTime); //once the timer ends, the alpha portion of the colour is subtracted until the trap becomes invisible
        } 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //when the player enters the collider of the trap, their movement speed is slowed to 0.05
        {
            player.GetComponent<PlayerMovement>().playerspeed = 0.05f;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //when the player exits the collider of the trap, their movement speed is sped up back to 0.2
        {
            player.GetComponent<PlayerMovement>().playerspeed = 0.2f;
        }
    }

   IEnumerator Timer(float waittime)
    {
        yield return new WaitForSeconds(waittime); //just waits a given amount of time, then makes the trap fade away in the update script
        fadebool = true;
    }

}
