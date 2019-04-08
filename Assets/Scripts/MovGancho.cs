using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovGancho : MonoBehaviour
{
    //Velocidad y rango máximo del gancho
    public float speed, range;
    //Referencia del Player para permitir la comunicación entre ambos 
    public PlayerController Player;
    //posicion de un objeto estático para desvincular (podria ahorrarse)
    //public Transform Padre1;
    //La posicion (dentro del jugador) desde la que el jugador lanza el gancho
    private Transform puntoLanzamiento;
    //booleano para saber si está en modo agarre o en modo gancho
    //bool agarre;
    //Recoge el vector de la dirección a la que mira el personaje y el del propio movimiento del gancho
    Vector2 dir,
            movement;

    //AnimacionGancho
    int shooting;

    //Estado actual del gancho (los estados están explicados en el script EstadosGancho)
    HookState currState;

    void Start()
    {
        puntoLanzamiento = Player.transform.GetChild(1);
        dir = Vector2.right;
        currState = HookState.Quieto;
        movement = Vector2.zero;
        shooting = 0;
    }

    void Update()
    {
        switch (currState)
        {
            case HookState.Quieto:
                {
                    shooting = 0;
                    transform.SetParent(Player.transform);
                    transform.rotation = transform.parent.rotation;
                    //Player.EnabledMovement(true);
                    //if (Input.GetButtonDown("Agarre"))
                    //{
                    //    //agarre = true;
                    //    dir = Player.DevuelveDireccion();
                    //    SetMovement(dir); 
                    //    currState = HookState.Ida;
                    //}
                    //else if (Input.GetButtonDown("Ataque"))
                    //{
                    //    //agarre = false;
                    //    dir = Player.DevuelveDireccion();
                    //    SetMovement(dir); 
                    //    currState = HookState.Ida;
                    //}
                    if (Input.GetButtonDown("Gancho"))
                    {
                        dir = Player.DevuelveDireccion();
                        SetMovement(dir);
                        currState = HookState.Ida;
                    }
                    transform.position = puntoLanzamiento.position;
                }
                break;
            case HookState.Ida:
                {
                    shooting = 1;
                    if (dir.y > 0)
                    {
                        shooting = 3;
                    }
                    //Player.EnabledMovement(false);
                    transform.Translate(movement);
                    //Debug.Log("Current State: " + currState);

                    if (Vector2.Distance(puntoLanzamiento.position, transform.position) >= range)
                        currState = HookState.Vuelta;
                }
                break;
            case HookState.Vuelta:
                {
                    shooting = 1;
                    if (dir.y > 0)
                    {
                        shooting = 3;
                    }
                    // Player.EnabledMovement(false);
                    //transform.Translate(-movement);
                    transform.position = Vector2.MoveTowards(transform.position, puntoLanzamiento.position, speed * Time.deltaTime);
                    //Debug.Log(Vector2.Distance(puntoSalida.position, transform.position));
                    if (Vector2.Distance(puntoLanzamiento.position, transform.position) < 0.5f) //0.5 es un valor de error, 
                    {
                        currState = HookState.Quieto;

                    }
                    // Debug.Log("Current State MovGancho: " + currState);
                }
                break;
            //este caso contempla cuando el gancho impacta sobre una zona viscosa (rosa)
            case HookState.Enganchado:
                {
                    shooting = 2;
                    transform.parent = null;//Desvinculamos el gancho como hijo del jugador
                    Player.CambiaEstado(true);//Cambia a true el bool "enganchado" en el PlayerController
                }
                break;
        }
        if (transform.childCount > 0 && Input.GetButtonDown("Soltar"))
        {
            //transform.GetComponentInChildren<AtraeQuimicos>().DesenganchaQuimico();
            transform.GetComponentInChildren<AtraeQuimicos>().Enganche();
            currState = HookState.Vuelta;
        }
    }

    //Establece el movimiento que va a seguir el gancho según la dirección a la que se mire
    void SetMovement(Vector2 direction)
    {
        movement = new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime);
        if (direction.y > 0)
        {
            shooting = 3;
        }
    }

    //Da el estado actual del gancho
    public HookState daEstado()
    {
        return currState;
    }

    //Cambia el estado del gancho
    public void cambiaEstado(HookState estado)
    {
        currState = estado;
    }
    public int GetShooting()
    {
        return shooting;
    }

    //Da el modo acutal del gancho (daño o agarre)
    //public bool daModo()
    //{
    //    return agarre;
    //}

}