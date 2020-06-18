using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    void Start()
    {
        // Calculate orthographic camera bounds
        //float height = 2f * Camera.main.orthographicSize;
        //float width = height * Camera.main.aspect;
    }

    private void Update()
    {
        
    }

    public IEnumerator CamShake(float shaketime, float magnitude)
    {
        Vector3 originalpos = transform.localPosition; //stores the original position of the camera in a variable

        float elapsed = 0.0f; //this measures how much time has elapsed since this coroutine started

        while (elapsed < shaketime) //while the time passed is less than the time to go, it moves the position of the camera in a random x and y direction to make it shake
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalpos.z);

            elapsed += Time.deltaTime; //adds time to the time passed so the camera only shakes for the allocated amount of time

            yield return null; //before running the loop again, this makes the coroutine wait until the next frame is drawn in the game
        }

        originalpos = transform.localPosition; //sets the camera position back to normal after it stops shaking
    }
}
