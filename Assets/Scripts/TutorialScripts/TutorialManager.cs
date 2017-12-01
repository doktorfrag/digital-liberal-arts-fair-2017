using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialManager : MonoBehaviour {
    //Notes on tutorial states


    //Stage 0: Tutorial ready to launch
    //Stage 1: Intro video is played
    //Stage 2: Instruction video on hand controller targeting is played
    //Stage 3: Sphere prefabs are instantiated and sphere zapping video is played
    //Stage 4: Wait for spehres to be zapped
    //Stage 5: Instruction video on hand controller transporting is played
    //Stage 6: Player transports are counted (4x)
    //Stage 7: Video for transporting into insertion chamber is played
    //Stage 8: Door to chamber is opened and floor collider for chamber room is enabled
    //Stage 9: Grinnell College Moonbase Station video is displayed

    //private variables
    private GameObject _labBackDoor;
    private Scene _scene;

    //private variable accessors
    private static TutorialManager _tutorialManagerInstance;
    public static TutorialManager Instance
    {
        get { return _tutorialManagerInstance ?? (_tutorialManagerInstance = new GameObject("TutorialManager").AddComponent<TutorialManager>()); }
    }

    //manage stage of tutorial
    private int _tutorialStage = 0;
    public int TutorialStage
    {
        get
        {
            return _tutorialStage;
        }

        set
        {
            _tutorialStage += value;
            Debug.Log("Launching Stage " + TutorialStage + " of tutorial");

            //open rear lab door if training is complete
            //in future: move this logic to DoorScript.cs
            if (TutorialStage == 8)
            {
                DoorOpen = true;
            }
        }
    }

    //keep track of spheres in tutorial room
    private int _sphereCount = 0;
    public int SphereCount
    {
        get
        {
            return _sphereCount;
        }

        set
        {
            _sphereCount += value;

            //advance tutorial stage if all spheres are zapped
            //can this be moved to SphereManager.cs?
            if (SphereCount == 4)
            {
                TutorialStage = 1;
            }
        }
    }

    //keep track of times user has teleported
    private int _teleportCount = 0;
    public int TeleportCount
    {
        get
        {
            return _teleportCount;
        }

        set
        {
            _teleportCount += value;

            //advance tutorial stage if user telports 2x
            //where can this logic be moved?
            if (TutorialStage == 6)
            {
                if(TeleportCount == 2)
                {
                    TutorialStage = 1;
                }
            }
        }
    }

    //keep trakc of door state in tutorial room
    private bool _doorOpen = false;
    public bool DoorOpen
    {
        get
        {
            return _doorOpen;
        }

        set
        {
            _doorOpen = value;

            //open door if all tutorial stages have been completed
            //in future: move all of this logic to DoorScript.cs
            if(_doorOpen == true)
            {
                _labBackDoor = GameObject.FindGameObjectWithTag("LabBackDoor");
                DoorScript _labDoorScript = _labBackDoor.GetComponent<DoorScript>();
                _labDoorScript.OpenDoor();
            }
        }
    }

    //make TutorialManager.cs persistent through all levels
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
