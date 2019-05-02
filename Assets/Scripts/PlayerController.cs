using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed,//ponemos un maximo de velocidad se puede poner publico
                 jumpForce; //acceleration no haría falta si se utiliza velocity
    private float moveX;
    bool grounded, hurtanim = false, deadanim = false;
    bool camaraMov = false;
    int shooting;
    private SpriteRenderer spr;
    private Animator anim;
    //public AudioClip hurt;
    //public AudioClip playerJump;
    //public AudioClip playerRun;
    //public AudioClip dead;
    public Transform debugDir;//para pruebas
    bool jump, landed, puedeSaltar; //landed no se si se podria utilizar como grounded


    bool izq = false;

   
    int tiempoinmune;
    Vector2 dirGancho; //movement eliminado
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
        spr = GetComponent<SpriteRenderer>();
        puedeSaltar = false;
    }
    private void Update()
    {
        //Animaciones
        shooting = gancho.GetShooting();
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsHurting", hurtanim);
        anim.SetBool("IsDead", deadanim);
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
           


            //esta serie de else if es para determinar la direccion donde mira el personaje
            //(para saber donde disparar el gancho)
            if (Input.GetAxisRaw("Horizontal") == 1) //Mira dcha.
            {
                izq = false;
                //dirGancho = Vector2.right;
                dirGancho = Vector2.right;
                //esta condicion es necesaria porque sino mientras el gancho esta en ida/vuelta y el jugador rota, el gancho tambien
                if (gancho.daEstado() == HookState.Quieto)
                {
                    //cambia la rotacion en el eje Y del player cuando se mueve a la izquierda
                    transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 0);
                }
                //rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //SoundManager.instance.RunSFX();
            }

            else if (Input.GetAxisRaw("Horizontal") == -1) //Mira izq
            {
                izq = true;
                dirGancho = Vector2.right;
                //esta condicion es necesaria porque sino mientras el gancho esta en ida/vuelta y el jugador rota, el gancho tambien
                if (gancho.daEstado() == HookState.Quieto)
                {
                    //pone la rotacion en el eje Y del jugador a 0 si se mueve a la derecha
                    transform.rotation = new Quaternion(transform.rotation.x, -180, transform.rotation.z, 0);
                }
                //rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                //SoundManager.instance.RunSFX();
            }
            else if (Input.GetAxisRaw("Vertical") == -1) //Mira abajo
            {
                izq = false;
                dirGancho = Vector2.down;

            }
            else if (Input.GetAxisRaw("Vertical") == 1) //Mira arriba
            {
                izq = false;
                dirGancho = Vector2.up;
            }

        }
        else if (enganchado)  //Estado Enganchado
        {
            float step = velocidadEnganchado * Time.deltaTime;
            posGancho = gancho.GetComponent<Transform>().position;
            if (transform.position.x < posGancho.x) posGancho -= posGanchoInicio;
            else posGancho += posGanchoInicio;
            transform.position = Vector2.MoveTowards(transform.position, gancho.transform.GetChild(0).position, step);
            if (Vector2.Distance(transform.position, gancho.transform.position) < 0.8f)
            {
                gancho.cambiaEstado(HookState.Quieto);
                CambiaEstado(false);
                rb.isKinematic = false;
            }

        }
    }
    private void FixedUpdate()//Mueve al personaje
    {

        if (enganchado == false) //aseguramos que no se ejecute cuando sea cinematico
        {

            rb.velocity = new Vector2(moveX * Speed, rb.velocity.y);
            
            //Salto
            //La variable jump se hace falsa despues de hacer el salto para que no se ejecute el salto mas veces en el FixedUpdate

            if (Salto())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jump = false;
                puedeSaltar = false;
                SoundManager.instance.CallSoundManager("jump");
            }

            if (rb.velocity.y < -10)//para que no caiga muy rapido
            {
                rb.velocity = new Vector2(rb.velocity.x, -10);
            }
        }

    }
    private bool Salto()
    { 
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

    //realiza un pequeño salto al entrar en contacto con un enemigo y sufrir daño
    //public void EnemyKnockBack(float enemyPosX)
    //{
    //    jump = true;
    //    float side = Mathf.Sign(enemyPosX - transform.position.x); //devuelve -1, 0 o 1 dependiendo de si un numero es positivo, negativo o 0. Asi sabremos la direccion donde aplicar la fuerza. 
    //    rb.AddForce(Vector2.left * side * jumpForce / 2, ForceMode2D.Impulse); //aplicamos una fuerza diagonal
    //    mov = false;//desactivamos mov mientras estemos sufriendo daño
    //    Invoke("EnableMovementandColision", Timedmg);//volvemos activar mov despues de un tiempo
    //}
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
    //Este metodo duelve la direccion del gancho, lleva un parametro para evitar un bug de que 
    //el input de derecha e izquierda hacian que el gancho no se disparaba hacia donde debia
    public Vector2 DevuelveDireccion()
    {
        //izquierda = izq;
        return dirGancho;
    }
    public void SetGrounded(bool ground)
    {
        grounded = ground;
    }

    public void SetHurt(bool hurting)
    {
        hurtanim = hurting;
        spr.color = Color.red;
        Debug.Log("Daño");
        Invoke("HurtFalse", 0.1f);
        SoundManager.instance.CallSoundManager("hurt");
    }

    void HurtFalse()
    {
        hurtanim = false;
        spr.color = Color.white;
    }

    public void SetDead(bool dying)
    {
        deadanim = dying;
        spr.color = Color.black;
        Invoke("DeadFalse", 1f);
        SoundManager.instance.CallSoundManager("dead");
    }

    void DeadFalse()
    {
        deadanim = false;
        spr.color = Color.white;
    }
}