using System;
using UnityEngine;

public static class EventManager
{
	public static class Inventory
	{
		public static event Action<AltarKey> KeyPickup;
		public static void OnKeyPickup(AltarKey key) => KeyPickup?.Invoke(key);
	}
	
	public static class Ability
	{
		public static event Action<Abilities> AbilityUnlock;
		public static void OnAbilityUnlock(Abilities ability) => AbilityUnlock?.Invoke(ability);
	}
}
