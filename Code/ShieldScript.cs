using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "missile") //if a missile hits the shield, the missile is destroyed. MAYBE ADD THIS TO THE MISSILE SCRIPT
        {
            Destroy(collision.gameObject);
        }
    }
}
