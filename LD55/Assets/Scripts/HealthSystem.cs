using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
	[SerializeField] float maxHealth = 100f;
	[SerializeField] float currentHealth;
	
	void Start()
	{
		currentHealth = maxHealth;
	}

	void Update()
	{
		
	}
	
	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Die();
		}
	}
	
	public void Die()
	{
		// drop currency
		// chance to drop health pack
		Destroy(gameObject);
	}
	
	private void OnTriggerEnter(Collider other) 
	{
		IDamageDealer damageDealer = other.GetComponent<IDamageDealer>();
		if (damageDealer != null)
		{
			TakeDamage(damageDealer.GetDamage());
			damageDealer.Deactivate();
		}
	}
}
