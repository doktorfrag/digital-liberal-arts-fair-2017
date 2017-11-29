using UnityEngine;


public class TutorialManager : MonoBehaviour {
    //Notes on tutorial states

    //create enum with game state descriptions and number "PlayIntroVid01"

    //Stage 0: Tutorial ready to launch
    //Stage 1: Intro video is played
    //Stage 2: Instruction video on hand controller targeting is played
    //Stage 3: Sphere prefabs are instantiated and sphere zapping video is played
    //Stage 4: Wait for spehres to be zapped
    //Stage 5: Instruction video on hand controller transporting is played
    //Stage 6: Player transports are counted (4x)
    //Stage 7: Video for transporting into insertion chamber is played
    //Stage 8: Door to chamber is opened and floor collider for chamber room is enabled

    //private variables
    private GameObject _labBackDoor;

    //private variable accessors
    private static TutorialManager _tutorialManagerInstance;
    public static TutorialManager Instance
    {
        get { return _tutorialManagerInstance ?? (_tutorialManagerInstance = new GameObject("TutorialManager").AddComponent<TutorialManager>()); }
    }

    private void Awake()
    {
        _labBackDoor = GameObject.FindGameObjectWithTag("LabBackDoor");
        GameObject.Find("SceneLoadTrigger").GetComponent<MeshCollider>().enabled = false;
    }

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
            if(TutorialStage == 8)
            {
                DoorOpen = true;
                GameObject.Find("SceneLoadTrigger").GetComponent<MeshCollider>().enabled = true;
            }
        }
    }

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

            if(SphereCount == 4)
            {
                TutorialStage = 1;
            }
        }
    }

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
            if (TutorialStage == 6)
            {
                if(TeleportCount > 4)
                {
                    TutorialStage = 1;
                }
            }
        }
    }

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
            if(_doorOpen == true)
            {
                DoorScript _labDoorScript = _labBackDoor.GetComponent<DoorScript>();
                _labDoorScript.OpenDoor();
            }
        }
    }
}
