﻿//<remarks>
//  Developed picture meshes need to have image applied on positive "z" side of the mesh
//  In addition to this script, the picture object also needs the following VRTK scripts:
//      - VRTK_InteractibleObject
//      - VRTK_FixedJointGrabAttach
//      - VRTK_SwapControllerGrabAction
//  The picture object also needs the following components attached:
//      - Rigidbody
//      - BoxCollider
//</remarks>


using UnityEngine;
using VRTK;

public class StatueScript: MonoBehaviour
{

    //serialized variables

    //public variables
    public float fadeDuration = 0.5f;
    public bool useMaterialAlpha = false;
    public float alphaStart = 1.0f;
    public float alphaEnd = 0.5f;

    //private variables
    private bool _isFadingOut = false;
    private bool _alreadyFaded = false;
    private float _alphaDiff;
    private float _startTime;
    private Renderer _rend;
    private Color _fadeColor;
    private bool _statueFrozen = true;
    private Rigidbody _thisRigidbody;
    private Collider _thisCollider;

    //private variable accessors

    //methods
    private void Awake()
    {
        _thisRigidbody = gameObject.GetComponent<Rigidbody>();
        _thisCollider = gameObject.GetComponent<Collider>();

        //make sure VRTK scripts are attached
        if (GetComponent<VRTK_InteractableObject>() == null)

        {
            Debug.LogError("Statue must have the VRTK_InteractableObject script attached to it");
            return;
        }

        //create event listeners for grabbing and releasing picture
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(StatueGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new InteractableObjectEventHandler(StatueReleased);
    }

    void Start()
    {
        //get renderer and set fade color
        //rendering mode on material must be set to "Fade"
        _rend = GetComponent<Renderer>();
        _fadeColor = _rend.material.color;

        if (!useMaterialAlpha)
        {
            _fadeColor.a = alphaStart;
        }

        _alphaDiff = alphaStart - alphaEnd;
    }

    void Update()
    {
        //manage statue physics and kinematics states
        if (_statueFrozen)
        {
            _thisRigidbody.useGravity = false;
            _thisRigidbody.Sleep();
        }

        if (!_statueFrozen)
        {
            _thisRigidbody.useGravity = true;
            _thisRigidbody.WakeUp();
        }

        //fade picture out
        if (_isFadingOut && !_alreadyFaded)
        {
            _thisCollider.enabled = false;
            float elapsedTime = Time.time - _startTime;
            if (elapsedTime <= fadeDuration)
            {
                float fadeProgress = elapsedTime / fadeDuration;
                float alphaChange = fadeProgress * _alphaDiff;
                _fadeColor.a = alphaStart - alphaChange;
                _rend.material.color = _fadeColor;
            }
            else
            {
                _fadeColor.a = alphaEnd;
                _rend.material.color = _fadeColor;
                _alreadyFaded = true;
            }
        }

        //fade picture in
        if (!_isFadingOut && _alreadyFaded)
        {
            _thisCollider.enabled = true;
            float elapsedTime = Time.time - _startTime;
            if (elapsedTime <= fadeDuration)
            {
                float fadeProgress = elapsedTime / fadeDuration;
                float alphaChange = fadeProgress * _alphaDiff;
                _fadeColor.a = alphaEnd + alphaChange;
                _rend.material.color = _fadeColor;
            }
            else
            {
                _fadeColor.a = alphaStart;
                _rend.material.color = _fadeColor;
                _alreadyFaded = false;
            }
        }
    }

    private void StatueGrabbed(object sender, InteractableObjectEventArgs e)
    {
        //set gravity and kinematic for simulation realism
        _statueFrozen = false;
        FadePictureOut();
    }

    private void StatueReleased(object sender, InteractableObjectEventArgs e)
    {
        FadePictureIn();
    }

    public void FadePictureOut()
    {
        _isFadingOut = true;
        _startTime = Time.time;
    }

    public void FadePictureIn()
    {
        _isFadingOut = false;
        _startTime = Time.time;
    }
}
