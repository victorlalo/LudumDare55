using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float speed = 5f;
	[SerializeField] float turnSpeed = 100f;
	bool isControllable = true;
	Vector3 movement;
	
	Rigidbody rb;
	Animator anim;
	
	IInteractable interactable;
	public bool canInteract = false;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		movement = Vector3.zero;
		
		interactable = null;
	}

	void FixedUpdate()
	{
		UpdateMovement();
	}
	
	void UpdateMovement()
	{
		if (movement != Vector3.zero && isControllable)
		{
			Quaternion targetRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
			Vector3 move = movement.normalized * speed * Time.fixedDeltaTime;
			rb.MovePosition(transform.position + move);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
			anim.SetBool("IsWalking", true);
		
			
			
		}
		else
		{
			anim.SetBool("IsWalking", false);
		}
		
	}
	
	void OnMove(InputValue val)
	{
		Vector2 m = val.Get<Vector2>();
		movement.x = m.x;
		movement.z = m.y;
	}
	
	void OnInteract()
	{
		if (isControllable && interactable != null)
		{
			interactable.OnInteract();
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		IInteractable i = other.GetComponent<IInteractable>();
		if (i != null)
		{
			interactable = i;
			canInteract = true;
			
			// show UI interact prompt
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		IInteractable i = other.GetComponent<IInteractable>();
		if (i != null)
		{
			interactable = null;
			canInteract = false;
			
			// hide UI interact prompt
		}
	}
}
