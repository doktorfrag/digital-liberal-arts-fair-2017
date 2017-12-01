using UnityEngine.SceneManagement;
using UnityEngine;
using VRTK;

//Left controller in charge of teleporting in tutorial
public class GCIEL_LeftControllerScript : MonoBehaviour
{
    //private class variables
    private Vector3 _currentPosition;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;
    private Vector3 _targetPosition;

    private void Start()
    {

        if (GetComponent<VRTK_DestinationMarker>() == null)
        {
            Debug.LogError("GCIEL_RightControllerScript is required to be attached to a Controller that has the `VRTK_DestinationMarker` script attached to it");
            return;
        }

        if (GetComponent<VRTK_ControllerEvents>() == null)
        {
            Debug.LogError("GCIEL_LeftControllerScript is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
            return;
        }

        //Setup controller event listeners
        GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);
        GetComponent<VRTK_DestinationMarker>().DestinationMarkerEnter += new DestinationMarkerEventHandler(DoPointerIn);
    }

    private void Update()
    {
        //always find and report current position
        RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit))
        {
            _currentPosition = hit.transform.position;

            if (TutorialManager.Instance.TutorialStage == 9 && hit.transform.name == "SceneLoadTrigger")
            {
                //reset TutorialManager.cs before leaving tutorial
                int currentStage = TutorialManager.Instance.TutorialStage;
                int currentTeleportCount = TutorialManager.Instance.TeleportCount;
                int currentSphereCount = TutorialManager.Instance.SphereCount;
                TutorialManager.Instance.TutorialStage = -currentStage;
                TutorialManager.Instance.TeleportCount = -currentTeleportCount;
                TutorialManager.Instance.SphereCount = -currentSphereCount;
                TutorialManager.Instance.DoorOpen = false;
                SceneManager.LoadScene("scene_01", LoadSceneMode.Single);
                Debug.Log(TutorialManager.Instance.TutorialStage);
            }
        }
    }

    //Event handler and logic for handling teleporting
    private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        if(TutorialManager.Instance.TutorialStage == 6)
        {
            //get position after teleport
            _oldPosition = _currentPosition;
            _newPosition = _targetPosition;
            TutorialManager.Instance.TeleportCount = 1;
        }
    }

    private void DoPointerIn(object sender, DestinationMarkerEventArgs e)
    {
        _targetPosition = e.destinationPosition;
    }
}