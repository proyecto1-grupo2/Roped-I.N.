using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transicion : MonoBehaviour {
    public GameObject levelCompleteScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
        levelCompleteScreen.gameObject.SetActive(true);
    }
}
