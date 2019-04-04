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
            pauseMenu.SetActive(true);
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
        
        lives[vida].enabled = false;
    }
    public void GanaVida(int vida)
    {
        //Debug.Log(vida);
        lives[vida].enabled = true;
    }
}