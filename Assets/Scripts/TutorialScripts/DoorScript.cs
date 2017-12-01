using UnityEngine;

public class DoorScript : MonoBehaviour {

    //private variables
    private Transform _objTransform = null;
    private float _startTime;
    private float _distanceToTravel = 0.0f;
    private bool _isDoorOpen = false;
    private Vector3 _posDoorStart;
    private AudioSource _doorSound;
    private Collider _sceneLoadTrigger;

    //public variables
    public Vector3 posDoorEnd;
    public float doorSmoothing = 0.5f;

    //locate trigger in LoadNewSceneBox object
    //new scene is loaded when player is over this collider
    private void Awake()
    {
        _sceneLoadTrigger = GameObject.Find("SceneLoadTrigger").GetComponent<Collider>();
        _sceneLoadTrigger.enabled = false;
    }

    //get position for door and load sound resource
    void Start () {
        _objTransform = transform;
        _posDoorStart = transform.localPosition;
        _doorSound = GetComponent<AudioSource>();
    }
	
	void Update () {
		if(_objTransform != null)
        {
            //move door
            if (_isDoorOpen)
            {
                float distCovered = (Time.time - _startTime) * doorSmoothing;
                float smoothing = distCovered / _distanceToTravel;
                transform.localPosition = Vector3.Lerp(_posDoorStart, posDoorEnd, smoothing);
            }
        }
	}

    //is called at start of Stage 8 of tutorial training
    public void OpenDoor()
    {
        _sceneLoadTrigger.enabled = true;
        _startTime = Time.time;
        _isDoorOpen = true;
        posDoorEnd = new Vector3(_posDoorStart.x, _posDoorStart.y + 1.9f, _posDoorStart.z);
        _distanceToTravel = Vector3.Distance(posDoorEnd, _posDoorStart);
        _doorSound.Play();
    }
}