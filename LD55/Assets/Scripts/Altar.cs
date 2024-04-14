using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Altar : MonoBehaviour, IInteractable
{
	public Demon demonPrefab;
	[SerializeField] Transform spawnPoint;
	
	Inventory inventory;
	
	void Start()
	{
		inventory = Inventory.Instance;
	}

	void Update()
	{
		
	}
	
	public void OnInteract()
	{
		if (inventory.SoulCount < 10)
		{
			Debug.Log("Not enough souls");
			return;
		}
		
		EventManager.Inventory.OnSpendSouls(10);
		SummonDemon(demonPrefab);
	}
	
	public void SummonDemon(Demon demonPrefab)
	{
		Demon d = Instantiate(demonPrefab, spawnPoint.position, Quaternion.identity);
		d.Summon();
	}
}
