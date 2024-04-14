using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AgentMotor))]
public abstract class Character : Interactable
{
    [SerializeField] protected int maxHealth = 100;  
    [SerializeField] protected int currentHealth;
    [SerializeField] private int damage = 20;
    [SerializeField] private float timeBeforeAttack = 1f;
    [SerializeField] public bool canAttack = true;
    protected AgentMotor motor;

    private void Start()
    {
        motor = GetComponent<AgentMotor>();
        currentHealth = maxHealth;
    }
    public override void Interact(GameObject subject)
    {
        StartCoroutine(OnInteracting(subject));
    }
    private IEnumerator OnInteracting(GameObject subject)
    {
        var character = subject.GetComponent<Character>();
        if (character != null)
        {
            if (character.canAttack)
            {
                while (isFocus && subject != null)
                {
                    if (Vector3.Distance(transform.position, subject.transform.position) <= interactRadius)
                    {
                        print($"{subject.name} hit {gameObject.name}");
                        TakeDamage(character.damage);
                        character.Attack();
                        yield return new WaitForSeconds(character.timeBeforeAttack);
                    }
                    yield return null;
                }
            }
        }
    }
    public void Attack()
    {
        motor.StartAttack(timeBeforeAttack);
    }
    private IEnumerator AttackCooldown(float timeBeforeAttack)
    {
        canAttack = false;
        yield return new WaitForSeconds(timeBeforeAttack);
        canAttack = true;
    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print($"Health {gameObject.name}: {currentHealth}");
        if (currentHealth <= 0f)
            Die();
    }
    protected abstract void Die();
}