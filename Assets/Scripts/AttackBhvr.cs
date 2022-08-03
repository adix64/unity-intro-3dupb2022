using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBhvr : StateMachineBehaviour
{
	public HumanBodyBones attackBone;
	public float hitboxBeginTimeStamp;
	public float hitboxEndTimeStamp;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var collider = animator.GetBoneTransform(attackBone).GetComponent<Collider>();
		var attackTrigger = collider.GetComponent<AttackTrigger>();
		attackTrigger.canHit = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var collider = animator.GetBoneTransform(attackBone).GetComponent<Collider>();
		var attackTrigger = collider.GetComponent<AttackTrigger>();
		float t = stateInfo.normalizedTime;
		collider.enabled = attackTrigger.canHit && t > hitboxBeginTimeStamp && t < hitboxEndTimeStamp;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
