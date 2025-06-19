using System.Collections;
using System.Collections.Generic;
using Mediapipe;
using UnityEngine;
using UnityEngine.UI;
public class ShiftTurnScript : MonoBehaviour
{
    public Transform topUIParent; // Assign TopUI here in Inspector
    private GameManager gameManager;
    public GameObject TurnPrefab;
    public List<Sprite> MonsterUISprite; // Assign different sprites for each turn UI in Inspector
    public List<GameObject> _activedmonsterUI;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }
    void Start()
    {
        //StartCoroutine(_DelayCreateShifting());
    }
    private Dictionary<GameObject, GameObject> monsterToUIMap = new Dictionary<GameObject, GameObject>();

    public IEnumerator _DelayCreateShifting()
    {
        yield return new WaitForSeconds(0f);

        if (gameManager.spawnedMonsters.Count > 0)
        {
            for (int i = 0; i < gameManager.spawnedMonsters.Count; i++)
            {
                GameObject monster = gameManager.spawnedMonsters[i];
                GameObject turnUI = Instantiate(TurnPrefab, topUIParent);
                _activedmonsterUI.Add(turnUI);

                UnityEngine.UI.Image imageComponent = turnUI.GetComponent<UnityEngine.UI.Image>();
                if (imageComponent != null && i < MonsterUISprite.Count)
                {
                    int id = monster.GetComponent<EnemyAI>()._MonsterID;
                    imageComponent.sprite = MonsterUISprite[id];
                }

                monsterToUIMap[monster] = turnUI;
            }
        }
    }
    public void ShiftTurnUI()
    {
        if (topUIParent.childCount == 0) return;

        Transform currentTurn = topUIParent.GetChild(0);
        currentTurn.SetSiblingIndex(topUIParent.childCount - 1);
    }
    public void RemoveTurnUI(GameObject monster)
    {
        if (monsterToUIMap.ContainsKey(monster))
        {
            GameObject ui = monsterToUIMap[monster];
            _activedmonsterUI.Remove(ui);
            Destroy(ui);
            monsterToUIMap.Remove(monster);
        }
    }
}
