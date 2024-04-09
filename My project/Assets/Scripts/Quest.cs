using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int id;
    new public string name = "Test Quest";
    [Multiline] public string description = "Do something!";
    public int coinsReward = 10;
    public List<GameObject> enemies;
    private int enemyCount;
    public bool isDone = false;
    private bool isActive = false;
    private void Start()
    {
        enemyCount = enemies.Count;
    }
    public void StartQuest()
    {
        if (!isActive)
        {
            QuestsSystem.Instance.activeQuests.Add(this);
            float areaLength = 5f;
            foreach (var enemy in enemies)
            {
                float posX = transform.position.x + (areaLength / 2) * Random.Range(-1, 1);
                float posZ = transform.position.x + (areaLength / 2) * Random.Range(-1, 1);
                var newEnemy = Instantiate(enemy, new Vector3(posX, transform.position.y, posZ), Quaternion.identity);
                newEnemy.GetComponent<Enemy>().quest = this;
            }
            isActive = true;
            print($"Start quest - {name}");
            print($"Desciption - {description}");
            print($"Money for quest - {coinsReward}");
        }
    }
    public void OnEnemyDead()
    {
        enemyCount--;
        if (enemyCount <= 0)
            QuestDone();
    }
    private void QuestDone()
    {
        QuestsSystem.Instance.activeQuests.Remove(this);
        QuestsSystem.Instance.doneQuests.Add(this);
        isDone = true;
        isActive = false;
        Player.Instance.AddCoins(coinsReward);
        print("Quest Done!");
    }
}