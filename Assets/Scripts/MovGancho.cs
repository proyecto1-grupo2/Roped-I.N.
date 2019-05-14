using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovGancho : MonoBehaviour
{
    //Velocidad y rango máximo del gancho
    public float speed, range;
    //Referencia del Player para permitir la comunicación entre ambos 
    public PlayerController Player;

    //La posicion (dentro del jugador) desde la que el jugador lanza el gancho
    private Transform puntoLanzamiento;
    //booleano para saber si está en modo agarre o en modo gancho
    //bool agarre;
    //Recoge el vector de la dirección a la que mira el personaje y el del propio movimiento del gancho
    Vector2 dir,
            movement;
    float Ndegrees = 90;

    //AnimacionGancho
    int shooting;

    //Estado actual del gancho (los estados están explicados en el script EstadosGancho)
    HookState currState;

    bool izquierda;
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
                    gameObject.GetComponent<Damage>().enabled = false;
                    shooting = 0;
                    transform.SetParent(Player.transform);
                    transform.localEulerAngles = Vector3.zero;
                    SetDir();
                    if (Input.GetButtonDown("Gancho"))
                    {
                        gameObject.GetComponent<Damage>().enabled = true;
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
                    transform.Translate(movement);

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
                    transform.position = Vector2.MoveTowards(transform.position, puntoLanzamiento.position, 3*  speed * Time.deltaTime);
                    if (Vector2.Distance(puntoLanzamiento.position, transform.position) < 0.5f) //0.5 es un valor de error, 
                    {
                        currState = HookState.Quieto;

                    }
                }
                break;
            //este caso contempla cuando el gancho impacta sobre una zona viscosa (rosa)
            case HookState.Enganchado: //modificado
                {
                    transform.parent = null;//Desvinculamos el gancho como hijo del jugador
                    shooting = 2;
                    if (Input.GetButtonDown("Soltar"))
                    {
                        Debug.Log("Me solté");    
                        Player.CambiaEstado(false);
                        currState = HookState.Vuelta;
                        
                    }
                }
                break;
        }
        if (transform.childCount > 0 && Input.GetButtonDown("Soltar"))
        {
            if (transform.GetChild(0).GetComponentInChildren<PullChemical>() != null)
            {
                transform.GetChild(0).GetComponentInChildren<PullChemical>().Enganche();
                currState = HookState.Vuelta;
            }
        }
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

    public Vector2 GetDir()
    {
        return dir;
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
}