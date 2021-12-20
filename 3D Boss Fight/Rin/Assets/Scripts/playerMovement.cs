// Some stupid rigidbody based movement by Dani

using System;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public Vector3 movement;
	public CharacterController characterController;
	void Start()
	{
		characterController = GetComponent<CharacterController>();
	}

	void Update()
	{

		if (characterController.isGrounded == true)
		{
			movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
			movement.Normalize();
			movement = transform.TransformDirection(movement);
			movement *= 40.0f;

			//if (Input.GetKey(KeyCode.Space) == true)
				//movement.y = 10.0f;
		}
		movement.y -= 80.0f * Time.deltaTime;
		characterController.Move(movement * Time.deltaTime);
		

	}
}