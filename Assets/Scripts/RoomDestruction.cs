using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDestruction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ClosedGround")) Destroy(collision.gameObject); 
    }
}