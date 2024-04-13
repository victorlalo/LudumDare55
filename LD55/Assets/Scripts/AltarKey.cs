using UnityEngine;
using DG.Tweening;

public class AltarKey : MonoBehaviour, IInteractable
{
	void Start()
	{
		
	}
	
	private void OnEnable() 
	{
		transform.DOLocalMoveY(1f, 1.25f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}
	
	private void OnDisable() 
	{
		transform.DOKill();
	}
	
	void Update()
	{

	}
	
	public void OnInteract()
	{
		// EventManager.Inventory.OnKeyPickup(this);
		Inventory.Instance.AddKey(this);
	}
}
