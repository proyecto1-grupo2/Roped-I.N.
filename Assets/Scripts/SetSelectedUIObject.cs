using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedUIObject : MonoBehaviour {

    void OnEnable () {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(this.gameObject);      
	}
	
}
