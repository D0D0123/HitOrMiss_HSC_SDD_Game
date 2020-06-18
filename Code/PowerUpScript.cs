using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpScript : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO THE POWERUP GAMEOBJECT
    
    public GameObject shield;
    public GameObject decoy;
    GameObject player;
    GameObject shieldInGame;
    bool shieldbool;
    public bool missilespeedbool;
    public bool decoybool;
    float PowerUpChance;
    GameObject[] missiles;
    GameObject[] spawners;
    public ParticleSystem explosion;
    public ParticleSystem healthp;
    public Text PowerUpText;
    AudioSource powerupsound;
    AudioSource slowtimesound;
    AudioSource obliteratesound;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        missilespeedbool = false;
        decoybool = false;
        PowerUpText = GameObject.Find("PowerupText").GetComponent<Text>(); 
        PowerUpText.text = ""; //no powerups at the start, so the text is empty
        AudioSource[] audios = GetComponents<AudioSource>();
        powerupsound = audios[0];
        slowtimesound = audios[1];
        obliteratesound = audios[2];
    }
	
	// Update is called once per frame
	void Update () {

        if (shieldbool == true) //if the shield powerup is activated, the shield object in game is always superimposed on top of the player
        {
            player = GameObject.FindGameObjectWithTag("Player"); 
            shieldInGame = GameObject.FindGameObjectWithTag("shield");
            shieldInGame.transform.position = player.transform.position;
        }


        
	}

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, 5); //rotates the powerup object in the level
    }

    void DestroyVisibility() //this function makes the powerup invisible and untouchable before it fully destroys it - if it instantly was destroyed, then various coroutines wouldn't run
    {
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpChance = Random.value; //generates a random value when something collides with the powerup

        if (collision.gameObject.tag == "Player") //if the player collides with the powerup, they gain a power depending on the random number generated
        {

            

            if (PowerUpChance <= 0.25f) //30% chance 
            {
                powerupsound.Play();
                PowerUpText.text = "Powerup: Shield";
                Instantiate(shield, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity); //spawns a shield in the level
                shieldbool = true;
                StartCoroutine(DestroyShield(5f));
                DestroyVisibility();
                Destroy(gameObject, 6);
            }

            else if (PowerUpChance > 0.25f && PowerUpChance <= 0.4f) //20% chance
            {
                obliteratesound.Play();
                PowerUpText.text = "Powerup: Obliterate";
                missiles = GameObject.FindGameObjectsWithTag("missile"); //makes an array of all the missiles and another array of the missile spawners in the level
                spawners = GameObject.FindGameObjectsWithTag("spawner");

                foreach (GameObject missile in missiles) //destroys all missiles and missile spawners
                {
                    Destroy(missile);
                    Instantiate(explosion, missile.transform.position, Quaternion.identity);
                }
                foreach (GameObject spawner in spawners)
                {
                    Destroy(spawner);
                    Instantiate(explosion, spawner.transform.position, Quaternion.identity);
                    player.GetComponent<PlayerMovement>().score += 1; //adds one to the score for every spawner destroyed
                }
                DestroyVisibility();
                StartCoroutine(ShowText(3f));
                Destroy(gameObject, 4);
                
            }

            else if (PowerUpChance > 0.4f && PowerUpChance <= 0.75f) //35% chance
            {

                slowtimesound.Play();
                PowerUpText.text = "Powerup: Slow Time";
                StartCoroutine(SlowTime(7f)); 
                DestroyVisibility();
                Destroy(gameObject, 8);

            }

            else if (PowerUpChance > 0.75f && PowerUpChance <= 0.9f) //15% chance
            {
                PowerUpText.text = "Powerup: Decoy";
                Instantiate(decoy, gameObject.transform.position, Quaternion.identity); //spawns a decoy in the level
                decoybool = true;
                DestroyVisibility();
                StartCoroutine(DestroyDecoy(10));

            }

            else if (PowerUpChance > 0.9f) //10% chance
            {
                PowerUpText.text = "Powerup: Heal";
                player.GetComponent<PlayerMovement>().hits = (player.GetComponent<PlayerMovement>().hits) / 2;
                Instantiate(healthp, transform.position, Quaternion.identity);
                StartCoroutine(ShowText(3f));
                DestroyVisibility();
                Destroy(gameObject, 4);
            }



        }
    }

    
    IEnumerator DestroyShield(float waittime)
    {
        yield return new WaitForSeconds(waittime); //waits an amount of time, then destroys the shield and powerup gameobject
        Destroy(shieldInGame);
        shieldbool = false;
        Destroy(gameObject);
        PowerUpText.text = ""; 

    }

    IEnumerator SlowTime(float waittime)
    {
        missilespeedbool = true;
        yield return new WaitForSeconds(waittime); //slows time, then makes time fast again by increasing the speed of the missiles in the "Homing Script"
        missilespeedbool = false;
        PowerUpText.text = "";
    }

    IEnumerator ShowText(float waittime)
    {
        yield return new WaitForSeconds(waittime); //just waits some time so the "Powerup: Obliterate" and "Powerup: Heal" text doesn't immediately disappear
        PowerUpText.text = "";
    }

    IEnumerator DestroyDecoy(float waittime)
    {
        yield return new WaitForSeconds(waittime); //waits an amount of time then destroys the decoy and the powerup gameobject
        Destroy(GameObject.FindGameObjectWithTag("decoy")); 
        decoybool = false;
        Destroy(gameObject);
        PowerUpText.text = "";
    }



}
