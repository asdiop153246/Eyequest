using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// Base class
public class EnemyAI : MonoBehaviour
{
    protected float attackPower;
    protected GameManager gameManager;
    public TextMeshProUGUI ActionText;
    public GameObject _Highlight;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ActionText = GameObject.Find("SkillCastText").GetComponent<TextMeshProUGUI>();
    }

    public virtual void TakeTurn()
    {
        Debug.Log($"{gameObject.name} is thinking...");
        Invoke(nameof(PerformAction), 3f);
    }

    protected virtual void PerformAction()
    {
        GameObject target = gameManager.GetRandomPlayer();
        if (target != null)
        {
            attackPower = 100f;
            Debug.Log($"{gameObject.name} attacks {target.name} for {attackPower} damage.");
            ActionText.text = $"{gameObject.name} used Normal Attack!";
            Player player = target.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(attackPower);
        }

        Invoke(nameof(EndTurn), 1.5f);
    }

    protected void EndTurn()
    {
        ActionText.text = "";
        gameManager.NextTurn();
    }

    void OnMouseDown()
    {
        if (gameManager != null && gameManager.currentTurnIndex == 0)
        {
            gameManager.SelectTarget(this.gameObject);
        }
    }
}

