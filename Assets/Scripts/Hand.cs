using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public OVRInput.Controller controller;
    public VRTeleporter teleporter;

    public bool primaryTouched = false;
    public bool primaryDown = false;
    public bool previousPrimaryTouched = false;
    public bool previousPrimaryDown = false;

    // Update is called once per frame
    void Update () {
        primaryTouched = OVRInput.Get(OVRInput.Touch.One, controller);
		primaryDown = OVRInput.Get(OVRInput.Button.One, controller);
        print("Touched: " + primaryTouched + "Presssed: " + primaryDown);

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

        }

        previousPrimaryTouched = primaryTouched;
        previousPrimaryDown = primaryDown;
	}
}
