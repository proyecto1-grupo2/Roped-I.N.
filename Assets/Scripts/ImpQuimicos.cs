using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ImpQuimicos : MonoBehaviour {
    EnemyState estadoEnemigo;
    int time;
    Vida vida;
    
    public int DañoporSegundo;
    bool quemado;
    // Use this for initialization
    void Start () {
        estadoEnemigo = EnemyState.Nada;
        time = 0;
        
        vida = gameObject.GetComponent<Vida>();
        quemado = false;
    }
	
	//Cambia los estados del enemigo
	void Update () {
        switch (estadoEnemigo)
        {

            case EnemyState.Nada:
                
                break;
            case EnemyState.Quemado:
                //cuando está quemado, pierde vida por segundo durante 3 segundos
                if (!quemado)
                {
                    quemado = true;
                    InvokeRepeating("QuitaVida", 1f, 1f);//Cada segundo invoca al metodo
                }
                else if(time>=3) CancelInvoke();
                break;

            case EnemyState.Congelado:
                //mientras esta congelado no se puede mover ni hace daño
                break;
            case EnemyState.Paralizado:
                //mientras esta paralizado no se puede mover
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
    void OnTriggerEnter2D(Collider2D other)
    {
        //si colisiona con el quimico de fuego cambiamos de estado a quemado y hacemos que el gancho vuelva
        if (other.gameObject.CompareTag("QuimicoFuego"))
        {
            estadoEnemigo = EnemyState.Quemado;
            Destroy(other.gameObject);
            MovGancho mov = other.GetComponentInParent<MovGancho>();
            if (mov!=null) mov.cambiaEstado(HookState.Vuelta);
            
        }
        
    }
    //quita vida al jugador
    void QuitaVida()
    {
        
        vida.LoseLife(DañoporSegundo);
        time +=1 ;//condicion de parada del invoke
    }
}
