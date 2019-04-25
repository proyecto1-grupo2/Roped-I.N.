using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedUIObject : MonoBehaviour {

    EventSystem eventSystem;
    private void Awake()
    {
        eventSystem = EventSystem.current;
    }
    void Start () {
        eventSystem.SetSelectedGameObject(this.gameObject);
	}
	
}
