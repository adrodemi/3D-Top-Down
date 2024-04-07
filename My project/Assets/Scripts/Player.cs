using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(AgentMotor))]
public class Player : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private LayerMask movableMask;
    private AgentMotor motor;

    [SerializeField] private Interactable focus;

    private void Start()
    {
        mainCamera = Camera.main;
        motor = GetComponent<AgentMotor>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movableMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }
    private void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        motor.FollowTarget(focus);
    }
    private void RemoveFocus()
    {
        focus = null;
        motor.StopFollowingTarget();
    }
}