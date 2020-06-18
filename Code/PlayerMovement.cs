using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour { //THIS SCRIPT IS ATTACHED TO THE PLAYER

    public Rigidbody2D rb2;
    public int score;
    public float playerspeed = 0.2f;
    public Text ScoreText;
    public Text AbilityText;
    public Image healthbar;
    public GameObject GameOver;
    public float hits = 0;
    public int closecount = 0;
    GameObject[] missiles;
    public Text MissesText;
    public Text HitsText;
    public Text FinalScore;
    public Text HighScore;
    bool pausebool = false;
    public ParticleSystem explosion;
    
    


	// Use this for initialization
	void Start () {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        GameOver.SetActive(false); //gameover screen is deactivated at start
        Camera.main.backgroundColor = Random.ColorHSV(0, 1, 0.4f, 0.4f, 0.9f, 0.9f); //starts the game with a random background colour
        HighScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        // these are the controls
        Vector2 movedelta = Vector2.zero; //accumulates all the inputs in movedelta, then moves the player in that direction - this is better than adding forces as it makes the controls more snappy 
        
        if (Input.GetAxisRaw("Vertical") == 1/*GetKey(KeyCode.W)*/) { movedelta += new Vector2(0, playerspeed); }//rb2.AddForce(Vector2.up); }

            else if (Input.GetAxisRaw("Vertical") == -1/*GetKey(KeyCode.S)*/) { movedelta += new Vector2(0, -playerspeed); }//rb2.AddForce(Vector2.down); }

        if (Input.GetAxisRaw("Horizontal") == 1/*GetKey(KeyCode.D)*/) { movedelta += new Vector2(playerspeed, 0); }//rb2.AddForce(Vector2.right); }

            else if (Input.GetAxisRaw("Horizontal") == -1/*GetKey(KeyCode.A)*/) { movedelta += new Vector2(-playerspeed, 0); }//rb2.AddForce(Vector2.left); }

        rb2.MovePosition(rb2.position + movedelta);


        if (Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.R)) //pressing R restarts the game
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
            Debug.Log(Time.timeScale);
            
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pausebool = !pausebool;
            if (pausebool == true) { Time.timeScale = 0; }
            else Time.timeScale = 1;
        }

        ScoreText.text = score.ToString(); //displays the score as a number on the UI canvas

        if (hits >= 10) //if the player is hit enough times, the gameover screen is shown
        {
            GameOver.SetActive(true);
            FinalScore.text = score.ToString();
            /*GameObject[] everything = FindObjectsOfType(GameObject);
            foreach (GameObject obj in everything)
            {

            }*/
            Time.timeScale = 0;
            
        }

        missiles = GameObject.FindGameObjectsWithTag("missile"); //stores all the missiles in an array 

        if (closecount >= 5) //if the player gets enough close calls, they can use an ability where all the missiles in the scene are destroyed
        {

            AbilityText.color = new Vector4(1, 1, 1, 1);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                closecount -= 5;
                foreach (GameObject missile in missiles)
                {
                    Destroy(missile);
                    Instantiate(explosion, missile.transform.position, Quaternion.identity);
                    
                }
            }
        }

        if (closecount < 5)
        {
            AbilityText.color = new Vector4(0,0,0,0);
        }
        //displays the misses and hits as text on the UI canvas
        MissesText.text = "Misses: " + closecount.ToString(); 
        HitsText.text = "Hits: " + ((int)hits).ToString();

        if (score > PlayerPrefs.GetInt("HighScore", 0)) //if the current score is greater than the previous high score, then the new high score is saved
        {
            PlayerPrefs.SetInt("HighScore", score);
            HighScore.text = "HIGHSCORE: " + score.ToString();
        }

        if (GameObject.FindGameObjectWithTag("trap") == null) //just in case the trap disappears while the player is still in it's trigger, this automatically reverts the player's speed back to normal
        {
            playerspeed = 0.2f;
        }


    }




    private void OnBecameInvisible() //if the player leaves the screen, they are teleported back to the centre
    {
        gameObject.transform.position = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision) //if the player hits a missile, the number of hits increases by 1
    {
        if (collision.gameObject.tag == "missile")
        {
            hits += 1;
            StartCoroutine(Camera.main.GetComponent<CameraController>().CamShake(0.1f, 0.2f)); //shakes the camera after getting hit
            
        }



        
    }
}
