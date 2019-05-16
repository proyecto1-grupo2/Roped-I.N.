using UnityEngine;

public class PullBox : MonoBehaviour
{
    
    Renderer rend;
    public LayerMask Mask;
    Rigidbody2D Cajarb;
    Vector3 boxsize;
    bool colision;
    public Transform Gancho;//Gancho 
    

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
    }

    //Cuando colisiona con el gancho pasa a ser su hijo. 
    void Update()
    {
        if (colision)
        {
            transform.SetParent(Gancho.transform);
        }
        
    }

    /*
    *En el primer if se suelta la caja cuando llega al jugador
    * en el segundo se suelta cuando no detecta suelo por su derecha
    * en el tercero se suelta cuando no detecta suelo por su izquierda
    */

    private void FixedUpdate() 
    {
        //Detectar si ha llegado hasta el jugador, si es cierto se desactiva el booleano colision
        if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1, Mask) ||
           Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 1, Mask)) 
        {
            colision = false;
            transform.parent = null;
        }
        // si no toca suelo se suelta la caja para que caiga
        else if (Gancho.position.x<transform.position.x && !Physics2D.Raycast(transform.position + boxsize, 
            transform.TransformDirection(Vector2.down), 2, Mask))
        {
            colision = false;
            transform.parent = null;
        }
        else if (Gancho.position.x > transform.position.x && !Physics2D.Raycast(transform.position - boxsize,
          transform.TransformDirection(Vector2.down), 2, Mask))// si no toca suelo se suelta la caja para que caiga
        {
            colision = false;
            transform.parent = null;
        }

    }


    // metodo para detectar colision con el gancho del player
    void OnTriggerEnter2D(Collider2D other)
    {   
        MovGancho hook = other.gameObject.GetComponent<MovGancho>();
        if (hook != null)
        {
            if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 2, Mask))  // detectar si se esta en contacto con el suelo 
            {
                colision = true;
            }
            else
            {
                colision = false;
            }           
        }
    }   
}
  
