using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO ALL ENEMIES THAT THE PLAYER CAN DESTROY (I.E. BOMB, MISSILE SPAWNER, LASER) - IT DESTROYS THE ENEMIES, PRODUCES PARTICLES AND A CAMERA SHAKE, AND INCREASES THE SCORE

    GameObject player;
    public ParticleSystem explosion;
    AudioSource scoresound;

    

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
        scoresound = GameObject.Find("SoundHolder").GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null) //if the shooter spawns out of scene (i.e. the aspect ratio is too small), then it is immediately destroyed.
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            if (!sprite.isVisible) 
            {
                Destroy(gameObject);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player") //if the player hits a missile shooter, it gets destroyed, and 1 is added to the score
        {
            scoresound.Play();
            StartCoroutine(Camera.main.GetComponent<CameraController>().CamShake(0.1f, 0.2f)); //Camera Shake
            player.GetComponent<PlayerMovement>().score += 1;
            Debug.Log((player.GetComponent<PlayerMovement>().score).ToString());
            if (player.GetComponent<PlayerMovement>().score % 5 == 0)
            {
                Camera.main.backgroundColor = Random.ColorHSV(0, 1, 0.4f, 0.4f, 0.9f, 0.9f); //every five points, the colour of the background changes to make the game feel more dynamic
            }
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity); //creates an explosion where the missile shooter was destroyed
            Destroy(gameObject.GetComponent<SpriteRenderer>()); //makes the shooter invisible and untouchable before completely destroying it so that the camera shake coroutine executes. 
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject, 0.2f);
        }

        
	}

 
}
