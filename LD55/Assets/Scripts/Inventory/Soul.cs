using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour, IPickupable
{
	bool isHoveringTowardsPlayer = false;
	GameObject hoverTarget;
	
	void Start()
	{
		
	}

	void Update()
	{
		if (isHoveringTowardsPlayer)
		{
			transform.position = Vector3.MoveTowards(transform.position, hoverTarget.transform.position, 60f * Time.deltaTime); // Move towards player's inventory
		}
	}
	
	public void OnPickup(GameObject target)
	{
		hoverTarget = target;
		isHoveringTowardsPlayer = true;
	}
	
	private void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.GetComponent<PlayerController>())
		{
			EventManager.Inventory.OnGatherSoul();
			Destroy(gameObject);
		}
	}
}
