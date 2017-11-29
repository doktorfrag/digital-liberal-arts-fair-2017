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
        if (Input.GetKeyDown(KeyCode.Space) && _scene.name == "TutorialRoom")
        {
            if (TutorialManager.Instance.TutorialStage == 0)
            {
                TutorialManager.Instance.TutorialStage = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && _scene.name == "scene_01")
        {
            SceneManager.LoadScene("TutorialRoom", LoadSceneMode.Single);
            GameController.Instance.RefreshCatalog();

        }

    }
}
