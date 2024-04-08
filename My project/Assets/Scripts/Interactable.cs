using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactRadius = 2f;
    protected bool isFocus = false;
    protected GameObject subject;

    private bool hasinteracted = false;

    public abstract void Interact(GameObject subject);
    protected virtual void Update()
    {
        if (isFocus && !hasinteracted)
        {
            float distance = Vector3.Distance(transform.position, subject.transform.position);
            if (distance <= interactRadius)
            {
                Interact(subject);
                hasinteracted = true;
            }
        }
    }
    public void OnFocused(GameObject newSubject)
    {
        isFocus = true;
        subject = newSubject;
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