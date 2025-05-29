using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Monster Settings")]
    public GameObject[] monsterPrefabs; // Assign different monster types in Inspector
    public Transform[] spawnPoints;     // Predefined spawn positions
    public int numberOfMonsters = 3;

    private List<GameObject> spawnedMonsters = new List<GameObject>();
    private int currentTurnIndex = 0;

    void Start()
    {
        SpawnMonsters();
        StartTurn();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            int prefabIndex = i % monsterPrefabs.Length; // Loop if fewer prefabs than spawns
            GameObject monster = Instantiate(monsterPrefabs[prefabIndex], spawnPoints[i].position, Quaternion.identity);
            monster.transform.Rotate(0,180,0); // Rotate to face the camera
            spawnedMonsters.Add(monster);
        }
    }

    void StartTurn()
    {
        if (spawnedMonsters.Count == 0)
        {
            Debug.Log("No monsters available.");
            return;
        }

        GameObject currentMonster = spawnedMonsters[currentTurnIndex];
        Debug.Log($"Turn for: {currentMonster.name}");
        // You could enable movement, AI, or attack logic here

        // Example: Proceed to next turn after 2 seconds
        Invoke(nameof(NextTurn), 2f);
    }

    void NextTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % spawnedMonsters.Count;
        StartTurn();
    }

    public List<GameObject> GetMonsters()
    {
        return spawnedMonsters;
    }
}
