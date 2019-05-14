using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOtherMenu : MonoBehaviour {

    public GameObject menu;
    private void OnEnable()
    {
        menu.SetActive(false);
    }
    private void OnDisable()
    {
        menu.SetActive(true);
    }
}
