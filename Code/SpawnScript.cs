using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO AN EMPTY GAMEOBJECT CALLED 'SPAWNER' IN THE LEVEL - IT SPAWNS DIFFERENT ENEMIES AND POWERUPS 

    int xcord;
    int ycord;
    public float spawnrate;
    public GameObject missilespawner;
    public GameObject player;
    public GameObject powerup;
    public GameObject bomb;
    public GameObject laser;
    public GameObject assassin;
    public GameObject trap;
    public GameObject spinner;
    public GameObject meteor;
    public GameObject[] enemies;
    GameObject[] spawnercount;
    GameObject[] powerupcount;
    //The following booleans are used so that the coroutines below don't keep executing continuously
    bool powerupbool;
    bool bombbool;
    bool shooterbool;
    bool laserbool;
    bool assassinbool;
    bool trapbool;
    bool spinnerbool;
    bool meteorbool;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        powerupbool = false;
        bombbool = false;
        shooterbool = false;
        laserbool = false;
        assassinbool = false;
        trapbool = false;
        spinnerbool = false;
        meteorbool = false;
        spawnrate = 4;

        enemies = new GameObject[] { missilespawner, bomb, laser, assassin, trap, spinner, meteor };
        
        
    }
	
	// Update is called once per frame
	void Update () {
        spawnercount = GameObject.FindGameObjectsWithTag("spawner"); //finds all missile spawners in scene, stores them in an array which can be counted
        powerupcount = GameObject.FindGameObjectsWithTag("powerup");
        if (powerupcount.Length == 0 && powerupbool == false) //spawns powerup if there are no powerups in the level, so the player doesn't get too powerful
        {
            StartCoroutine(SpawnPowerup(5));
            
        }

        if (bombbool == false) { StartCoroutine(SpawnBomb(Random.Range(20, 30))); } //spawns a bomb every 20 to 30 seconds

        if (spawnrate > 1)
        {
            spawnrate = 4 - ((player.GetComponent<PlayerMovement>().score) * 0.05f);
        }
        else //ensures spawnrate doesn't get too low, or the game becomes impossible
        {
            spawnrate = 1;
        }
         //The time between spawns of missile shooters decreases proportional to the score - this creates a progressive difficulty 
        Debug.Log("SPAWNRATE" + spawnrate);
        

        if (spawnercount.Length <= 10 && shooterbool == false) //ensures that too many missile spawners don't spawn.
        {
            StartCoroutine(SpawnShooter(spawnrate));
        }

        if (laserbool == false)
        {
            StartCoroutine(SpawnLaser(Random.Range(15,20))); //spawns a laser every 15 to 20 seconds
        }

        if (assassinbool == false)
        {
            StartCoroutine(SpawnAssassin(Random.Range(15, 20))); 
        }

        if (trapbool == false)
        {
            StartCoroutine(SpawnTrap(Random.Range(10, 20)));
        }

        if (spinnerbool == false)
        {
            StartCoroutine(SpawnSpinner(Random.Range(10, 20)));
        }

        if (meteorbool == false)
        {
            StartCoroutine(SpawnMeteor(Random.Range(15, 20)));
        }

    }

   
//All of the following generate a random x and y coordinate within a certian range, then spawn their respective enemies
    IEnumerator SpawnShooter(float waittime) 
    {
        shooterbool = true; //this bool ensures that the coroutine isn't continuously started in the update function
        yield return new WaitForSeconds(waittime);
        xcord = Random.Range(-17, 17);
        ycord = Random.Range(-10, 10);
        Instantiate(enemies[0], new Vector2(xcord, ycord), Quaternion.identity);
        shooterbool = false;


    }

    IEnumerator SpawnPowerup(float waittime)
    {
        powerupbool = true; 
        yield return new WaitForSeconds(waittime);
        xcord = Random.Range(-17, 17);
        ycord = Random.Range(-10, 10);
        Instantiate(powerup, new Vector2(xcord, ycord), Quaternion.identity);
        powerupbool = false;

    }

    IEnumerator SpawnBomb(float waittime)
    {
        bombbool = true;
        yield return new WaitForSeconds(waittime);
        xcord = Random.Range(-17, 17);
        ycord = Random.Range(-10, 10);
        Instantiate(enemies[1], new Vector2(xcord, ycord), Quaternion.identity);
        bombbool = false;
    }

    IEnumerator SpawnLaser(float waittime)
    {
        laserbool = true;
        yield return new WaitForSeconds(waittime);
        xcord = Random.Range(-17, 17);
        ycord = Random.Range(-10, 10);
        Instantiate(enemies[2], new Vector2(xcord, ycord), Quaternion.identity);
        laserbool = false;
    }

    IEnumerator SpawnAssassin(float waittime)
    {
        assassinbool = true;
        yield return new WaitForSeconds(waittime);
        xcord = Random.Range(-17, 17);
        ycord = Random.Range(-10, 10);
        Instantiate(enemies[3], new Vector2(xcord, ycord), Quaternion.identity);
        assassinbool = false;
    }

    IEnumerator SpawnTrap(float waittime)
    {
        trapbool = true;
        yield return new WaitForSeconds(waittime);
        for (int i = 0; i < 3; i++)
        {
            xcord = Random.Range(-17, 17);
            ycord = Random.Range(-10, 10);
            Instantiate(enemies[4], new Vector2(xcord, ycord), Quaternion.identity);
        }
        
        trapbool = false;
    }

    IEnumerator SpawnSpinner(float waittime)
    {
        spinnerbool = true;
        yield return new WaitForSeconds(waittime);
        Instantiate(enemies[5], new Vector2(-22, 0), Quaternion.identity);
        spinnerbool = false;
    }

    IEnumerator SpawnMeteor(float waittime)
    {
        meteorbool = true;
        yield return new WaitForSeconds(waittime);
        for (int i = 0; i < 4; i++)
        {
            xcord = Random.Range(-17, 17);
            Instantiate(enemies[6], new Vector2(xcord, 12), Quaternion.identity);
        }
        meteorbool = false;
    }
    
}
