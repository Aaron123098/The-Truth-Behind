using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player") && !collision.CompareTag("SpawnPoint") && !collision.CompareTag("Destruction"))
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
            {
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
