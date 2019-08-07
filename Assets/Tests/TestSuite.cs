using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class TestSuite
{
    private GameObject game;
    private GameManager gameManager;
    private Player player;
    [SetUp]
    public void Setup()
    {
        // Load and spawn Game prefab
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(prefab);
        // Get GameManager
        gameManager = game.GetComponent<GameManager>();
        player = game.GetComponentInChildren<Player>();

    }

    [UnityTest]
    public IEnumerator GamePrefabLoaded()
    {
        yield return new WaitForEndOfFrame();

        // Game object should exist at this point in time
        Assert.NotNull(game);
    }

    [UnityTest]
    public IEnumerator PlayerExists()
    {
        yield return new WaitForEndOfFrame();
        Assert.NotNull(player, "No player dawg");
    }

    [UnityTest]
    public IEnumerator ItemCollectedAndScoreAdded()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        Vector3 playerPosition = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, playerPosition, Quaternion.identity);
        int oldScore = gameManager.score;
        //score.Score += newScore;
        //Assert.IsTrue(oldScore != newScore);
        //score.score += score;

        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        int newScore = gameManager.score;

        Assert.IsTrue(newScore > oldScore);
    }
    [UnityTest]
    public IEnumerator PlayerShoots()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        Vector3 playerPosition = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, new Vector3(playerPosition.x,playerPosition.y,playerPosition.z+3), Quaternion.identity);
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(1);
        Assert.IsTrue(player.Shoot());
    }
    [UnityTest]
    public IEnumerator Jumped()
    {
        //rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
        yield return new WaitForFixedUpdate();
    }
    [UnityTest]
    public IEnumerator PlayerMoves()
    {
        player = game.GetComponentInChildren<Player>();
        Vector3 oldPlayerPosition = player.transform.position;

        player.Move(2, 2);
        //Player Transform Vector3  float

        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        Vector3 newPlayerPosition = player.transform.position;

        Assert.IsTrue(oldPlayerPosition != newPlayerPosition);
     
    }
    [UnityTest]
    public IEnumerator ItemCollidesWithPlayer()
    {
        //Item item = gameManager.itemManager.GetItem(0);
        //item.transform.position = player.transform.position;

        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        Vector3 playerPosition = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, playerPosition, Quaternion.identity);

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        Assert.IsTrue(item == null);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game);
    }
}
