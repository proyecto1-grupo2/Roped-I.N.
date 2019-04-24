using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;//ponemos un maximo de velocidad se puede poner publico
    public float acceleration, jumpForce; //acceleration no haría falta si se utiliza velocity
    private float moveX;
    public GameObject Suelo;
    bool grounded;
    bool camaraMov = false;
    int shooting;
    private Animator anim;
    public AudioClip hurt;
    public AudioClip playerJump;
    public AudioClip playerRun;
    public AudioClip dead;
    public Transform debugDir;//para pruebas
    bool jump, landed, puedeSaltar; //landed no se si se podria utilizar como grounded
    int tiempoinmune;
    Vector2 dirGancho; //movement eliminado
    Rigidbody2D rb;
    int i = 0;

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
        puedeSaltar = false;
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
            //// Para que el jugador se pare al soltal el teclado
            // if (Input.GetButtonUp("Derecha") ||  Input.GetButtonUp("Izquierda"))
            // {
            //     rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            // }
            //if (!mov)                moveX = 0;

            //Salto  si estoy en el suelo, landed lo comprueba 

            jump = Input.GetButtonDown("Jump");
            landed = Mathf.Abs(rb.velocity.y) < 0.01f;
            if (jump && landed)
            {
                puedeSaltar = true;
            }
            //SoundManager.instance.RandomizeSfx(playerJump);


            //esta serie de else if es para determinar la direccion donde mira el personaje
            //(para saber donde disparar el gancho)
            if (Input.GetAxisRaw("Horizontal") == 1) //Mira dcha.
            {
                dirGancho = Vector2.right;
                //esta condicion es necesaria porque sino mientras el gancho esta en ida/vuelta y el jugador rota, el gancho tambien
                if (gancho.daEstado() == HookState.Quieto)
                {
                    //cambia la rotacion en el eje Y del player cuando se mueve a la izquierda
                    transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 0);
                }
                //rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                debugDir.localPosition = dirGancho;
                //SoundManager.instance.RandomizeSfx(playerRun);
            }

            else if (Input.GetAxisRaw("Horizontal") == -1) //Mira izq
            {
                dirGancho = Vector2.right;
                //esta condicion es necesaria porque sino mientras el gancho esta en ida/vuelta y el jugador rota, el gancho tambien
                if (gancho.daEstado() == HookState.Quieto)
                {
                    //pone la rotacion en el eje Y del jugador a 0 si se mueve a la derecha
                    transform.rotation = new Quaternion(transform.rotation.x, -180, transform.rotation.z, 0);
                }
                //rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //SoundManager.instance.RandomizeSfx(playerRun);
                debugDir.localPosition = dirGancho;

            }
            else if (Input.GetAxisRaw("Vertical") == -1) //Mira abajo
            {
                dirGancho = Vector2.down;
                debugDir.localPosition = dirGancho;

            }
            else if (Input.GetAxisRaw("Vertical") == 1) //Mira arriba
            {
                dirGancho = Vector2.up;
                debugDir.localPosition = dirGancho;

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
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 0.6f, Color.red);
    }
    private void FixedUpdate()//Mueve al personaje
    {
        //Debug.Log("landed " + landed + " jump " + jump);
        if (enganchado == false) //aseguramos que no se ejecute cuando sea cinematico
        {

            rb.velocity = new Vector2(moveX * maxSpeed, rb.velocity.y);
            //if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            //    rb.AddForce(new Vector2(moveX * acceleration, 0)); //Movimiento           
            //Salto
            //La variable jump se hace falsa despues de hacer el salto para que no se ejecute el salto mas veces en el FixedUpdate

            if (Salto())
            {
                // Debug.Log("Aqui tambien es el intento " + i);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jump = false;
                puedeSaltar = false;
                //saltando = true;
            }
            //Salto();

            if (rb.velocity.y < -10)//para que no caiga muy rapido
            {
                rb.velocity = new Vector2(rb.velocity.x, -10);
            }
        }

    }
    private bool Salto()
    {
        Debug.Log("hola");
        return puedeSaltar;
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
    //private void OnCollisionEnter2D(Collision2D Suelo)
    //{
    //    saltando = false;
    //}

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





/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaPlayerController : MonoBehaviour {

    /// <summary>
    /// Velocidad máxima
    /// </summary>
    public float speed = 1.0f;

    /// <summary>
    /// Aceleración de movimiento horizontal
    /// </summary>
    public float accel = 1.0f;

    /// <summary>
    /// Fuerza de salto
    /// </summary>
    public float jumpForce = 10.0f;


    bool jump = false;
    bool landed;
    float moveX = 1f;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        landed = Mathf.Abs(rb.velocity.y) < 0.01f;
        jump = Input.GetButtonDown("Jump");
        moveX = Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// Llamado justo antes de la simulación física, cambia
    /// la velocidad del objeto en base al eje horizontal de
    /// la entrada.
    /// </summary>
	void FixedUpdate()
    {

        // Somos flexibles en el movimiento horizontal del PlayerController,
        // siempre que seamos coherentes:
        // 1. Se puede modificar la velocidad del jugador de manera instantánea, 
        //    con cuidado de no perder la velocidad en Y
        // 2. Se puede aplicar una fuerza para acelerarlo y limitar la velocidad máxima.
        //    En este caso, es necesario que haya una variable de aceleración
        //    (no tiene sentido usar la misma que la velocidad)

        // TRUCO: Si quitas una / a la siguiente línea cambia el código comentado ;-)
        //*
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
        /*/
/* if (Mathf.Abs(rb.velocity.x) < speed)
 {
     // Al igual que haremos con el salto, lo deberíamos limitar a una vez por frame
     rb.AddForce(new Vector2(accel*moveX, 0));
 }
 /**/

// La fuerza de salto solo se debe de aplicar una vez por frame para evitar
// saltos incontrolados
/* if (landed && jump && rb != null)
 {
     rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
     jump = false;
 }
}
}*/
