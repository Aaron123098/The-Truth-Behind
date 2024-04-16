using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            //if (collision.GetComponent<EnemyRecieveDamage>() != null)
            //{
            //    collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            //}
            Destroy(gameObject);
        }
    }
}
