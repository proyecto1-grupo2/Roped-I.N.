using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ImpQuimicos : MonoBehaviour
{
    EnemyState estadoEnemigo;
    int time;
    Vida vida;

    public int dpsFuego, dañoElectrico;
    bool quemado;
    public float tStun;


    void Start()
    {
        estadoEnemigo = EnemyState.Nada;
        time = 0;

        vida = gameObject.GetComponent<Vida>();
        quemado = false;
    }

    //Cambia los estados del enemigo
    void Update()
    {
        switch (estadoEnemigo)
        {

            case EnemyState.Nada:
                time = 0;//variable para hacer daño con quimico fuego y electrico
                break;
            case EnemyState.Quemado:
                //cuando está quemado, pierde vida por segundo durante 3 segundos
                if (!quemado)
                {
                    quemado = true;
                    InvokeRepeating("QuitaVida", 1f, 1f);//Cada segundo invoca al metodo
                }
                else if (time >= 3)
                {
                    quemado = false;
                    cambiaEstado(EnemyState.Nada);
                    CancelInvoke();
                }
                break;

            case EnemyState.Congelado:
                //mientras esta congelado no se mueve ni hace daño
                //Desactivar el daño al jugador
                //Debug.Log("Congelado");
                gameObject.GetComponent<PingPongMovement>().enabled = false;
                gameObject.GetComponent<Damage>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

                //Cambiar sprite (De momento solo cambia el tono a un más azulado)
                SpriteRenderer sprit = gameObject.GetComponent<SpriteRenderer>();
                sprit.material.color = Color.blue;
                Invoke("cambiaEstadoNada", tStun);
                break;
            case EnemyState.Paralizado:
                //mientras esta paralizado no se puede mover
                //Debug.Log("PARALIZADO");
                if (time < 1)
                {
                    Debug.Log("Veces");
                    QuitaVida();
                }
                gameObject.GetComponent<PingPongMovement>().enabled = false;
                Invoke("cambiaEstadoNada", tStun);
                break;
        }

    }
    public EnemyState daEstado()
    {
        return estadoEnemigo;
    }

    public void cambiaEstado(EnemyState estado)
    {
        estadoEnemigo = estado;
    }
    public void cambiaEstadoNada()
    {
        estadoEnemigo = EnemyState.Nada;
        gameObject.GetComponent<PingPongMovement>().enabled = true;
        gameObject.GetComponent<Damage>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //si colisiona con el quimico de fuego cambiamos de estado a quemado y hacemos que el gancho vuelva
        if (other.gameObject.CompareTag("QuimicoFuego"))
        {
            estadoEnemigo = EnemyState.Quemado;
            Destroy(other.gameObject);
            MovGancho mov = other.GetComponentInParent<MovGancho>();
            if (mov != null) mov.cambiaEstado(HookState.Vuelta);

        }
        else if (other.gameObject.CompareTag("QuimicoElectrico"))
        {
            estadoEnemigo = EnemyState.Paralizado;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("QuimicoHielo"))
        {
            estadoEnemigo = EnemyState.Congelado;
            Destroy(other.gameObject);
        }

    }
    //quita vida al jugador
    void QuitaVida()
    {

        vida.LoseLife(dpsFuego);
        time += 1;//condicion de parada del invoke
    }
}
