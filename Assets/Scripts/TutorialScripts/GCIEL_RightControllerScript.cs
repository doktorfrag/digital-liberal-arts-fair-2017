using UnityEngine;
using VRTK;

//Right controller in charge of interacting with objects in tutorial
public class GCIEL_RightControllerScript : MonoBehaviour
{
    //private class variables
    private bool _sphereTargeted = false;
    private GameObject _targetSphere;

    private void Start()
    {
        if (GetComponent<VRTK_DestinationMarker>() == null)
        {
            Debug.LogError("GCIEL_RightControllerScript is required to be attached to a Controller that has the `VRTK_DestinationMarker` script attached to it");
            return;
        }

        if (GetComponent<VRTK_ControllerEvents>() == null)
        {
            Debug.LogError("GCIEL_RightControllerScript is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
            return;
        }

        //Setup controller event listeners
        GetComponent<VRTK_DestinationMarker>().DestinationMarkerEnter += new DestinationMarkerEventHandler(DoPointerIn);
        GetComponent<VRTK_DestinationMarker>().DestinationMarkerExit += new DestinationMarkerEventHandler(DoPointerOut);
        GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
    }

    //Event handlers for interactring with sphere objects
    //In future: move logic to SphereScript.cs
    private void DoPointerIn(object sender, DestinationMarkerEventArgs e)
    {
        if(e.target.transform.tag == "Sphere")
        {
            _sphereTargeted = true;
            _targetSphere = e.target.transform.gameObject;

        }

    }

    private void DoPointerOut(object sender, DestinationMarkerEventArgs e)
    {
        _sphereTargeted = false;
        _targetSphere = null;
    }

    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (_sphereTargeted)
        {
            SphereScript _sphere = _targetSphere.GetComponent<SphereScript>();
            _sphere.DestroySphere();
            _sphere = null;
        }
    }

    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        _targetSphere = null;
        _sphereTargeted = false;
    }
}