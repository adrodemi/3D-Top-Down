using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentAnimator))]
public class AgentMotor : MonoBehaviour
{
    private NavMeshAgent agent;
    private AgentAnimator animator;
    private Transform target;

    private bool isPickUp = false;
    private bool isAttacking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<AgentAnimator>();
    }
    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            LookAtTarget();
        }
        if (!isPickUp && !isAttacking)
        {
            if (agent.velocity.magnitude < agent.speed * 0.2f)
                animator.SetAnimState(AgentAnimator.AnimStates.Idle);
            else
                animator.SetAnimState(AgentAnimator.AnimStates.Running);
        }
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
    public void FollowTarget(Interactable newTarget)
    {
        target = newTarget.transform;
        agent.stoppingDistance = newTarget.interactRadius;
        agent.updateRotation = false;
    }
    public void StopFollowingTarget()
    {
        target = null;
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
    }
    public void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public void StartPickUp()
    {
        StartCoroutine(PickUp());
    }
    private IEnumerator PickUp()
    {
        isPickUp = true;
        animator.SetAnimState(AgentAnimator.AnimStates.PickUp);
        yield return new WaitForSeconds(1f);
        isPickUp = false;
    }
    public void StartAttack(float timeBeforeAttack)
    {
        StartCoroutine(Attack(timeBeforeAttack));
    }
    private IEnumerator Attack(float timeBeforeAttack)
    {
        isAttacking = true;
        animator.SetAnimState(AgentAnimator.AnimStates.Attack);
        yield return new WaitForSeconds(timeBeforeAttack - 0.05f);
        isAttacking = false;
    }
}