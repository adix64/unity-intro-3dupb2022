using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
	//apelata o data, la initializare
	void Start()
    {
		GetCommonComponents();
    }
	//apelata de N ori pe secunda, N fluctuant
	void Update()
	{
		if (!isAlive)
			return;
		GetMoveDir();

		FighterUpdate();

		HandleJump();

		if (Input.GetButtonDown("Fire1"))
			animator.SetTrigger("Punch");
	}

	private void HandleJump()
	{
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
		}
		if (transform.position.y < minimumRespawnY)
			transform.position = initPos;
	}

	private void GetMoveDir()
	{
		float x = Input.GetAxis("Horizontal");// -1 pentru tasta A, 1 pentru tasta D, 0 altfel
		float z = Input.GetAxis("Vertical");// -1 pentru tasta S, 1 pentru tasta W, 0 altfel
		Vector3 cameraFwd_xOz = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
		Vector3 cameraRight_xOz = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

		moveDir = x * cameraRight_xOz + z * cameraFwd_xOz;
		moveDir = moveDir.normalized * Mathf.Max(Mathf.Abs(moveDir.x), Mathf.Abs(moveDir.z));
	}
}