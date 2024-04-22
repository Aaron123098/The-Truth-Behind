using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
            if(!collision.CompareTag("SpawnPoint") && !collision.CompareTag("Destruction")) Destroy(gameObject);
        }
    }
}
