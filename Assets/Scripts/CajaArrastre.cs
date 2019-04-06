using UnityEngine;

public class CajaArrastre : MonoBehaviour
{
    
    Renderer rend;
    public LayerMask Mask;
    Rigidbody2D Cajarb;
    Vector3 boxsize;
    bool colision;
    //bool colisionTerreno;
    public Transform target;//Gancho 
    

    void Start()
    {
        rend = GetComponent<Renderer>();
        boxsize = rend.bounds.size/2;
        
        
        Cajarb = GetComponent<Rigidbody2D>();
        //Si no tiene Rigidbody
        if (Cajarb == null)
        {
            Debug.LogError("Falta RigidBody");
        }
        colision = false;
        //colisionTerreno = false;
    }

    
    void Update()
    {

        /*Debug.DrawRay(transform.position + boxsize, transform.TransformDirection(Vector2.down) * 2, Color.yellow); //Debug para saber la linea de raycast
        Debug.DrawRay(transform.position - boxsize, transform.TransformDirection(Vector2.down) * 2, Color.red); //Debug para saber la linea de raycast
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 1, Color.black); // "*/

        if (colision)
        {
            transform.SetParent(target.transform);
            //Debug.Log("siiiii");
        }
        /*else if (!colisionTerreno)
        {
            transform.Translate(0, -5*Time.deltaTime, 0);
        }*/


    }
    private void FixedUpdate() //metodo para cuando colision es cierta cambie la posicion de la caja al gancho
    {
        //Detectar si ha llegado hasta el jugador, si es cierto se desactiva el booleano colision
        if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1, Mask) ||
           Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 1, Mask)) 
        {
            colision = false;
            transform.parent = null;
        }
        else if (target.position.x<transform.position.x && !Physics2D.Raycast(transform.position + boxsize, 
            transform.TransformDirection(Vector2.down), 2, Mask))// si no toca suelo se suelta la caja para que caiga
        {
            colision = false;
            transform.parent = null;
            //colisionTerreno = false;
        }
        else if (target.position.x > transform.position.x && !Physics2D.Raycast(transform.position - boxsize,
          transform.TransformDirection(Vector2.down), 2, Mask))// si no toca suelo se suelta la caja para que caiga
        {
            colision = false;
            transform.parent = null;
            //colisionTerreno = false;
        }

    }


    
    void OnTriggerEnter2D(Collider2D other)// metodo para detectar colision
    {
        // condicion para detectar si colisiona con el gancho del player 
        
        MovGancho hook = other.gameObject.GetComponent<MovGancho>();
        if (hook != null)
        {
            if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 2, Mask))  // detectar si se esta en contacto con el suelo 
            {

                colision = true;
                hook.cambiaEstado(HookState.Vuelta);
            }
            else
            {
                colision = false;

            }
            
        }
        /*else if (other.gameObject.CompareTag("Suelo") || other.gameObject.CompareTag("Boton"))
        {
            colisionTerreno = true;
        }*/


    }

   /* void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo") || other.gameObject.CompareTag("Boton"))
        {
            colisionTerreno = true;
        }
    }*/



}
  
