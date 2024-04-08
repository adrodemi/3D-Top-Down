using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Interactable
{
    public override void Interact()
    {
        Player.Instance.Heal();
        Player.Instance.PickUp();
    }
}