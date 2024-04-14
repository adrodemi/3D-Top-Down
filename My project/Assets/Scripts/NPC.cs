using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    [SerializeField] private Quest quest;
    public override void Interact(GameObject subject)
    {
        quest.StartQuest();
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}