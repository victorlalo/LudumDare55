using UnityEngine;

public class Ability : MonoBehaviour
{
	protected bool isUnlocked;
	
	void Start()
	{
		
	}
	
	public virtual void Activate()
	{
		if (!isUnlocked)
		{
			Debug.Log("Power is locked");
			return;
		}
		Debug.Log("Activating Power");
	}
	
	public virtual void Deactivate()
	{
		if (!isUnlocked)
		{
			Debug.Log("Power is locked");
			return;
		}
		Debug.Log("Deactivating Power");
	}
	
	public void UnlockPower()
	{
		isUnlocked = true;
	}
}
