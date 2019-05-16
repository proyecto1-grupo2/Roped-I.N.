using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private Vector2 dir;//variable para guardar donde mira el personaje
    int playerLives;
    UIManager UIManager;// hace referencia al script UIManager
    bool savedData = false;
    bool pausado = false;
    bool immuneCheat = false, nogravityCheat = false; //cheats

    void Awake()
    {
        if (instance == null)//creamos un GameManager si no existe
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);//asi aunque cambiamos de escena no se borra
        }
        else
        {
            Destroy(this.gameObject);//si ya existe un objeto GameManager, no necestiamos otro
        }
    }

    void Update()
    {
        //se puede pausar el juego siempre que el jugador este vivo
        if (Input.GetButtonDown("Pausa") && playerLives>0)
        {
            Pausa();
        }
    }

    //establece el atributo uimanager
    internal void SetUI(UIManager ui)
    {
        UIManager = ui;
    }
    //resta las vidas de la UI
    public void PlayerLoseLife(int vidasplayer)
    {
        UIManager.LifeLost(vidasplayer);
        playerLives = vidasplayer;

    }
    //inicializa las vidas que tiene el jugador
    public void SetPlayerHealth(int playerHealth)
    {
        playerLives = playerHealth;
    }
    //Suma uno a la vida del jugador
    public void PlayerGetLife(int vidasplayer)
    {
        
        if (vidasplayer < 3)
        {
            
            UIManager.GanaVida(vidasplayer);
            vidasplayer += 1;
        }
        playerLives = vidasplayer;
    }
    //devuelve el num de vidas
    public int getLives()
    {
        return playerLives;
    }
    //Cambia de escena 
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        pausado = true;
        Pausa();
    }
    //Guarda la partida. Solo guarda la escena en la que estas
    public void Save()
    {
       
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        savedData = true;
    }

    //carga partida si hay una guardada
    public void Load()
    {
        if(savedData)
        {
            ChangeScene(PlayerPrefs.GetString("scene"));
        }
    }
    //Si no esta pausado, se pausa y salta el menu pausa y viceversa 
    public void Pausa()
    {
        pausado = !pausado;

        if (pausado)
        {
            Time.timeScale = 0;
            UIManager.ModifyPauseMenu(true);
        }
        else if (!pausado)
        {
            Time.timeScale = 1;
            UIManager.ModifyPauseMenu(false);
        }
    }

    //Cheats
    public bool GetImmuneCheat()
    {
        return immuneCheat;
    }

    public bool GetNoGravityCheat()
    {
        return nogravityCheat;
    }

    public void SetCheats(bool immune, bool nogravity)
    {
        immuneCheat = immune;
        nogravityCheat = nogravity;
    }
}
