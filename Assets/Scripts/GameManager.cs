using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private Vector2 dir;//variable para guardar donde mira el personaje
    int vidasplayer;
    //bool isInmune = false;
    UIManager UIManager;// hace referencia al script UIManager

    void Awake()
    {
        if (instance == null)//creamos un GameManager si no existe
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);//asi aunque cambiamos de escena no se borra
        }
        else
        {
            Destroy(this.gameObject);//si ya existe un objeto GameObject, no necestiamos otro
        }
    }
   
    
    public void direccion(Vector2 direc)//metodo para obtener la direccion (se llama desde PlayerController)
    {
       
        dir = direc;
    }
    public Vector2 dirGancho()//sirve para dar la direccion al gancho
    {
        
        return dir;
    }

    //establece el atributo uimanager
    internal void SetUI(UIManager ui)
    {
        UIManager = ui;
    }
    //resta vida, llama a Lifelost y devuelve true si el jugador aun tiene vidas
    public void PlayerLoseLife(int vidasplayer)
    {
        UIManager.LifeLost(vidasplayer);
        

    }
    public void PlayerGanaVida(int vidasplayer)
    {
        
        if (vidasplayer < 3)
        {
            
            UIManager.GanaVida(vidasplayer);
            vidasplayer += 1;
        }
        
    }
    //devuelve el num de vidas
    public int getVidas()
    {
        return vidasplayer;
    }
    public void resetGame()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void finishGame()
    {
        resetGame();
    }
}
