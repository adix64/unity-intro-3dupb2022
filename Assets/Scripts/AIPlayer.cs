using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : Fighter
{
	NavMeshAgent agent;
	public float attackRange = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		GetCommonComponents();
    }

    // Update is called once per frame
    void Update()
    {
		if (!isAlive)
			return;
		moveDir = agent.velocity.normalized;
		FighterUpdate();
		agent.SetDestination(opponent.transform.position);
		if (toOpponent.magnitude < attackRange)
			animator.SetTrigger("Punch");
	}
}
