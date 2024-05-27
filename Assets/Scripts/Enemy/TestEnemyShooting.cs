using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    private GameObject player;
    public float damage;
    public float projectileForce;
    public float cooldDown;
    public float distance;
    public float distanceBetween;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootPlayer());
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldDown);
        if(player != null && distance < distanceBetween)
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 myPos = transform.position;
            Vector2 playerPos = player.transform.position;
            Vector2 direction = (playerPos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestEnemyProjectile>().damage = damage;
        }
        StartCoroutine(ShootPlayer());
    }

}
