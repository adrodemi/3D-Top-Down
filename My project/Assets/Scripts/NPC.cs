using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private Quest quest;
    public override void Interact(GameObject subject)
    {
        quest.StartQuest();
    }
}