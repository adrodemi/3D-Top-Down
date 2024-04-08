using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Interactable
{
    public override void Interact(GameObject subject)
    {
        Player.Instance.Heal();
        Player.Instance.PickUp();
        print(gameObject.name);
    }
}