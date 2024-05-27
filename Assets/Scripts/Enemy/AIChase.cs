using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    public Vector2 direction;
    
    public float distance;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool alreadyRunning;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        alreadyRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (!alreadyRunning)
            {
                animator.SetBool("IsMoving", true);
                alreadyRunning = true;
            }
        }
        else
        {
            if (alreadyRunning)
            {
                animator.SetBool("IsMoving", false);
                alreadyRunning= false;
            }
        }

        direction = player.transform.position - transform.position;
        direction.Normalize();

        if(direction.x > 0)
        {
            //Facing E
            spriteRenderer.flipX = false;
        }
        else if(direction.x < 0) 
        {
            //Facing O
            spriteRenderer.flipX = true;
        }
    }
     
}
