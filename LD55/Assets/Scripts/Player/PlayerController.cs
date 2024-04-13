using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public enum Abilities { Dash, Slash, Phase, Decipher, Familiar }

public class PlayerController : MonoBehaviour
{
	[SerializeField] float speed = 10f;
	[SerializeField] float turnSpeed = 500f;
	[SerializeField] float dashForce = 100f;
	[SerializeField] float attackDamage = 10f;
	bool isControllable = true;
	Vector3 movement;
	
	Rigidbody rb;
	Animator anim;
	AttackController attackController;
	
	IInteractable interactable;
	public bool canInteract = false;
	
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
		// attackController.gameObject.SetActive(false);
		
		movement = Vector3.zero;
		
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
	
	void OnAttack()
	{
		bool canAttack = unlockedAbilities[Abilities.Slash];
		if (isControllable && canAttack)
		{
			// attackController.gameObject.SetActive(true);
			attackController.Activate();
		}
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
