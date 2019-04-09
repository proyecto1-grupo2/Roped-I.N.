using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transicion : MonoBehaviour {
    public string nextLevel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            GameManager.instance.ChangeScene(nextLevel);
        }
    }
}
