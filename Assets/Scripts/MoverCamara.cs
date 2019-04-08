using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamara : MonoBehaviour
{

    public Transform target;
    Vector3 posicion;
    public int mueveCamara;// cuanto mas grande sea mas se desplaza



    //Zona Muerta
    Vector3 pos;
    public float speed;
    public float xDifference;
    public float yDifference;
    bool mov = false;

    public float movementThreshold;

    //Boundary
    private BoxCollider2D cameraBox;

    // Use this for initialization
    private void Start()
    {
        posicion = new Vector3(0, 0, -5);
        cameraBox = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        //FollowBoundary();
        //AspectRatioBoxChange();
        /*if (target.transform.position.x > transform.position.x)
        {
            xDifference = target.transform.position.x - transform.position.x;
        }
        else
        {
            xDifference = transform.position.x - target.transform.position.x;
        }

        if (target.transform.position.y > transform.position.y)
        {
            yDifference = target.transform.position.y - transform.position.y;
        }
        else
        {
            yDifference = transform.position.y - target.transform.position.y;
        }

        if (xDifference >= movementThreshold || yDifference >= movementThreshold)
        {
            //posicion = new Vector3(target.position.x, target.position.y, -5);
            //transform.position = posicion;
            //pos = transform.position;

            posicion = target.transform.position;
            posicion.z = -5;
            transform.position = Vector3.MoveTowards(transform.position, posicion, speed * Time.deltaTime);
        }*/

        if (Input.GetKey("w"))
        {
           if (transform.position.y < pos.y + mueveCamara)
           {
                mov = true;
                transform.Translate(0, 15 * Time.deltaTime, 0);
           }
        }
        else if (Input.GetKey("s"))
        {
            if (transform.position.y > pos.y - mueveCamara)
            {
                mov = true;
                transform.Translate(0, -15 * Time.deltaTime, 0);
            }
        }
        else
        {
            mov = false;
            posicion = new Vector3(target.position.x, target.position.y, -5);
            transform.position = posicion;
            pos = transform.position;
        }

        
    }

    void AspectRatioBoxChange()
    {
        //16:10 ratio
        if (Camera.main.aspect >= 1.6f && Camera.main.aspect < 1.7f)
        {
            cameraBox.size = new Vector2(23, 14.3f);
        }
        else if(Camera.main.aspect >= 1.7f && Camera.main.aspect < 1.8f)
        {
            cameraBox.size = new Vector2(25.47f, 14.3f);
        }
        else if (Camera.main.aspect >= 1.25f && Camera.main.aspect < 1.3f)
        {
            cameraBox.size = new Vector2(18f, 14.3f);
        }
        else if (Camera.main.aspect >= 1.3f && Camera.main.aspect < 1.4f)
        {
            cameraBox.size = new Vector2(19.13f, 14.3f);
        }
        else if (Camera.main.aspect >= 1.5f && Camera.main.aspect < 1.6f)
        {
            cameraBox.size = new Vector2(21.6f, 14.3f);
        }
    }

    void FollowBoundary()
    {
        if (GameObject.Find("Boundary"))
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                              Mathf.Clamp(target.position.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                                              transform.position.z);
        }
    }

    public bool GetMov()
    {
       return mov;
    }
}
