using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && TutorialManager.Instance.TutorialStage == 0)
        {
            TutorialManager.Instance.TutorialStage = 1;
        }

            
    }
}
