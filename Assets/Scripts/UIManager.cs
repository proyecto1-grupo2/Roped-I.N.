using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] lives;
    public GameObject pauseMenu;
    // Use this for initialization
    void Start()
    {
        GameManager.instance.SetUI(this);
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    //cambia a la escena indicada en el parámetro del OnClick() del botón correspondiente
    public void SceneChange(string sceneName)
    {
        GameManager.instance.ChangeScene(sceneName);
    }

    public void SaveSceneData()
    {
        GameManager.instance.Save();
    }

    public void LoadSceneData()
    {
        GameManager.instance.Load();
    }
    public void LifeLost(int vida)
    {

        if (vida <= 0) { vida = 0; } //Para evitar algun posible bug
        else if (vida >= lives.Length - 1) vida = lives.Length - 1;//para evitar bugs 
        lives[vida].enabled = false;
    }
    public void GanaVida(int vida)
    {
        if (vida >= lives.Length - 1) vida = lives.Length - 1;//Para evitar algun posible bug
        lives[vida].enabled = true;
    }
    public void ModifyMenu(bool mode)
    {
        pauseMenu.SetActive(mode);
    }
}