using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBar;
    public Slider healthBarSlider;

    public GameObject lootDrop;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    public void DealDamage(float damage)
    {
        audioManager.soundEnemyDamage();
        healthBar.SetActive(true);
        health -= damage;
        CheckDeath();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverHealth();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private void CheckOverHealth()
    {
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            if (gameObject.CompareTag("Boss"))
            {
                DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

                if(dialogueManager.dialogueState == DialogueManager.DialogueState.ThirdRundCompleted)
                {
                    FindAnyObjectByType<GameManager>().SendToYardAfterWin();
                }
                else
                {
                    FindAnyObjectByType<BossDialogue>().TriggerDialogue();
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<AIChase>().enabled = false;
                }
            }
            else
            {
                Instantiate(lootDrop, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
    }

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }
}
