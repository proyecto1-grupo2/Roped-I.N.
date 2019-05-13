using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedUIObject : MonoBehaviour {

    void Start () {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(this.gameObject);
        
	}
	
}
