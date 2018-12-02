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

    public float handTriggerState;
    private bool grabbingObject = false;
    private GameObject grabbedObject;

    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Button") && gameplayController.stage == 4)
        {
            gameplayController.stage++;
        }

        if (other.CompareTag("Interactable"))
        {
            if (handTriggerState > 0.9f && !grabbingObject)
            {
                Grab(other.gameObject);
            }
        }

    }

    void Grab(GameObject obj)
    {
        grabbingObject = true;
        grabbedObject = obj;
        grabbedObject.transform.parent = transform;

        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Release()
    {
        grabbedObject.transform.parent = null;

        Rigidbody rigidbody = grabbedObject.GetComponent<Rigidbody>();

        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;

        rigidbody.velocity = OVRInput.GetLocalControllerVelocity(controller);

        grabbingObject = false;
        grabbedObject = null;
    }

    // Update is called once per frame
    void Update () {
        primaryTouched = OVRInput.Get(OVRInput.Touch.One, controller);
		primaryDown = OVRInput.Get(OVRInput.Button.One, controller);
        handTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
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

        if (grabbingObject)
        {
            if (handTriggerState < 0.9f)
            {
                Release();
            }
        }

    }
}
