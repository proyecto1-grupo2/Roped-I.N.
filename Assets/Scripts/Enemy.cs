//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    bool isInmune = false;
//    public float tiempoinmune;
//    //si el objecto que activa el trigger tiene un componente PlayerController, le quita una vida
//    private void OnTriggerStay2D(Collider2D other)
//    {
//        if (other.GetComponent<PlayerController>())
//        {
//            if (!isInmune)
//            {
//                other.GetComponent<PlayerController>().LoseLive();
//                other.SendMessage("EnemyKnockBack", transform.position.x);
//                isInmune = true;

//                Invoke("noInmune", tiempoinmune);
//            }
//        }
//    }
//    void noInmune()
//    {
//        isInmune = false;
//    }
//}