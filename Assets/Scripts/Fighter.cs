using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
	public bool isAlive = true;
	public Transform cameraTransform;
	public float moveSpeed = 3f;
	public float rotSpeed = 5f;
	public float jumpPower = 5f;
	public float groundedThreshold = .15f;
	public float minimumRespawnY = -50f;
	public float engagingRange = 5f;
	protected const float joystickActiveTolerance = 3f * 10e-2f; //0.03

	protected Vector3 initPos;
	protected Vector3 moveDir;
	protected Rigidbody rigidbody;
	protected Animator animator;
	protected CapsuleCollider capsule;
	protected bool isGrounded = true;

	public Fighter opponent;
	protected Vector3 toOpponent;
	//apelata o data, la initializare
	protected void GetCommonComponents()
    {
		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		capsule = GetComponent<CapsuleCollider>();
		initPos = transform.position;
    }
	//apelata de N ori pe secunda, N fluctuant
	protected void FighterUpdate()
	{
		CheckIfGrounded();
		SetAnimatorMoveParams();

		toOpponent = opponent.transform.position - transform.position;
		
		ApplyRootRotation();
	}

	private void OnAnimatorMove()
	{
		MovePlayer();
	}
	private void CheckIfGrounded()
	{
		Vector3 bottomCapsuleSphereCenter = transform.position + Vector3.up * (capsule.radius + groundedThreshold);
		Vector3 topCapsuleSphereCenter = transform.position + Vector3.up * (capsule.height - capsule.radius + groundedThreshold);
		isGrounded = Physics.CapsuleCast(bottomCapsuleSphereCenter, topCapsuleSphereCenter,
										 capsule.radius, Vector3.down, groundedThreshold * 2f);
		animator.SetBool("Grounded", isGrounded);
	}
	private void SetAnimatorMoveParams()
	{
		Vector3 characterSpaceMoveDir = transform.InverseTransformVector(moveDir) * 1.2f;
		animator.SetFloat("Forward", characterSpaceMoveDir.z);
		animator.SetFloat("Right", characterSpaceMoveDir.x);
	}
	private void MovePlayer()
	{
		//transform.position += moveDir * moveSpeed * Time.deltaTime; // ilegal pentru Rigidbody
		if (!isGrounded)
			return;
		float velY = rigidbody.velocity.y;
		Vector3 newVelo = animator.deltaPosition / Time.deltaTime * moveSpeed;
		rigidbody.velocity = new Vector3(newVelo.x, velY, newVelo.z);
	}

	private void ApplyRootRotation()
	{
		Vector3 lookDir = transform.forward;
		
		if (moveDir.magnitude > joystickActiveTolerance)//avem miscare
			lookDir = moveDir;

		if (opponent != null && toOpponent.magnitude < engagingRange)
			lookDir = Vector3.ProjectOnPlane(toOpponent, Vector3.up).normalized; //se uita catre oponent daca este la mai putin de engaging range metri

		Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
		float rotSlerpFactor = Mathf.Clamp01(rotSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSlerpFactor);
	}
}
