using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public OVRInput.Controller controller;
    public VRTeleporter teleporter;
    public GameplayController gameplayController;
    public bool rightHand;

    public bool primaryTouched = false;
    public bool primaryDown = false;
    public bool previousPrimaryTouched = false;
    public bool previousPrimaryDown = false;

    public float handTriggerState;
    private bool grabbingObject = false;
    private bool grabbingCore = false;
    private GameObject grabbedObject;

    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Button") && gameplayController.stage == 4)
        {
            gameplayController.stage++;
        }

        if (other.CompareTag("Button") && gameplayController.stage == 8)
        {
            gameplayController.stage++;
        }

        if (other.CompareTag("Interactable") || other.CompareTag("Core"))
        {
            if (handTriggerState > 0.9f && !grabbingObject)
            {
                if (other.CompareTag("Core"))
                {
                    grabbingCore = true;
                }
                else
                {
                    grabbingCore = false;
                }
                Grab(other.gameObject);
            }
        }

    }

    void Grab(GameObject obj)
    {
        grabbingObject = true;
        grabbedObject = obj;
        grabbedObject.transform.parent = transform;

        if (grabbingCore)
        {
            if (rightHand)
            {
                grabbedObject.transform.localPosition = new Vector3(-0.6187326f, -0.1650915f, -0.03062823f);
                grabbedObject.transform.localEulerAngles = new Vector3(-35.969f, -7.807f, -75.728f);
            }
            else
            {
                grabbedObject.transform.localPosition = new Vector3(0.5849947f, -0.2146529f, -0.09517444f);
                grabbedObject.transform.localEulerAngles = new Vector3(54.884f, -20.437f, 60.327f);
                //grabbedObject.transform.localPosition = new Vector3(-0.5839626f, -0.1678392f, -0.002085599f);
                //grabbedObject.transform.localEulerAngles = new Vector3(-65.50201f, -29.101f, -59.123f);
            }
        }
        else
        {
            if (rightHand)
            {
                grabbedObject.transform.localPosition = new Vector3(-0.3937587f, -0.1522766f, 0.01180603f);
                grabbedObject.transform.localEulerAngles = new Vector3(-9.032001f, -170.619f, 73.193f);
            }
            else
            {
                grabbedObject.transform.localPosition = new Vector3(0.001604551f, -0.04487047f, -0.01250178f);
                grabbedObject.transform.localEulerAngles = new Vector3(-142.882f, -18.38199f, -65.75198f);
                //grabbedObject.transform.localPosition = new Vector3(-0.5839626f, -0.1678392f, -0.002085599f);
                //grabbedObject.transform.localEulerAngles = new Vector3(-65.50201f, -29.101f, -59.123f);
            }
        }

        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Release()
    {
        grabbedObject.transform.parent = null;

        Rigidbody rigidbody = grabbedObject.GetComponent<Rigidbody>();

        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        
        Vector3 controllerVelocity = OVRInput.GetLocalControllerVelocity(controller);
        rigidbody.velocity = new Vector3(-controllerVelocity.x, controllerVelocity.y, -controllerVelocity.z);

        grabbingObject = false;
        grabbingCore = false;
        grabbedObject = null;
    }

    // Update is called once per frame
    void Update () {

        handTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

        if (rightHand)
        {
            primaryTouched = OVRInput.Get(OVRInput.Touch.One, controller);
            primaryDown = OVRInput.Get(OVRInput.Button.One, controller);
            //print("Touched: " + primaryTouched + "Presssed: " + primaryDown);

            if (primaryTouched)
            {
                teleporter.ToggleDisplay(false);
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

        if (grabbingObject)
        {
            if (handTriggerState < 0.9f)
            {
                Release();
            }
        }

    }
}
