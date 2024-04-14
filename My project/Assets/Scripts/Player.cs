using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private Camera mainCamera;
    [SerializeField] private LayerMask movableMask;
    [SerializeField] private Interactable focus;
    [SerializeField] private GameObject swordObject;
    [SerializeField] private int coinsCount;

    public Text health;

    public Button restartButton;

    public static Player Instance;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
        canAttack = false;
        transform.position = SaveSystem.GetPlayerPosition();
    }
    private void Start()
    {
        motor = GetComponent<AgentMotor>();
        currentHealth = maxHealth / 2;
    }
    protected override void Update()
    {
        health.text = "Health: " + currentHealth;
        base.Update();
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
        focus.OnFocused(gameObject);
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
    protected override void Die()
    {
        Time.timeScale = 0;
        restartButton.gameObject.SetActive(true);
    }
    public void AddCoins(int coins)
    {
        coinsCount += coins;
        print($"You get - {coins} coins \n You have - {coinsCount} coins");
    }
}