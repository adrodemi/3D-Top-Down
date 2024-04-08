using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactRadius = 2f;
    public bool isFocus = false;
    private Transform subject;

    private bool hasinteracted = false;

    public abstract void Interact();
    private void Update()
    {
        if (isFocus && !hasinteracted)
        {
            float distance = Vector3.Distance(transform.position, subject.position);
            if (distance <= interactRadius)
            {
                Interact();
                hasinteracted = true;
            }
        }
    }
    public void OnFocused(Transform subjectTransform)
    {
        isFocus = true;
        subject = subjectTransform;
        hasinteracted = false;
    }
    public void OnDefocused()
    {
        isFocus = false;
        subject = null;
        hasinteracted = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}