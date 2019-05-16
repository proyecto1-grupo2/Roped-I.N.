using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed,//ponemos un maximo de velocidad se puede poner publico
                 jumpForce; //acceleration no haría falta si se utiliza velocity
    private float moveX;
    bool hurtanim = false, deadanim = false;
    int shooting;
    private SpriteRenderer spr;
    private Animator anim;
    private ContactFilter2D contactFilter; //Para detectar colisiones cuando el jugador está enganchado
    bool jump, landed, puedeSaltar; //landed no se si se podria utilizar como grounded


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
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask(gameObject.layer)); //el filtro detecta las colisiones en la layer del player
        contactFilter.useLayerMask = true;
    }
    private void Update()
    {
        //Animaciones
        shooting = gancho.GetShooting();
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", landed);
        anim.SetBool("IsHurting", hurtanim);
        anim.SetBool("IsDead", deadanim);
        anim.SetBool("Jump", jump);
        anim.SetInteger("Shooting", shooting);

        //Nos aseguramos de que sea dinámico  
        if (!enganchado && mov)
        {

            //Leo entrada de teclado para moverme en el ejeX
            moveX = Input.GetAxis("Horizontal");
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
                //dirGancho = Vector2.right;
                dirGancho = Vector2.right;
                //esta condicion es necesaria porque sino mientras el gancho esta en ida/vuelta y el jugador rota, el gancho tambien
                if (gancho.daEstado() == HookState.Quieto)
                {
                    //cambia la rotacion en el eje Y del player cuando se mueve a la izquierda
                    transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 0);
                }
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                SoundManager.instance.RunSFX();
            }
            else if (Input.GetAxisRaw("Vertical") == -1) //Mira abajo
            {
                dirGancho = Vector2.down;

            }
            else if (Input.GetAxisRaw("Vertical") == 1) //Mira arriba
            {
                dirGancho = Vector2.up;
            }

        }
       //Se encarga de mover al jugador cuando está enganchado
        else if (enganchado)  
        {
            float step = velocidadEnganchado * Time.deltaTime;
            posGancho = gancho.GetComponent<Transform>().position;
            if (transform.position.x < posGancho.x) posGancho -= posGanchoInicio;
            else posGancho += posGanchoInicio;
            transform.position = Vector2.MoveTowards(transform.position, gancho.transform.GetChild(0).position, step);
            if (Vector2.Distance(transform.position, gancho.transform.position) < 0.8f)
            {
                CambiaEstado(false);
                rb.isKinematic = false;
            }

        }
    }
    private void FixedUpdate()//Mueve al personaje
    {

        if (!enganchado) //aseguramos que no se ejecute cuando sea cinematico
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
        //Estado Enganchado: Detecta colisiones haciendo un cast collider, 
        //una distancia de 0,8f hacia la direccion en la que se haya lanzado el gancho
        //porque cuando esta enganchado se desactiva la simulacion fisica(para solucionar muchos bugs)
        else 
        {
            RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
            int count = rb.Cast(gancho.GetDir(), contactFilter, hitBuffer, 0.25f);
            int x = 0;
            if (count > 0)

            for (int i=0; i<count; ++i)
            {
                if (hitBuffer[i].distance != 0) x = i; //por si no se guarda en el primer elemento del array
            }
            if (count > 0 && hitBuffer[x].distance != 0)
                CambiaEstado(false);
        }
    }
    private bool Salto()
    { 
        return puedeSaltar;
    }

    
    //activa/desactiva el movimiento
    public bool EnabledMovement(bool valor)
    {
        return mov = valor;
    }
    //Metodo para alternar el estado del jugador
    //bool "enganchado" true==>mov cinematico   false ==> mov fisico
    public void CambiaEstado(bool estado) //modificado
    {
        enganchado = estado;
        if (estado)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().simulated = false;
        }

        else
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().simulated = true;
            gancho.cambiaEstado(HookState.Vuelta);
        }
    }
    //Este metodo duelve la direccion del gancho
    public Vector2 DevuelveDireccion()
    {
        return dirGancho;
    }
    //animaciones
    public void SetHurt(bool hurting)
    {
        hurtanim = hurting;
        spr.color = Color.red;
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