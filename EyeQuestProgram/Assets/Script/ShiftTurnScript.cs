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
        if (gameManager.spawnedMonsters.Count >= 0)
        {
            Debug.LogWarning("No monsters spawned. Please ensure monsters are spawned before shifting turns.");
            for (int i = 0; i < gameManager.spawnedMonsters.Count; i++)
            {
                GameObject turnUI = Instantiate(TurnPrefab, topUIParent);
                UnityEngine.UI.Image imageComponent = turnUI.GetComponent<UnityEngine.UI.Image>();
                if (imageComponent != null && i < MonsterUISprite.Count)
                {
                    imageComponent.sprite = MonsterUISprite[gameManager.spawnedMonsters[i].GetComponent<EnemyAI>()._MonsterID];
                }
                else
                {
                    Debug.LogWarning("Image component missing or sprite index out of range.");
                }
            }
            return;
        }
    }
    public void ShiftTurnUI()
    {
        if (topUIParent.childCount == 0) return;

        Transform currentTurn = topUIParent.GetChild(0);
        currentTurn.SetSiblingIndex(topUIParent.childCount - 1);
    }
}
