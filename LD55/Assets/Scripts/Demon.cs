using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{	
	Animator anim;
	
	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
		// anim.SetTrigger("Hidden");
	}

	void Update()
	{
		
	}
	
	public void Summon()
	{
		anim.SetTrigger("Summon");
	}
}
