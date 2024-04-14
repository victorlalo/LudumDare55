using UnityEngine;
using DG.Tweening;

public class AltarKey : MonoBehaviour, IPickupable
{
	bool isHoveringTowardsPlayer = false;
	
	private void OnEnable() 
	{
		// transform.DOLocalMoveY(1f, 1.25f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}
	
	private void OnDisable() 
	{
		// transform.DOKill();
	}
	
	void Update()
	{
		if (isHoveringTowardsPlayer)
		{
			transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 5f * Time.deltaTime); // Move towards player's inventory
		}
	}
	
	public void OnPickup(GameObject target)
	{
		// EventManager.Inventory.OnKeyPickup(this);
		// Inventory.Instance.AddKey(this);
		isHoveringTowardsPlayer = true;
	}
	
	private void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.GetComponent<PlayerController>())
		{
			isHoveringTowardsPlayer = false;
			gameObject.SetActive(false);
		}
	}
}
