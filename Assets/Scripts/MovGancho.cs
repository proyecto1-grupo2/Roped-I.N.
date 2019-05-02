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
    float Ndegrees = 90;
    Vector3 degrees;

    //AnimacionGancho
    int shooting;

    //Estado actual del gancho (los estados están explicados en el script EstadosGancho)
    HookState currState;

    //public SpriteRenderer spriteGancho;
    //public SpriteRenderer cadena;
    bool izquierda;
    void Start()
    {
        puntoLanzamiento = Player.transform.GetChild(1);
        dir = Vector2.right;
        currState = HookState.Quieto;
        movement = Vector2.zero;
        shooting = 0;
        degrees = new Vector3(0, 0, 90);
        //spriteGancho.enabled = false;
        //cadena.enabled = false;
    }

    void Update()
    {
        //Debug.Log("Posicion de " + transform.name + " : " + transform.position);
        //Debug.Log("Rotacion de " + transform.name + " : " + transform.rotation);
        switch (currState)
        {
            case HookState.Quieto:
                {
                    shooting = 0;
                    transform.SetParent(Player.transform);
                    //transform.rotation = transform.parent.rotation;
                    //spriteGancho.enabled = false;
                    //cadena.enabled = false;
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

                    //dir = Player.DevuelveDireccion();
                    //this.GetComponentInChildren<RotacionSpriteGancho>().SetDir(dir);
                    transform.localEulerAngles = Vector3.zero;
                    SetDir();
                    if (Input.GetButtonDown("Gancho"))
                    {
                       
                        SetMovement(dir);
                        currState = HookState.Ida;
                    }
                    
                    transform.position = puntoLanzamiento.position;
                }
                break;
            case HookState.Ida:
                {
                    //spriteGancho.enabled = true;
                    //cadena.enabled = true;
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
                    //spriteGancho.enabled = true;
                    //cadena.enabled = true;
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
                    //cadena.enabled = true;
                    //spriteGancho.enabled = true;
                    shooting = 2;
                    transform.parent = null;//Desvinculamos el gancho como hijo del jugador
                    Player.CambiaEstado(true);//Cambia a true el bool "enganchado" en el PlayerController
                }
                break;
        }
        if (transform.childCount > 0 && Input.GetButtonDown("Soltar"))
        {
            //transform.GetComponentInChildren<AtraeQuimicos>().DesenganchaQuimico();
            if (transform.GetChild(0).GetComponentInChildren<AtraeQuimicos>() != null)
            {
                transform.GetChild(0).GetComponentInChildren<AtraeQuimicos>().Enganche();
                currState = HookState.Vuelta;
            }
        }
        Debug.Log("DireccionMovGancho: " + dir);
    }

    void SetDir()
    {
        dir = Player.DevuelveDireccion();
        switch((int)dir.y)
        {
            case 1:
                transform.Rotate(0, 0, Ndegrees, Space.Self);
                break;
            case -1:
                transform.Rotate(0, 0, 3 * Ndegrees, Space.Self);
                break;
        }
    }
    //Establece el movimiento que va a seguir el gancho según la dirección a la que se mire
    void SetMovement(Vector2 direction)
    {
        movement = new Vector2(speed * Time.deltaTime, 0);
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