using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour {

    public int vida;
    public float tiempoinmune;
    Rigidbody2D rb;
    //UIManager UIManager;
    bool isInmune = false;
    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();

    }

    /*public void SetUI(UIManager ui)
    {
        UIManager = ui;
    }*/


    public void LoseLife(int dmg)
    {
        vida -= dmg;
        if (this.gameObject.GetComponent<PlayerController>()) //si el jugador sufre daño
        {
            Debug.Log("vidas:"+vida);
            GameManager.instance.PlayerLoseLife(vida);
            if (vida <= 0)
            {
                GameManager.instance.resetGame();
            }
            else if (!this.gameObject.CompareTag("Player"))
            {
                this.GetComponent<Death>().OnDead();
            }
        }
        else if (vida <= 0) { this.GetComponent<Death>().OnDead(); }
    }
    public void OnDeadZone(Transform spawnPoint)
    {
        
        if (GameManager.instance.getVidas() >= 0)
        {
           
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            rb.velocity = Vector2.zero;
        }
    }

    void NoInmune()
    {
        isInmune = false;
    }

    //Lo hacemos inmune durante un tiempo 
    public void Inmune()
    {
        isInmune = true;
        Invoke("NoInmune", tiempoinmune);

    }
    public bool DaInmune()
    {
        return isInmune;
    }

}