using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
	// Dialogue dialogue;
	Vector3 hiddenPosition;
	Vector3 activePosition;
	bool isSummoned;
	
	[SerializeField] Abilities abilityGifted;
	
	Animator anim;
	
	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		anim.SetTrigger("Hidden");
	}

	void Update()
	{
		
	}
	
	public void Summon()
	{
		anim.SetTrigger("Summon");
		isSummoned = true;
		
		// TODO: Move unlocking power to happen once dialogue is finished
		EventManager.Ability.OnAbilityUnlock(abilityGifted);
	}
}
