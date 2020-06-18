using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO AN EMPTY 'BULLETSPAWNER' GAMEOBJECT UNDER THE MISSILE SPAWNER/SHOOTER - IT SPAWNS THE ACTUAL MISSILES

    public GameObject missileprefab;
    public GameObject player;
    public GameObject missilespawner;
    public GameObject shooter;
    Transform target;


	// Use this for initialization
	void Start () {

        InvokeRepeating("LaunchMissile", 1f, 3f); //continues spawning missiles every 3 seconds

	}
	
	// Update is called once per frame
	void Update () {
        target = player.transform;
    }

    void LaunchMissile() //spawms a missile and destroys it after five seconds
    {
        var missile = Instantiate(missileprefab, missilespawner.transform.position, Quaternion.identity);
        StartCoroutine(ScaleMissileSpawner(0.2f));
        Destroy(missile, 5);
    }

    IEnumerator ScaleMissileSpawner(float waittime) //scales the missile spawner slightly larger, then smaller, to make a sort of "bubble" effect as it fires
    {
        shooter.transform.localScale += new Vector3(Mathf.Lerp(0, 1, 0.2f), Mathf.Lerp(0, 1, 0.2f), Mathf.Lerp(0, 1, 0.2f));
        yield return new WaitForSeconds(waittime);
        shooter.transform.localScale -= new Vector3(Mathf.Lerp(0, 1, 0.2f), Mathf.Lerp(0, 1, 0.2f), Mathf.Lerp(0, 1, 0.2f));
    }

}
