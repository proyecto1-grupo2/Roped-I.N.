using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] lives;
    // Use this for initialization
    void Start()
    {
        GameManager.instance.SetUI(this);
    }

    public void LifeLost(int vida)
    {
        
        lives[vida].enabled = false;
    }
    public void GanaVida(int vida)
    {
        //Debug.Log(vida);
        lives[vida].enabled = true;
    }
}