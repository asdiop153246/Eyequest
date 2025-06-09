using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Monster Settings")]
    public GameObject[] monsterPrefabs; // Assign different monster types in Inspector
    public Transform[] spawnPoints;     // Predefined spawn positions
    public GameObject selectedTarget;
    public int numberOfMonsters = 3;
    
    private List<GameObject> spawnedMonsters = new List<GameObject>();
    [Header("Player Settings")]
    public List<GameObject> players;
    public bool isReadyToAttack = false; // Flag to check if player is ready to attack
    [Header("Turn Management")]
    public int currentTurnIndex = 0;
    public TextMeshProUGUI turnText;

    void Start()
    {
        SpawnMonsters();
        StartTurn();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            int prefabIndex = Random.Range(0, monsterPrefabs.Length);
            GameObject prefab = monsterPrefabs[prefabIndex];

            GameObject monster = Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
            monster.transform.Rotate(0, 180, 0);

            monster.name = prefab.name;

            spawnedMonsters.Add(monster);
        }
    }
    public GameObject GetRandomPlayer()
    {
        if (players.Count == 0) return null;
        return players[Random.Range(0, players.Count)];
    }
    void StartTurn()
    {
        if (turnText != null)
        {
            if (currentTurnIndex == 0)
            {
                turnText.text = "Player's Turn";
                StartCoroutine(AnimateTurnText());
            }
            else
            {
                GameObject currentMonster = spawnedMonsters[currentTurnIndex - 1];
                turnText.text = $"{currentMonster.name}'s Turn";
                StartCoroutine(AnimateTurnText());
                EnemyAI ai = currentMonster.GetComponent<EnemyAI>();
                if (ai != null)
                {
                    ai.TakeTurn();
                }
            }


        }
        else
        {
            Debug.LogWarning("Turn Text UI is not assigned, skipping turn display.");
        }

        if (spawnedMonsters.Count == 0)
        {
            Debug.Log("No monsters available.");
            return;
        }

        // For Test
        //Invoke(nameof(NextTurn), 2f);
    }

    private IEnumerator AnimateTurnText()
    {
        if (turnText != null)
        {
            turnText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            turnText.gameObject.SetActive(false);
        }
    }
    public void NextTurn()
    {
        //Debug.Log("Total Turn = " + (spawnedMonsters.Count + 1) + ", Current Turn Index = " + currentTurnIndex);
        int totalTurns = spawnedMonsters.Count + 1; 
        currentTurnIndex = (currentTurnIndex + 1) % totalTurns;
        FindObjectOfType<ShiftTurnScript>()?.ShiftTurnUI();
        StartTurn();
    }
    public void SelectTarget(GameObject target)
    {
        selectedTarget = target;
        Debug.Log($"Selected target: {target.name}");

        // Optionally highlight target visually here
        // e.g., change material color or enable outline
        isReadyToAttack = true; // Set flag to indicate player is ready to attack
        foreach (GameObject monster in spawnedMonsters)
        {
            if (monster != null && monster != selectedTarget)
            {
                monster.GetComponent<EnemyAI>()._Highlight.SetActive(false); // Hide highlight for other monsters
            }
            else if (monster != null && monster == selectedTarget)
            {
                monster.GetComponent<EnemyAI>()._Highlight.SetActive(true); // Show highlight for selected monster
            }
        }


        // Optional: Show "Attack" button after selection
    }
    public List<GameObject> GetMonsters()
    {
        return spawnedMonsters;
    }
}
