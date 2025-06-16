using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Monster Settings")]
    public GameObject[] monsterPrefabs;
    public Transform[] spawnPoints;
    public GameObject selectedTarget;
    public int numberOfMonsters;

    public List<GameObject> spawnedMonsters = new List<GameObject>();
    [Header("Player Settings")]
    public List<GameObject> players;
    // Modifier from Player's Items
    public int StrengthItemModifier = 0;
    public int LuckItemModifier = 0;
    public int TenacityItemModifier = 0;
    public bool isReadyToAttack = false;
    [Header("Turn Management")]
    public int currentTurnIndex = 0;
    public TextMeshProUGUI turnText;
    [Header("World Settings")]
    public int worldIndex;
    public int stageIndex;
    public float statsModifier;
    public enum EnemyTier { Normal, Miniboss, Boss }

    public TextMeshProUGUI PrepareText;

    public GameObject _EndgamePanel;
    void Start()
    {
        CalculateStatsModifier();
        StartCoroutine(DelaybeforeStartGame());

    }
    public EnemyTier GetTierForCurrentStage()
    {
        // For simplicity, Wait for input from MenuManager to set worldIndex and stageIndex 
        // รอหน้าเมนูส่งข้อมูลทีหลัง
        if (stageIndex >= 9) return EnemyTier.Boss;
        if (stageIndex >= 8) return EnemyTier.Miniboss;
        return EnemyTier.Normal;
    }
    IEnumerator DelaybeforeStartGame()
    {
        PrepareText.gameObject.SetActive(true);
        PrepareText.text = $"World {worldIndex} - Stage {stageIndex}";
        yield return new WaitForSeconds(2f);
        PrepareText.text = "Monsters are gathering...";
        yield return new WaitForSeconds(2f);
        SpawnMonsters();
        yield return new WaitForSeconds(1f);
        PrepareText.text = "Prepare for Battle!";
        yield return new WaitForSeconds(2f);
        PrepareText.text = "Get Ready!";
        yield return new WaitForSeconds(2f);
        PrepareText.text = "Fight!";
        yield return new WaitForSeconds(1f);
        StartTurn();
        PrepareText.gameObject.SetActive(false);
    }
    void CalculateStatsModifier()
    {
        statsModifier = (worldIndex * 1.4f) + (stageIndex * 0.08f);
        Debug.Log($"Stats Modifier: {statsModifier}");
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
        EnemyTier currentTier = GetTierForCurrentStage();
        numberOfMonsters = GetAmountofMonsterForCurrentStage();

        // Determine which monster prefabs are allowed based on stageIndex
        int maxPrefabIndex = GetMaxMonsterIndexForStage(stageIndex);

        for (int i = 0; i < numberOfMonsters; i++)
        {
            int prefabIndex = Random.Range(0, maxPrefabIndex + 1);
            GameObject prefab = monsterPrefabs[prefabIndex];

            GameObject monster = Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
            monster.transform.Rotate(0, 180, 0);
            monster.name = prefab.name;

            EnemyAI ai = monster.GetComponent<EnemyAI>();
            if (ai != null)
            {
                if (currentTier == EnemyTier.Boss)
                {
                    EnemyTier tier = (i == 0) ? EnemyTier.Boss : EnemyTier.Normal;
                    ai.ApplyTierModifier(tier, statsModifier);
                    monster.name = $"{prefab.name} (Boss)";
                }
                else if (currentTier == EnemyTier.Miniboss)
                {
                    ai.ApplyTierModifier(EnemyTier.Miniboss, statsModifier);
                    monster.name = $"{prefab.name} (Miniboss)";
                }
                else
                {
                    ai.ApplyTierModifier(EnemyTier.Normal, statsModifier);
                }
            }

            spawnedMonsters.Add(monster);
        }
    }
    int GetMaxMonsterIndexForStage(int stage)
    {

        if (stage <= 2) return 0;
        else if (stage <= 5) return 1;
        else return Mathf.Min(2, monsterPrefabs.Length - 1);
    }
    public int GetAmountofMonsterForCurrentStage()
    {
        if (stageIndex <= 2) return 1; // Early stages
        else if (stageIndex <= 5) return 2; // Mid stages
        else if (stageIndex <= 8) return 2; // MiniBoss stages
        else if (stageIndex == 9) return 1; // Boss stage

        return 1;
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


    public void NextStage()
    {
        stageIndex++;

        // Optional: Reset worldIndex if you want to loop or increase it based on stage
        // if (stageIndex > 9)
        // {
        //     worldIndex++;
        //     stageIndex = 1;
        // }

        // Reset turn index and flags
        currentTurnIndex = 0;
        isReadyToAttack = false;

        // Destroy all monsters in scene
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            Destroy(monster);
        }
        spawnedMonsters.Clear();

        foreach (var player in players)
        {
            if (player != null)
            {
                player.GetComponent<Player>().ResetForNewStage();
            }
        }

        CalculateStatsModifier();
        StartCoroutine(DelaybeforeStartGame());
    }
    public void ReturntoMenu()
    {
        // Reset all game state variables
        currentTurnIndex = 0;
        spawnedMonsters.Clear();
        isReadyToAttack = false;
        worldIndex = 0;
        stageIndex = 0;
        statsModifier = 1.0f; // Reset to default

        // Clear all monsters and players in the scene
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            Destroy(monster);
        }

        // Optionally, reset player states if needed
        foreach (var player in players)
        {
            if (player != null)
            {
                player.GetComponent<Player>().ResetForNewStage();
            }
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
