using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Transform cameraTransform;
	public float moveSpeed = 3f;
	public float rotSpeed = 5f;
	public float jumpPower = 5f;
	const float joystickActiveTolerance = 3f * 10e-2f; //0.03
	Vector3 moveDir;
	Rigidbody rigidbody;
	//apelata o data, la initializare
	void Start()
    {
		rigidbody = GetComponent<Rigidbody>();
    }
	//apelata de N ori pe secunda, N fluctuant
	void Update()
	{
		GetMoveDir();

		MovePlayer();

		if (Input.GetButtonDown("Jump"))
			rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);

		ApplyRootRotation();
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
	private void MovePlayer()
	{
		//transform.position += moveDir * moveSpeed * Time.deltaTime; // ilegal pentru Rigidbody
		float velY = rigidbody.velocity.y;
		Vector3 newVelo = moveDir * moveSpeed;
		rigidbody.velocity = new Vector3(newVelo.x, velY, newVelo.z);
	}

	private void ApplyRootRotation()
	{
		Vector3 lookDir = transform.forward;
		if (moveDir.magnitude > joystickActiveTolerance)//avem miscare
			lookDir = moveDir;

		Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
		float rotSlerpFactor = Mathf.Clamp01(rotSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSlerpFactor);
	}
	
}
