using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    //private variable
    private Scene _scene;

    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();
        Debug.Log(_scene.name + " has been loaded");
    }

    void Update () {

        //Start tutorial
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(TutorialManager.Instance.TutorialStage == 0)
            {
                TutorialManager.Instance.TutorialStage = 1;
            }
        }

        //Return to tutorial room
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_scene.name == "scene_01")
            {
                GameController.Instance.RefreshCatalog();
                SceneManager.LoadScene("TutorialRoom", LoadSceneMode.Single);
            }
        }
    }
}
