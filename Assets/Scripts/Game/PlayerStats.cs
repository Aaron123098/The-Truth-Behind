using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public  static PlayerStats playerStats;
    
    public float health;
    public float maxHealth;

    public GameObject player;
    public Text healthText;
    public Slider healthSlider;
    public Text coinsText;

    public int coins;

    public GameManager gameManager;
    public GameObject screens;

    public AudioManager audioManager;

    void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);

    }

    void Start()
    {
        

        health = maxHealth;
        SetHealthUI();
        gameManager = GetComponent<GameManager>();
        screens = GameObject.FindGameObjectWithTag("Screens");
        audioManager = FindAnyObjectByType<AudioManager>();


    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealthUI();
        audioManager.soundPlayerDamage();
    }

    public void HealPlayer(float heal)
    {
        health += heal;
        CheckOverhealt();
        SetHealthUI();
    }

    private void CheckOverhealt()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(player);
            if (!gameManager.isGameOver)
            {
                gameManager.GameOver();
            }
        }
    }

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }

    private void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + "/" + Mathf.Ceil(maxHealth).ToString();
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        coinsText.text = "Soles: " + coins.ToString();
        screens.GetComponent<ScoreManager>().scoreText.text = "Puntaje: " + coins.ToString();
        screens.GetComponent<ScoreManager>().score = coins;
    }
}
