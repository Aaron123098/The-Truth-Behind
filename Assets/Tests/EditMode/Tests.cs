using Cainos.PixelArtTopDown_Basic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;


public class Tests
{
    [Test]
    public void PlayerController()
    {
        GameObject playerTest = new GameObject();
        TopDownCharacterController playerController = playerTest.AddComponent<TopDownCharacterController>();

        playerController.Init(); //Borrar

        Assert.IsTrue(true);
    }

    [Test]
    public void PlayerStats()
    {
        GameObject playerTest = new GameObject();
        PlayerStats playerStats = playerTest.AddComponent<PlayerStats>();

        float health = 100;
        int coins = 10;

        playerStats.health = health;
        playerStats.coins = coins;

        playerStats.DealDamage(10);
        health -= 10;

        playerStats.AddCoins(10);
        coins += 10;

        Assert.IsTrue(playerStats.health == health && playerStats.coins == coins);
    }

    [Test]
    public void SceneController()
    {
        GameObject sceneTest = new GameObject();
        SceneController sceneController = sceneTest.AddComponent<SceneController>();

        sceneController.Init();

        Assert.IsTrue(sceneController.transition != null && sceneController.transitionTime == 1);
    }

    [Test]
    public void RoomSpawner()
    {
        GameObject gameObject = new GameObject();
        RoomSpawner roomSpawner = gameObject.AddComponent<RoomSpawner>();

        roomSpawner.Init();

        Assert.IsTrue(roomSpawner.roomTemplates != null);
    }

    [Test]
    public void RoomTemplates()
    {
        GameObject gameObject = new GameObject();
        RoomTemplates roomTemplates = gameObject.AddComponent<RoomTemplates>();

        roomTemplates.Init();

        Assert.IsTrue(roomTemplates.bottomRooms != null && roomTemplates.leftRooms != null && roomTemplates.topRooms != null &&
            roomTemplates.rightRooms != null && roomTemplates.closedRoom != null);
    }

    [Test]
    public void GameManager()
    {
        GameObject gameObject = new GameObject();
        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.saveLoadSystem = new JSONSaveLoadSystem();

        ConfigurableData configurableData = new ConfigurableData();

        configurableData.srcNumbL1 = 3;
        configurableData.helpL1 = "Ninguna";
        configurableData.srcNumbL2 = 3;
        configurableData.helpL2 = "Ninguna";
        configurableData.awardL3 = 10;
        configurableData.awardL4 = 10;
        configurableData.timeLimitL5 = 120;
        configurableData.awardL6 = 10;
        configurableData.wrongLimitL7 = 5;
        configurableData.awardL8 = 10;
        configurableData.wrongLimitL9 = 5;
        configurableData.timeLimitL10 = 120;
        configurableData.timeLimitL11 = 120;
        configurableData.l1act = false;
        configurableData.l2act = true;
        configurableData.l34act = true;
        configurableData.l5act = false;
        configurableData.l6act = true;
        configurableData.l7act = true;
        configurableData.l8act = true;
        configurableData.l9act = false;
        configurableData.l10act = true;
        configurableData.l11act = true;

        gameManager.saveLoadSystem.LoadFromJSON();

        Assert.AreEqual(configurableData, gameManager.saveLoadSystem.confData);
    }

}
