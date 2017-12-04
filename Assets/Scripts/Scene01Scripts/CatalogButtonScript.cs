using UnityEngine;
using UnityEngine.UI;
using VRTK;
using VRTK.GrabAttachMechanics;

public class CatalogButtonScript : MonoBehaviour {

    //public variables
    public string roomNumber;
    public string resourceTitle;
    public bool resourceDisplayed;
    public string resourceDescription;
    public string filePath;

    //private variables
    public Button _thisButton;
    private UI_CatalogManager _catalogList;
    private Vector3 _room1pos;
    private Vector3 _room2pos;
    private string _dataPath;

    // methods
    void Start ()
    {
        //get data path
        _dataPath = Application.dataPath;

        //populate catalog
        _thisButton = gameObject.GetComponent(typeof(Button)) as Button;
        _thisButton.GetComponentInChildren<Text>().text = resourceTitle;
        _thisButton.onClick.AddListener(HandleClick);

        //get position of rooms for later art instantiation
        GameObject room1 = GameObject.Find("Room 1");
        GameObject room2 = GameObject.Find("Room 2");
        _room1pos = room1.transform.position;
        _room2pos = room2.transform.position;
    }

    void HandleClick()
    {
        //update main catalog list in GameController.cs
        GameController.Instance.UpdateCatalog(roomNumber, resourceTitle);

        //update roopm catalog display
        _catalogList = gameObject.GetComponentInParent<UI_CatalogManager>();
        _catalogList.RefreshMenu();

        //instantiate artwork in room
        //Debug.Log("Just instantiated " + resourceTitle + " in " + roomNumber + ". " + "Art description: " + resourceDescription);
        string artPath = "Exhibition Rooms/" + roomNumber + "/" + resourceTitle;
        Debug.Log(artPath);
        GameObject art = Instantiate(Resources.Load(artPath)) as GameObject;
        art.AddComponent<VRTK_InteractableObject>();
        art.AddComponent<VRTK_FixedJointGrabAttach>();
        art.AddComponent<Rigidbody>();
        art.AddComponent<PictureScript>();

        //set options in VRTK_InteractableObject
        art.GetComponent<VRTK_InteractableObject>().disableWhenIdle = true;
        art.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
        art.GetComponent<VRTK_InteractableObject>().stayGrabbedOnTeleport = true;
        art.GetComponent<VRTK_InteractableObject>().grabAttachMechanicScript = art.GetComponent<VRTK_FixedJointGrabAttach>();

        //insert art into scene
        if(roomNumber == "Room 1")
        {
            art.transform.position = new Vector3 (_room1pos.x, _room1pos.y + 1.3f, _room1pos.z);
        }

        if (roomNumber == "Room 2")
        {
            art.transform.position = new Vector3(_room2pos.x, _room2pos.y + 1.3f, _room2pos.z);
        }
    }
}
