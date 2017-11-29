using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    //private variables
    private Transform _objTransform = null;
    private float _startTime;
    private float _distanceToTravel = 0.0f;
    private bool _isDoorOpen = false;
    private Vector3 _posDoorStart;
    private AudioSource _doorSound;

    //public variables
    public Vector3 posDoorEnd;
    public float doorSmoothing = 0.5f;

	void Start () {
        _objTransform = transform;
        _posDoorStart = transform.localPosition;
        _doorSound = GetComponent<AudioSource>();
    }
	
	void Update () {
		if(_objTransform != null)
        {
            if (_isDoorOpen)
            {
                float distCovered = (Time.time - _startTime) * doorSmoothing;
                float smoothing = distCovered / _distanceToTravel;
                transform.localPosition = Vector3.Lerp(_posDoorStart, posDoorEnd, smoothing);
            }
        }
	}

    public void OpenDoor()
    {
        _startTime = Time.time;
        _isDoorOpen = true;
        posDoorEnd = new Vector3(_posDoorStart.x, _posDoorStart.y + 1.9f, _posDoorStart.z);
        _distanceToTravel = Vector3.Distance(posDoorEnd, _posDoorStart);
        _doorSound.Play();
    }
}
