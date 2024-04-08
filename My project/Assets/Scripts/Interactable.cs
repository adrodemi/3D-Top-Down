using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactRadius = 2f;
    public bool isFocus = false;
    private Transform subject;
    private void Update()
    {
        if (isFocus)
        {
            float distance = Vector3.Distance(transform.position, subject.position);
            if (distance <= interactRadius)
            {
                print("Взаимодействие!");
            }
        }
    }
    public void OnFocused(Transform subjectTransform)
    {
        isFocus = true;
        subject = subjectTransform;
    }
    public void OnDefocused()
    {
        isFocus = false;
        subject = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}