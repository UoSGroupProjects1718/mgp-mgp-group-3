using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public void DisableObject(GameObject ObjectToDisable)
    {
        ObjectToDisable.SetActive(false);
    }

    public void EnableObject(GameObject ObjectToEnable)
    {
        ObjectToEnable.SetActive(true);
    }

    public void RestartSceme()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
