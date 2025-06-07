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
    
    public List<GameObject> spawnedMonsters = new List<GameObject>();
    [Header("Player Settings")]
    public List<GameObject> players;
    public bool isReadyToAttack = false; // Flag to check if player is ready to attack
    [Header("Turn Management")]
    public int currentTurnIndex = 0;
    public TextMeshProUGUI turnText;

    public GameObject _EndgamePanel;
    void Start()
    {
        SpawnMonsters();
        StartTurn();
    }

    public void startGame()
    {
        // Reset game state
        currentTurnIndex = 0;
        spawnedMonsters.Clear();
        isReadyToAttack = false;

        // Clear previous monsters and players
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            Destroy(monster);
        }

        // Reinitialize players and monsters
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
        // Remove null references just in case
        spawnedMonsters.RemoveAll(m => m == null);

        if (turnText != null)
        {
            if (currentTurnIndex == 0)
            {
                turnText.text = "Player's Turn";
                players[0].GetComponent<Player>().takeTurn();
                StartCoroutine(AnimateTurnText());
            }
            else
            {
                if (spawnedMonsters.Count == 0)
                {
                    Debug.Log("No monsters left, ending game.");
                    _EndgamePanel.SetActive(true);
                    return;
                }

                // Ensure valid index
                int monsterIndex = currentTurnIndex - 1;
                if (monsterIndex >= spawnedMonsters.Count)
                {
                    NextTurn(); // Skip to next
                    return;
                }

                GameObject currentMonster = spawnedMonsters[monsterIndex];

                if (currentMonster == null)
                {
                    Debug.LogWarning("Monster reference was null. Skipping turn.");
                    spawnedMonsters.RemoveAt(monsterIndex);
                    NextTurn(); // Skip
                    return;
                }

                turnText.text = $"{currentMonster.name}'s Turn";
                StartCoroutine(AnimateTurnText());

                EnemyAI ai = currentMonster.GetComponent<EnemyAI>();
                ai?.TakeTurn();
            }
        }
        else
        {
            Debug.LogWarning("Turn Text UI is not assigned.");
        }
    }

    public void RemoveMonster(GameObject monster)
    {
        if (spawnedMonsters.Contains(monster))
        {
            spawnedMonsters.Remove(monster);
        }
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
        spawnedMonsters.RemoveAll(monster => monster == null);

        int totalTurns = spawnedMonsters.Count + 1;

        if (totalTurns == 1) // Only player left
        {
            Debug.Log("All monsters defeated.");
            _EndgamePanel.SetActive(true);
            return;
        }

        currentTurnIndex = (currentTurnIndex + 1) % totalTurns;
        FindObjectOfType<ShiftTurnScript>()?.ShiftTurnUI();

        StartTurn();
    }
    public void SelectTarget(GameObject target)
    {
        selectedTarget = target;
        Debug.Log($"Selected target: {target.name}");
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
