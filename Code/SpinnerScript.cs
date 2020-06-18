using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour {//THIS SCRIPT IS ATTACHED TO THE SPINNER

    Rigidbody2D rb;
    Vector2 startpos;
    float StartTime; 
    float wavelength; 
    float amplitude;
    float frequency;


	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        startpos = transform.position;
        Destroy(gameObject, 7);
        StartTime = Time.time; //the time when the spinner was spawned
        frequency = Random.Range(5, 8);
        amplitude = Random.Range(3, 8);
        wavelength = Random.Range(15, 20);



    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0,-720) * Time.deltaTime); //rotates the spinner rapidly
        
    }

    private void FixedUpdate()
    {
        float objectSpawnTime = Time.time - StartTime; //gets time since the spinner was spawned

        rb.MovePosition(startpos + new Vector2(wavelength * objectSpawnTime, amplitude * Mathf.Sin(objectSpawnTime * frequency))); //the spinner moves as a sine wave, with wavelength, amplitude and frequency being the three randomised parameters
    }

    private void OnTriggerEnter2D(Collider2D collision) //the player incurs a hit if they collide with the spinner
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().hits += 1;
            StartCoroutine(Camera.main.GetComponent<CameraController>().CamShake(0.2f, 0.2f));
        }
        
    }


}
