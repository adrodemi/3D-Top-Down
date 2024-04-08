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

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private bool canAttack = false;
    [SerializeField] private GameObject swordObject;

    public static Player Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        mainCamera = Camera.main;
        motor = GetComponent<AgentMotor>();
        currentHealth = maxHealth / 2;
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
        if (focus != newFocus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(focus);
        }
        focus.OnFocused(transform);
    }
    private void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }
    public void Heal()
    {
        currentHealth = maxHealth;
    }
    public void PickUp()
    {
        motor.StartPickUp();
    }
    public void ActivateSword()
    {
        canAttack = true;
        swordObject.SetActive(true);
    }
}