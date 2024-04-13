using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Altar : MonoBehaviour, IInteractable
{
	int numKeysPlaced = 0;
	int numKeysNeeded = 3;
	[SerializeField] Transform[] keyPlacementLocations;
	
	bool hasSummoned = false;
	public Demon demon;
	Vector3 demonHiddenPosition;
	Vector3 demonActivePosition;
	
	void Start()
	{
		demon = GetComponentInChildren<Demon>();
		demonActivePosition = demon.transform.position;
		demonHiddenPosition = demon.transform.position - Vector3.up * 5f;
		demon.gameObject.SetActive(false);
		// demon.transform.position = demonHiddenPosition;
	}

	void Update()
	{
		
	}
	
	public void OnInteract()
	{
		if (hasSummoned)
		{
			// Interact with demon
		}
		else
		{
			List<AltarKey> keys = Inventory.Instance.GetStoredKeys();
			// Check Inventory for needed keys
			if (keys.Count > 0)
			{
				foreach(AltarKey key in keys)
				{
					key.transform.position = keyPlacementLocations[numKeysPlaced].position;
					key.transform.SetParent(this.transform);
					key.gameObject.SetActive(true);
					numKeysPlaced++;
				}
				
				Inventory.Instance.ClearKeys();
			}
			
			if (numKeysPlaced == numKeysNeeded)
			{
				hasSummoned = true;
				// summoning animation
				
				// on competion of animation, activate demon object and bring into center
				Invoke("SummonDemon", 1f);
			}
		}
		
	}
	
	public void SummonDemon()
	{
		demon.gameObject.SetActive(true);
		demon.Summon();
		// demon.transform.DOMoveY(demonActivePosition.y, 2f).SetEase(Ease.OutBack);
	}
}
