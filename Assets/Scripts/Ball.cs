using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GameplayController gameplayController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        Debug.Log("detected collision");
        if (other.CompareTag("Generator Core Detector") && gameplayController.stage == 6)
        {
            Debug.Log("detected detector");
            gameplayController.stage++;
        }
        
    }

}
