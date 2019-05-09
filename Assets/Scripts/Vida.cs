using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour {

    public int vida;
    public float tiempoinmune;
    private PlayerController player;
    Rigidbody2D rb;
    //UIManager UIManager;
    public GameObject deathMenu;
    bool isInmune = false;
    bool hurt = false;
    private Animator anim;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();
        if (player != null)
            GameManager.instance.SetPlayerHealth(vida);
        anim = GetComponent<Animator>();
        

    }
    private void Update()
    {
        anim.SetBool("IsHurting", hurt);
    }

    //Quitamos vida en funcion del daño que llega
    public void LoseLife(int dmg)
    {
        SoundManager.instance.CallSoundManager("hurt");
        vida -= dmg;
        if (gameObject.GetComponent<PlayerController>()) //si el jugador sufre daño
        {
            
            if (!isInmune)
            {
                player.SetHurt(true);
                Debug.Log("vidas:" + vida);
                GameManager.instance.PlayerLoseLife(vida);
                if (vida <= 0)
                {
                    player.SetDead(true);
                    //GameManager.instance.Pausa();
                    Time.timeScale = 0;
                    deathMenu.SetActive(true);

                }
            }


        }else if (vida > 0)
        {
            hurt = true;
            Invoke("HurtFalse", 1f);
        }
        else if (vida <= 0) { GetComponent<Death>().OnDead(); }
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
    /*public int daVida()
    {
        return vida;
    }*/
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

    void HurtFalse()
    {
        hurt = false;
    }
}