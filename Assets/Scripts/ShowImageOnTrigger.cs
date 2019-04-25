using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImageOnTrigger : MonoBehaviour {
    public SpriteRenderer image;
    public float time;
    private void Start()
    {
        HideImage();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
            image.gameObject.SetActive(true);
        //Invoke("HideImage", time);
    }

    public void HideImage()
    {
        image.gameObject.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
            image.gameObject.SetActive(false);
    }

}
