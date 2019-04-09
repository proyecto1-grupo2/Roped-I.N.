using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float maxSpeed = 5;//ponemos un maximo de velocidad se puede poner publico
    public float acceleration, jumpForce;
    private float moveX;
    bool saltando;
    public GameObject Suelo;
    bool grounded;
    bool camaraMov = false;
    int shooting;
    private Animator anim;
    bool jump;
    int tiempoinmune;
    Vector2 movement, dirGancho;
    Rigidbody2D rb;

    //daño
    private bool mov = true;
    public float Timedmg;

    //Referencia al gancho para comunicarse con él
    MovGancho gancho;

    //Movimiento del personaje enganchado
    public float velocidadEnganchado;
    bool enganchado = false;
    Vector3 posGancho, posGanchoInicio;

    private void Start()
    {
        gancho = transform.GetChild(0).GetComponent<MovGancho>();
        rb = GetComponent<Rigidbody2D>();
        //Si no tiene Rigidbody
        if (rb == null)
        {
            Debug.LogError("Falta RigidBody");
        }
        dirGancho = Vector2.right;
        posGanchoInicio = gancho.transform.localPosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        //Animaciones
        shooting = gancho.GetShooting();
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Jump", jump);
        anim.SetInteger("Shooting", shooting);

        //Nos aseguramos de que sea dinámico  
        if (!enganchado && mov)
        {

            //Leo entrada de teclado para moverme en el ejeX
            moveX = Input.GetAxis("Horizontal");
            // Para que el jugador se pare al soltal el teclado
             if (!saltando && Input.GetButtonUp("Derecha") || (!saltando && Input.GetButtonUp("Izquierda")))
             {
                 rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
             }
            if (!mov) { moveX = 0; }

            //Salto  si estoy en el suelo
            if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f) //Nos aseguramos de que el jugador esté en el suelo comparando su rb.velocity.y
            {
                jump = true;
            }

            //esta serie de else if es para determinar la direccion donde mira el personaje
            //(para saber donde disparar el gancho)
            if (Input.GetButton("Derecha"))
            {
                //dirGancho = Vector2.right;
                dirGancho = Vector2.right;
                //esta condicion es necesaria porque sino mientras el gancho esta en ida/vuelta y el jugador rota, el gancho tambien
                if (gancho.daEstado() == HookState.Quieto)
                {
                    //cambia la rotacion en el eje Y del player cuando se mueve a la izquierda
                    transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 0);
                }
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            else if (Input.GetButton("Izquierda"))
            {
                dirGancho = Vector2.right;
                //esta condicion es necesaria porque sino mientras el gancho esta en ida/vuelta y el jugador rota, el gancho tambien
                if (gancho.daEstado() == HookState.Quieto)
                {
                    //pone la rotacion en el eje Y del jugador a 0 si se mueve a la derecha
                    transform.rotation = new Quaternion(transform.rotation.x, -180, transform.rotation.z, 0);
                }
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else if (Input.GetKey(KeyCode.S))
            {

                dirGancho = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                dirGancho = Vector2.up;
            }
        }
        else if (enganchado)  //Estado Enganchado
        {
            float step = velocidadEnganchado * Time.deltaTime;
            posGancho = gancho.GetComponent<Transform>().position;
            if (transform.position.x < posGancho.x) posGancho -= posGanchoInicio;
            else posGancho += posGanchoInicio;
            //var heading = posGancho - transform.position;
            //Debug.Log("heading"+heading);
            // if(transform.position.x<posGancho.x) transform.Translate(heading.x * Time.deltaTime, heading.y * Time.deltaTime, 0);
            //else transform.Translate(-heading.x * Time.deltaTime, heading.y * Time.deltaTime, 0);
            transform.position = Vector2.MoveTowards(transform.position, posGancho, step);
            //transform.position = Vector2.Lerp(transform.position, posGancho, step);
            if (Vector2.Distance(transform.position, gancho.transform.position) < 0.8f)
            {
                gancho.cambiaEstado(HookState.Quieto);
                CambiaEstado(false);
                rb.isKinematic = false;
            }

        }
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 0.6f, Color.red);
    }
    private void FixedUpdate()//Mueve al personaje
    {
        if (enganchado == false) //aseguramos que no se ejecute cuando sea cinematico
        {
            //Vector de movimiento
            movement = new Vector2(moveX, 0);
            if (rb != null && Mathf.Abs(rb.velocity.x) < maxSpeed) //Limitamos la velocidad del jugador con el rb.velocity.x
            {
                /* //para que no se deslice
                  if (cambio)
                  {
                      rb.velocity = new Vector2(0, rb.velocity.y);
                  }
                  //para que se pueda volver a mover
                  if (movement.x == 0)  cambio = false;*/

                rb.AddForce(movement * acceleration); //Movimiento 
            }
            if (rb.velocity.y < -10)//para que no caiga muy rapido
            {
                rb.velocity = new Vector2(rb.velocity.x, -10);
            }
            //Salto
            //La variable jump se hace falsa despues de hacer el salto para que no se ejecute el salto mas veces en el FixedUpdate
            if (jump)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jump = false;
                saltando = true;
            }
        }

    }

    /*****************************************************************************
 * Este OnTriggerEnter2D detecta cualquier colision que sufra el player      *
 * mientras se desplaza con el gancho.                                       *
 * (incluida la propia colision con el enganche)                             *
 * De forma provisional está metido como metodo dentro del PlayerController, *
 * si no fuese la manera más correcta de hacerlo, lo paso a una componente   *
 * externa.                                                 -Nico F. Thovar  *
 *****************************************************************************/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enganchado == true)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;//Movimiento dinamico si player colisiona 
            gancho.cambiaEstado(HookState.Quieto);
            CambiaEstado(false);
        }
    }
     private void OnCollisionEnter2D(Collision2D Suelo)
     {
         saltando = false;
     }

    //realiza un pequeño salto al entrar en contacto con un enemigo y sufrir daño
    public void EnemyKnockBack(float enemyPosX)
    {
        jump = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x); //devuelve -1, 0 o 1 dependiendo de si un numero es positivo, negativo o 0. Asi sabremos la direccion donde aplicar la fuerza. 
        rb.AddForce(Vector2.left * side * jumpForce / 2, ForceMode2D.Impulse); //aplicamos una fuerza diagonal
        mov = false;//desactivamos mov mientras estemos sufriendo daño
        Invoke("EnableMovementandColision", Timedmg);//volvemos activar mov despues de un tiempo
    }
    //activa/desactiva el movimiento
    public bool EnabledMovement(bool valor)
    {
        return mov = valor;
    }
    //Metodo para alternar el estado del jugador
    //bool "enganchado" true==>mov cinematico   false ==> mov fisico
    public void CambiaEstado(bool estado)
    {
        enganchado = estado;
        if (estado)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().simulated = false;
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                gancho.cambiaEstado(HookState.Vuelta);
                CambiaEstado(false);
            }
        }

        else
        {

            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().simulated = true;

        }
    }
    public Vector2 DevuelveDireccion()
    {
        return dirGancho;
    }
    public void SetGrounded(bool ground)
    {
        grounded = ground;
    }
}


