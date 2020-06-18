using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO THE BOMB

    public GameObject square;
    public GameObject line; //square and line are the two physical components of this gamobject, they are seperate because the square needs to be scaled seperately
    public GameObject missile;
    public float bombtimer = 0;
    public ParticleSystem explosion;
    public AudioSource blast;
    public AudioSource tick;

	// Use this for initialization
	void Start () {
        tick.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (bombtimer < 5) //the time increases until 5 seconds (like a countdown)
        {
            
            bombtimer += Time.deltaTime; 
            square.transform.localScale = new Vector3(bombtimer * 0.4f, bombtimer * 0.4f, 0); //the square within the bomb gets bigger to visually show the time passing
            
        }

        if (bombtimer >= 5)
        {
            blast.Play();
            SpawnMissiles(); 
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            StartCoroutine(Camera.main.GetComponent<CameraController>().CamShake(0.1f, 0.4f));
            Destroy(square.GetComponent<SpriteRenderer>());
            Destroy(line.GetComponent<SpriteRenderer>());
            Destroy(gameObject.GetComponent<BoxCollider2D>()); 
            Destroy(gameObject.GetComponent<BombScript>()); //destroys the script so the loop for spawning missiles doesn't execute in the next frame
            Destroy(gameObject, 1f);
        }



	}

    void SpawnMissiles()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(missile, gameObject.transform.position, Quaternion.identity); //spawns 10 missiles

        }
    }
}
