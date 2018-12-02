using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public OVRInput.Controller controller;
    public VRTeleporter teleporter;
    public GameplayController gameplayController;

    public bool primaryTouched = false;
    public bool primaryDown = false;
    public bool previousPrimaryTouched = false;
    public bool previousPrimaryDown = false;

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Detected collision");
        Debug.Log(other.CompareTag("Button"));
        Debug.Log(gameplayController.stage);
        if (other.CompareTag("Button") && gameplayController.stage == 4)
        {
            gameplayController.stage++;
        }
    }

    // Update is called once per frame
    void Update () {
        primaryTouched = OVRInput.Get(OVRInput.Touch.One, controller);
		primaryDown = OVRInput.Get(OVRInput.Button.One, controller);
        //print("Touched: " + primaryTouched + "Presssed: " + primaryDown);

        if (!previousPrimaryTouched && primaryTouched)
        {
            teleporter.ToggleDisplay(true);
        }

        if (previousPrimaryTouched && !primaryTouched)
        {
            teleporter.ToggleDisplay(false);
        }

        if (!previousPrimaryDown && primaryDown)
        {
            teleporter.Teleport();
        }

        previousPrimaryTouched = primaryTouched;
        previousPrimaryDown = primaryDown;

	}
}
