using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
	float attackDamage = 10f;
	ParticleSystem attackParticles;
	Collider attackCollider;
	
	bool canAttack = true;
	
	float cooldownTime = .75f;
	SimpleTimer attackTimer;
	
	void Awake()
	{
		attackTimer = new SimpleTimer(cooldownTime);
		attackTimer.OnTimerEnd += SetCanAttack;
	}
	
	void Start()
	{
		attackCollider = GetComponent<Collider>();
		attackParticles = GetComponent<ParticleSystem>();
		attackParticles.Clear();

	}
	
	private void OnEnable() 
	{
		
	}
	
	private void OnDisable() 
	{
		attackTimer.OnTimerEnd -= SetCanAttack;
	}
	
	void Update()
	{
		attackTimer.Tick();
	}
	
	public void SetDamage(float damage)
	{
		attackDamage = damage;
	}
	
	public float GetDamage()
	{
		return attackDamage;
	}

	public void Activate()
	{
		if (!canAttack)
		{
			return;
		}
		
		canAttack = false;
		attackTimer.Reset();
		
		attackParticles.Play();
		attackCollider.enabled = true;
		
		Invoke("Deactivate", 0.5f);
	}
	
	public void Deactivate()
	{
		attackParticles.Stop();
		attackCollider.enabled = false;
		// gameObject.SetActive(false);
	}
	
	void SetCanAttack()
	{
		canAttack = true;
	}
}
