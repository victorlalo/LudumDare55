using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static Inventory Instance;
	public int SoulCount = 0;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	void Start()
	{
		
	}

	void Update()
	{
		
	}
	
	public void OnEnable()
	{
		EventManager.Inventory.GatherSoul += IncrementSoulCount;
		EventManager.Inventory.SpendSouls += SpendSouls;
	}
	
	public void OnDisable()
	{
		EventManager.Inventory.GatherSoul -= IncrementSoulCount;
		EventManager.Inventory.SpendSouls -= SpendSouls;
	}
	
	void IncrementSoulCount()
	{
		SoulCount++;
	}
	
	void SpendSouls(int amount)
	{
		SoulCount -= amount;
	}
	
	private void OnTriggerEnter(Collider other) 
	{
		IPickupable pickup = other.GetComponent<IPickupable>();
		if (pickup != null)
		{
			pickup.OnPickup(this.gameObject);
		}
		
	}
}
