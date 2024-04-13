using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour, IInteractable
{
	int numKeysPlaced = 0;
	int numKeysNeeded = 3;
	
	void Start()
	{
		
	}

	void Update()
	{
		
	}
	
	public void OnInteract()
	{
		Debug.Log("Interacting with Altar");
		// Check Inventory for needed keys
		
		// If player has the keys, place them on the altar
		// numKeysPlaced++;
		// if (numKeysPlaced == numKeysNeeded)
		// {
		// 	// Summon a demon
		// }
	}
}
