using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {

    public void DestroySphere ()
    {
        Destroy(gameObject);
        TutorialManager.Instance.SphereCount = 1;
    }
}
