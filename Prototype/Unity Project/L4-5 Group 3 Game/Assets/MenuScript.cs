using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public void DisableObject(GameObject ObjectToDisable)
    {
        ObjectToDisable.SetActive(false);
    }

    public void EnableObject(GameObject ObjectToEnable)
    {
        ObjectToEnable.SetActive(true);
    }
}
