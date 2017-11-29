using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour {
    //serialized variables
    [SerializeField]
    private GameObject _spherePrefab;

    //private variables
    private bool _spheresInstantiated = false;
    private GameObject _sphere;

	void Update () {

        //Load spheres for tutorial stage 3
        if (TutorialManager.Instance.TutorialStage == 3 && _spheresInstantiated == false)
        {
            for(int i = 0; i < 4; i++)
            {
                _sphere = Instantiate(_spherePrefab) as GameObject;
                switch (i)
                {
                    case 0:
                        _sphere.transform.position = new Vector3(-1.0f, 1.5f, -0.5f);
                        break;
                    case 1:
                        _sphere.transform.position = new Vector3(-1.0f, 1.5f, 5.4f);
                        break;
                    case 2:
                        _sphere.transform.position = new Vector3(-5.4f, 1.5f, 5.4f);
                        break;
                    case 3:
                        _sphere.transform.position = new Vector3(-5.4f, 1.5f, -0.5f);
                        break;
                    default:
                        break;
                }
            }
            _spheresInstantiated = true;
        }
    }
}
