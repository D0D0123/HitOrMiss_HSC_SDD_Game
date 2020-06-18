using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCall : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO THE TRANSPARENT TRIANGLE AROUND THE MISSILE

    public GameObject player;
    public AudioSource Woosh;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Woosh = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ActivateCloseCall(0.2f));
            player.GetComponent<PlayerMovement>().closecount += 1; //adds 1 to the number of misses
            Woosh.Play();

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false; //only shows transparent triangle when the player enters the collider
        }
    }

    IEnumerator ActivateCloseCall(float delayTime) //shows a transparent triangle for a little while to show that a close call occurred
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(delayTime);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
