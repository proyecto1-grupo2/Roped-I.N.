using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image[] lives;
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject levelsMenu;
    public GameObject cheatsMenu;

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

    public void ModifyPauseMenu(bool mode)
    {
        ModifyMenu(pauseMenu, mode);
    }

    public void ModifyControlsMenu(bool mode)
    {
        ModifyMenu(controlsMenu, mode);
    }
    
    public void ModifyLevelsMenu(bool mode)
    {
        ModifyMenu(levelsMenu ,mode);
    }

    public void ModifyCheatsMenu(bool mode)
    {
        ModifyMenu(cheatsMenu, mode);
    }

    private void ModifyMenu(GameObject menu, bool mode)
    {
        if(menu !=null)
            menu.SetActive(mode);            
    }

    public void CierraJuego()
    {
        Debug.Log("CerrandoJuego");
        Application.Quit();
    }
    public void Retry()
    {
        SceneChange(SceneManager.GetActiveScene().name);
    }


}