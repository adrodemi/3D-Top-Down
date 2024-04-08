using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Interactable
{
    public override void Interact()
    {
        Player.Instance.PickUp();
        Player.Instance.ActivateSword();
        Destroy(gameObject);
    }
}