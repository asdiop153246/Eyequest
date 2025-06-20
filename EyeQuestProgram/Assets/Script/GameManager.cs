using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

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
    public GameObject _skillUI;
    public GameObject _TurnBar;
    public GameObject _HpBar;
    public List<GameObject> _PotionIcon;
    public bool _isGameStart;

    public GameObject _Player;
    public enum EnemyTier { Normal, Miniboss, Boss }

    public TextMeshProUGUI PrepareText;

    public GameObject _EndgamePanel;
    [Header("Reward Settings")]
    public GameObject[] Stars;
    public Userdata _userdata;
    void Start()
    {
        //CheckAndRequestCameraPermission();
        _userdata = FindObjectOfType<Userdata>();
        if (_userdata == null)
        {
            Debug.LogError("Userdata not found in the scene.");

        }
        else
        {

            worldIndex = _userdata._CurrentWorld;
            stageIndex = _userdata._CurrentStage;

            _PotionIcon[0].SetActive(false);
            _PotionIcon[1].SetActive(false);
            _PotionIcon[2].SetActive(false);
            _PotionIcon[3].SetActive(false);

            if (_userdata._isUsePotion_A)
            {
                _PotionIcon[0].SetActive(true);
                Userdata.Instance._User.data.booster.booster1 -= 1;
            }

            if (_userdata._isUsePotion_B)
            {
                _PotionIcon[1].SetActive(true);
                Userdata.Instance._User.data.booster.booster2 -= 1;
            }

            if (_userdata._isUsePotion_C)
            {
                _PotionIcon[2].SetActive(true);
                Userdata.Instance._User.data.booster.booster3 -= 1;
            }

            if (_userdata._isUsePotion_D)
            {
                _PotionIcon[3].SetActive(true);
                Userdata.Instance._User.data.booster.booster4 -= 1;
            }

            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._BuyBooster(
           Userdata.Instance._User.data.booster.booster1,
           Userdata.Instance._User.data.booster.booster2,
           Userdata.Instance._User.data.booster.booster3,
           Userdata.Instance._User.data.booster.booster4));


            if (stageIndex == 0)
            {
                stageIndex = 1;
            }

            if (worldIndex == 0)
            {
                worldIndex = 1;
            }
            Debug.Log($"World Index: {worldIndex}, Stage Index: {stageIndex}");
        }
        Player player = players[0].GetComponent<Player>();
        player.ApplyStats();
        CalculateStatsModifier();
        StartCoroutine(DelaybeforeStartGame());

    }

    public bool _isAlreadySelectionSkill;
    void Update()
    {
        if (!_isAlreadySelectionSkill)
        {
            if (selectedTarget == null || currentTurnIndex != 0 || _Player.GetComponent<Player>().isAction == true)
            {
                _skillUI.SetActive(false);
            }
            else
            {
                _skillUI.SetActive(true);
            }
        }
        else
        {
            _skillUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            /*foreach(GameObject x in spawnedMonsters)
            {
                spawnedMonsters.Remove(x);
            }*/

            _EndgamePanel.SetActive(true);
            StartCoroutine(_CalulateReward());
            StartCoroutine(_UpdateLeaderBoard());
            Userdata.Instance.gameObject.GetComponent<UnlockWorldLevel>()._UpdateLevel(CalculateScore(), 3);
            
        }
       
    }

    public void _SelectionSkill()
    {
        _isAlreadySelectionSkill = true;
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
        _HpBar.SetActive(false);
        _TurnBar.SetActive(false);
        PrepareText.transform.parent.gameObject.SetActive(true);
        PrepareText.text = $"World {worldIndex} - Stage {stageIndex}";
        yield return new WaitForSeconds(1f);
        PrepareText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        PrepareText.gameObject.SetActive(true);
        PrepareText.text = "Monsters are gathering...";
        yield return new WaitForSeconds(1f);
        PrepareText.gameObject.SetActive(false);
        PrepareText.transform.parent.gameObject.SetActive(false);
        SpawnMonsters();
        yield return new WaitForSeconds(0.5f);
        _TurnBar.SetActive(true);
        StartCoroutine(GetComponent<ShiftTurnScript>()._DelayCreateShifting());

        yield return new WaitForSeconds(1f);
        PrepareText.transform.parent.gameObject.SetActive(true);
        PrepareText.gameObject.SetActive(true);
        PrepareText.text = "Prepare for Battle!";
        yield return new WaitForSeconds(1f);
        PrepareText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        PrepareText.gameObject.SetActive(true);
        PrepareText.text = "Get Ready!";
        yield return new WaitForSeconds(1f);
        PrepareText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        PrepareText.gameObject.SetActive(true);
        PrepareText.text = "Fight!";
        yield return new WaitForSeconds(1f);
        PrepareText.gameObject.SetActive(false);
        PrepareText.transform.parent.gameObject.SetActive(false);
        _HpBar.SetActive(true);
        _isGameStart = true;
        StartTurn();

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

    public void restartStage()
    {
        _userdata._CurrentStage = stageIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    void SpawnMonsters()
    {
        EnemyTier currentTier = GetTierForCurrentStage();
        numberOfMonsters = GetAmountofMonsterForCurrentStage();

        // Determine which monster prefabs are allowed based on stageIndex
        int maxPrefabIndex = GetMaxMonsterIndexForStage(stageIndex); //stageIndex for gameplay

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
        return 8;
        // if (stage <= 2) return 0;
        // else if (stage <= 5) return 1;
        // else return Mathf.Min(2, monsterPrefabs.Length - 1);
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

                    StartCoroutine(_EndGame());
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

    public IEnumerator _EndGame()
    {
        _EndgamePanel.SetActive(true);
        _CurrentScore = CalculateScore();
        _CurrentStar = CalculateEndGameRewards();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(_CalulateReward());
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(_UpdateLeaderBoard());
        yield return new WaitForSeconds(0.2f);
        Userdata.Instance.gameObject.GetComponent<UnlockWorldLevel>()._UpdateLevel(_CurrentScore, _CurrentStar);
    }

    public TMPro.TextMeshProUGUI _ScoreUI;
    public int _CurrentScore;
    public int _CurrentStar;
    int CalculateScore()
    {
        bool levelCleared;
        Player player = players[0].GetComponent<Player>();
        if (spawnedMonsters.Count == 0)
        {
            levelCleared = true;
        }
        else
        {
            levelCleared = false;
        }


        if (!levelCleared)
            return 0;

        float clampedHealth = Mathf.Clamp(((float)player.stats.currentHealth / player.stats.maxHealth), 0f, 100f);

        int baseScore = 5000; // คะแนนจากการผ่านฉาก
        int hpScore = Mathf.RoundToInt((clampedHealth / 100f) * 5000f); // คะแนนจากพลังชีวิต
        _ScoreUI.text = (baseScore + hpScore) + "";
        _CurrentScore = (baseScore + hpScore);
        return baseScore + hpScore; // รวมเป็นคะแนนสุดท้าย
    }

    public List<GameObject> _RewardIcon;
    IEnumerator _CalulateReward()
    {
        _RewardIcon[0].SetActive(false);
        _RewardIcon[1].SetActive(false);
        _RewardIcon[2].SetActive(false);

        int _Vision = Random.Range(10, 100);
        int _Gem = 0;
        int _Gold = Random.Range(10, 100);

        yield return new WaitForSeconds(1f);
        _RewardIcon[0].SetActive(true);
        _RewardIcon[0].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = _Gold + "";
        Userdata.Instance._User.data.currency.gold += _Gold;
        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(0, 0));

        yield return new WaitForSeconds(1f);
        _RewardIcon[1].SetActive(true);
        _RewardIcon[1].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = _Vision + "";
        Userdata.Instance._User.data.currency.vision_point += _Vision;
        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(0, 2));

        yield return new WaitForSeconds(1f);
        /*_RewardIcon[2].SetActive(true);
        _RewardIcon[2].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Random.Range(10, 100) + "";
        Userdata.Instance._User.data.currency.gem += _Gem;
        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(0, 1));*/

    }
    public int starcount;
public float starDelay = 0.7f; // time between each star popping out

    public int CalculateEndGameRewards()
    {
        if (Stars.Length < 3) return 0;

        foreach (GameObject star in Stars)
        {
            star.SetActive(false);
        }

        starcount = 0;

        Player player = players[0].GetComponent<Player>();
        float healthPercent = (float)player.stats.currentHealth / player.stats.maxHealth;

        bool star1 = spawnedMonsters.Count == 0;
        bool star2 = healthPercent >= 0.3f;
        bool star3 = healthPercent >= 0.5f;

        if (star1) starcount++;
        if (star2) starcount++;
        if (star3) starcount++;

        StartCoroutine(PopStars(star1, star2, star3));
        return starcount;
    }
    private IEnumerator PopStars(bool s1, bool s2, bool s3)
    {
        yield return new WaitForSeconds(starDelay);
        if (s1)
        {
            Stars[0].SetActive(true);
            yield return new WaitForSeconds(starDelay);
        }

        if (s2)
        {
            Stars[1].SetActive(true);
            yield return new WaitForSeconds(starDelay);
        }

        if (s3)
        {
            Stars[2].SetActive(true);
        }
    }


    public List<GameObject> _UserName;
    public List<GameObject> _Score;
    IEnumerator _UpdateLeaderBoard()
    {
        _UserName[0].SetActive(true);
        _Score[0].SetActive(true);

        _UserName[1].SetActive(true);
        _Score[1].SetActive(true);

        _UserName[2].SetActive(true);
        _Score[2].SetActive(true);

        var leaderboard = Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].Leaderboard;

        // เตรียม List ชั่วคราว
        List<string> tempNames = new List<string>();
        List<int> tempScores = new List<int>();

        bool foundSameName = false;

        for (int i = 0; i < leaderboard.Count; i++)
        {
            string name = leaderboard[i].playerName;
            int score = leaderboard[i].playerScore;

            // ถ้าชื่อซ้ำ
            if (name == Userdata.Instance._User.data.user.name)
            {
                foundSameName = true;

                // ถ้าคะแนนใหม่มากกว่า → ใช้คะแนนใหม่
                if (_CurrentScore > score)
                {
                    tempNames.Add(Userdata.Instance._User.data.user.name);
                    tempScores.Add(_CurrentScore);
                }
                else
                {
                    tempNames.Add(name);
                    tempScores.Add(score);
                }
            }
            else
            {
                // ชื่อไม่ซ้ำ → เก็บไว้เหมือนเดิม
                tempNames.Add(name);
                tempScores.Add(score);
            }
        }

        // ถ้ายังไม่เคยมีชื่อผู้เล่นนี้เลย → แทรกเข้าให้เหมาะสม
        if (!foundSameName)
        {
            int insertIndex = -1;
            for (int i = 0; i < tempScores.Count; i++)
            {
                if (_CurrentScore > tempScores[i])
                {
                    insertIndex = i;
                    break;
                }
            }

            if (insertIndex == -1)
            {
                tempNames.Add(Userdata.Instance._User.data.user.name);
                tempScores.Add(_CurrentScore);
            }
            else
            {
                tempNames.Insert(insertIndex, Userdata.Instance._User.data.user.name);
                tempScores.Insert(insertIndex, _CurrentScore);
            }
        }

        // จำกัดให้เหลือแค่ 3 อันดับ
        while (tempNames.Count > 3)
        {
            tempNames.RemoveAt(tempNames.Count - 1);
            tempScores.RemoveAt(tempScores.Count - 1);
        }

        // เขียนกลับไปยัง Leaderboard
        for (int i = 0; i < tempNames.Count; i++)
        {
            if (i >= leaderboard.Count)
            {
                leaderboard.Add(new Userdata.LeaderboardEntry()
                {
                    playerName = tempNames[i],
                    playerScore = tempScores[i]
                });
            }
            else
            {
                leaderboard[i].playerName = tempNames[i];
                leaderboard[i].playerScore = tempScores[i];
            }
        }

        // แสดงผลบน UI
        for (int i = 0; i < 3; i++)
        {
            _UserName[i].SetActive(false);
            _Score[i].SetActive(false);

            if (i < tempNames.Count)
            {
                _UserName[i].SetActive(true);
                _Score[i].SetActive(true);

                _UserName[i].GetComponent<TMPro.TextMeshProUGUI>().text = tempNames[i];
                _Score[i].GetComponent<TMPro.TextMeshProUGUI>().text = tempScores[i].ToString();
            }

            yield return new WaitForSeconds(1f);
        }

        //Userdata.Instance._CurrentStage = Userdata.Instance._CurrentStage + 1;

    }
    public void RemoveMonster(GameObject monster)
    {
        if (spawnedMonsters.Contains(monster))
        {
            spawnedMonsters.Remove(monster);
        }

        // Remove its UI
        ShiftTurnScript shiftTurn = FindObjectOfType<ShiftTurnScript>();
        if (shiftTurn != null)
        {
            shiftTurn.RemoveTurnUI(monster);
        }
    }
    private IEnumerator AnimateTurnText()
    {
        if (turnText != null)
        {
            turnText.transform.parent.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            turnText.transform.parent.gameObject.SetActive(false);
        }
    }
    public void NextTurn()
    {
        spawnedMonsters.RemoveAll(monster => monster == null);

        int totalTurns = spawnedMonsters.Count + 1;

        if (totalTurns == 1) // Only player left
        {
            Debug.Log("All monsters defeated.");
            StartCoroutine(_EndGame());

            return;
        }

        currentTurnIndex = (currentTurnIndex + 1) % totalTurns;
        FindObjectOfType<ShiftTurnScript>()?.ShiftTurnUI();

        StartTurn();
    }
    public void SelectTarget(GameObject target)
    {
        if (!_isGameStart) return;
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
        _EndgamePanel.SetActive(false);
        Player _player = players[0].GetComponent<Player>();
        _player.ApplyStats();
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
    

        void CheckAndRequestCameraPermission()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Debug.Log("Camera permission already granted.");
            OnCameraPermissionGranted();
        }
        else
        {
            Debug.Log("Camera permission NOT granted. Requesting now...");
            Permission.RequestUserPermission(Permission.Camera);
            // We will wait for user's response, so listen for callback
        }
    }

    // This method is called automatically by Unity when user responds to permission request
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // When app regains focus, check permission status again
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Debug.Log("Camera permission granted after request.");
                OnCameraPermissionGranted();
            }
            else
            {
                Debug.LogWarning("Camera permission denied.");
                OnCameraPermissionDenied();
            }
        }
    }

    void OnCameraPermissionGranted()
    {
        // Put your code here for what to do if camera permission is available
        Debug.Log("You can now safely access the camera.");
    }

    void OnCameraPermissionDenied()
    {
        // Put your code here for what to do if user denied permission
        Debug.LogWarning("Camera permission denied - app may not work correctly.");
    }
}
