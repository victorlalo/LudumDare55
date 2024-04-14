using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Abilities { Dash, Slash, Phase, Decipher, Familiar }

public class PlayerController : MonoBehaviour
{
	[SerializeField] float speed = 10f;
	[SerializeField] float turnSpeed = 500f;
	[SerializeField] float dashForce = 100f;
	[SerializeField] float attackDamage = 10f;
	bool isControllable = true;
	Vector3 movement;
	public Vector3 aim;
	public Vector3 ScreenPos;
	[SerializeField] LayerMask groundLayer;
	
	Rigidbody rb;
	Animator anim;
	AttackController attackController;
	PlayerInput playerInput;
	
	IInteractable interactable;
	public bool canInteract = false;
	
	public bool isGamepad = false;
	
	public Dictionary<Abilities, bool> unlockedAbilities = new Dictionary<Abilities, bool>()
	{
		{ Abilities.Dash, false },
		{ Abilities.Slash, false },
		{ Abilities.Phase, false },
		{ Abilities.Decipher, false },
		{ Abilities.Familiar, false }
	};
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		attackController = GetComponentInChildren<AttackController>();
		attackController.SetDamage(attackDamage);
		playerInput = GetComponent<PlayerInput>();
		// attackController.gameObject.SetActive(false);
		
		movement = Vector3.zero;
		aim = Vector3.zero;
		
		interactable = null;
	}
	
	void OnEnable()
	{
		EventManager.Ability.AbilityUnlock += UnlockAbility;
	}
	
	void OnDisable()
	{
		EventManager.Ability.AbilityUnlock -= UnlockAbility;
	}
	
	void Update()
	{
		
	}

	void FixedUpdate()
	{
		UpdateMovement();
		UpdateAim();
	}
	
	void UpdateMovement()
	{
		if (movement != Vector3.zero && isControllable)
		{
			
			Vector3 move = movement.normalized * speed * Time.fixedDeltaTime;
			rb.MovePosition(transform.position + move);
			
			anim.SetBool("IsWalking", true);
		}
		else
		{
			anim.SetBool("IsWalking", false);
		}
		
	}
	
	void UpdateAim()
	{
		if (isGamepad && aim != Vector3.zero && isControllable)
		{
			Quaternion targetRotation = Quaternion.LookRotation(aim.normalized, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
		}
		else
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
			{
				ScreenPos = hit.point;
			}
			
			ScreenPos.y = transform.position.y;
	
			transform.forward = (ScreenPos - transform.position).normalized;
			// Vector3 lookDir = (transform.position - ScreenPos).normalized;
			
			// float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg;
			// rb.MoveRotation(Quaternion.Euler(0, angle, 0));
		}
	}
	
	public void UnlockAbility(Abilities ability)
	{
		unlockedAbilities[ability] = true;
	}
	
	void OnMove(InputValue val)
	{
		Vector2 m = val.Get<Vector2>();
		movement.x = m.x;
		movement.z = m.y;
	}
	
	void OnAim(InputValue val)
	{
		Vector2 a = val.Get<Vector2>();
		aim.x = a.x;
		aim.z = a.y;
		isGamepad = playerInput.currentControlScheme.Equals("Gamepad");
	}
	
	void OnInteract()
	{
		if (isControllable && interactable != null)
		{
			interactable.OnInteract();
			interactable = null;
		}
	}
	
	void OnDash()
	{
		bool canDash = unlockedAbilities[Abilities.Dash];
		if (isControllable && canDash)
		{
			rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
		}
	}
	
	void OnAttack(InputValue val)
	{
		// bool canAttack = unlockedAbilities[Abilities.Slash];
		// if (isControllable && canAttack)
		// {
		// 	// attackController.gameObject.SetActive(true);
		// 	attackController.Activate();
		// }
		attackController.SetButtonDown(val.isPressed);
	}
	
	void OnPhase()
	{
		bool canPhase = unlockedAbilities[Abilities.Phase];
		if (isControllable && canPhase)
		{
			// phase through objects
		}
	}
	
	void OnDecipher()
	{
		bool canDecipher = unlockedAbilities[Abilities.Decipher];
		if (isControllable && canDecipher)
		{
			// decipher objects
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
