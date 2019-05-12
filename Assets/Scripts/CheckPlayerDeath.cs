using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPlayerDeath : MonoBehaviour {

    private void OnEnable()
    {
        Debug.Log("Prueba con : " + GameManager.instance.GetPlayerHealth());
        if (GameManager.instance.GetPlayerHealth() == 0)
        {
            Debug.Log("entró");
            gameObject.SetActive(false);
        }

    }
}
