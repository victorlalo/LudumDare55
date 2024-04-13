using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static Inventory Instance;
	
	public List<AltarKey> keys = new List<AltarKey>();
	// public List<Power> powers;
	
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
	
	// public void OnEnable()
	// {
	// 	EventManager.Inventory.KeyPickup += AddKey;
	// }
	
	// public void OnDisable()
	// {
	// 	EventManager.Inventory.KeyPickup -= AddKey;
	// }
	
	public void AddKey(AltarKey key)
	{
		keys.Add(key);
		key.transform.SetParent(transform);
		key.gameObject.SetActive(false);
		key.transform.position = transform.position;
	}
	
	public List<AltarKey> GetStoredKeys()
	{
		return keys;
	}
	
	public void ClearKeys()
	{
		keys.Clear();
	}
}
