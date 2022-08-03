using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
	public int damage = 5;
	public string layerToCollideWith;
	public bool canHit = true;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != LayerMask.NameToLayer(layerToCollideWith))
			return;
		var opponentAnimator = other.transform.parent.GetComponentInParent<Animator>();
		opponentAnimator.SetInteger("damageIntake", damage);
		opponentAnimator.Play("takeHit");
		GetComponent<Collider>().enabled = false;
		canHit = false;
	}
}
