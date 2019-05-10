using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ImpQuimicos : MonoBehaviour
{
    EnemyState estadoEnemigo;
    int time;
    Vida vida;

    public int dpsFuego=10, dañoEleco=10;//daño del quimico electrico y fuego por defecto
    bool quemado;
    public float tStun=4;//tiempo de stun por defecto
    public GameObject hielo;

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
                if(gameObject.GetComponent<PingPongMovement>()!=null)
                {
                    gameObject.GetComponent<PingPongMovement>().enabled = false;
                }
                else if (gameObject.GetComponent<PursuitTarget>() != null)
                {
                    gameObject.GetComponent<PursuitTarget>().enabled = false;
                }
                else if (gameObject.GetComponent<ShootTurm>() != null)
                {
                    gameObject.GetComponent<ShootTurm>().enabled = false;
                }
                //Desactivar el daño al jugador
                gameObject.GetComponent<Damage>().enabled = false;
                //Desactiva que sea Trigger
                if (gameObject.GetComponent<BoxCollider2D>())
                {
                    gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                }
                else if (gameObject.GetComponent<CircleCollider2D>())
                {
                    gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
                }
                //Instantiate(hielo, transform.position, transform.rotation);
                hielo.transform.position = transform.position;
                hielo.GetComponent<SpriteRenderer>().enabled = true;
                //hielo.GetComponent<SpriteRenderer>().enabled = true;
                hielo.transform.localScale = transform.localScale*2;
                //Cambiar sprite (De momento solo cambia el tono a un más azulado)
                /*SpriteRenderer sprit = gameObject.GetComponent<SpriteRenderer>();
                sprit.material.color = Color.blue;*/
                Invoke("cambiaEstadoNada", tStun);

                break;
            case EnemyState.Paralizado:
                //mientras esta paralizado no se puede mover
                //Debug.Log("PARALIZADO");
                if (time < 1)
                {
                    //Debug.Log("Veces");
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
        if (gameObject.GetComponent<PingPongMovement>() != null)
        {
            gameObject.GetComponent<PingPongMovement>().enabled = true;
        }
        else if (gameObject.GetComponent<PursuitTarget>() != null)
        {
            gameObject.GetComponent<PursuitTarget>().enabled = true;
        }
        else if (gameObject.GetComponent<ShootTurm>() != null)
        {
            gameObject.GetComponent<ShootTurm>().enabled = true;
        }
        gameObject.GetComponent<Damage>().enabled = true;
        // gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        hielo.GetComponent<SpriteRenderer>().enabled = false;
        if (gameObject.GetComponent<BoxCollider2D>())
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (gameObject.GetComponent<CircleCollider2D>())
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
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
            SoundManager.instance.CallSoundManager("fuego");
        }
        else if (other.gameObject.CompareTag("QuimicoElectrico"))
        {
            estadoEnemigo = EnemyState.Paralizado;
            Destroy(other.gameObject);
            SoundManager.instance.CallSoundManager("electrico");
        }
        else if (other.gameObject.CompareTag("QuimicoHielo"))
        {
            estadoEnemigo = EnemyState.Congelado;
            Destroy(other.gameObject);
            SoundManager.instance.CallSoundManager("hielo");
        }

    }
    //quita vida al jugador
    void QuitaVida()
    {

        vida.LoseLife(dpsFuego);
        time += 1;//condicion de parada del invoke
    }
}
