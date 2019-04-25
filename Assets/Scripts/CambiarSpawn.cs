using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarSpawn : MonoBehaviour
{
    public GameObject agua;
    public Transform spawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { agua.GetComponent<DeadZone>().CambioSpawn(spawn); }
    }
}
