using System;
using UnityEngine;

public static class EventManager
{
	public static class Inventory
	{
		public static event Action<AltarKey> KeyPickup;
		public static void OnKeyPickup(AltarKey key) => KeyPickup?.Invoke(key);
		
		public static event Action GatherSoul;
		public static void OnGatherSoul() => GatherSoul?.Invoke();
		
		public static event Action<int> SpendSouls;
		public static void OnSpendSouls(int amount) => SpendSouls?.Invoke(amount);
	}
	
	public static class Ability
	{
		public static event Action<Abilities> AbilityUnlock;
		public static void OnAbilityUnlock(Abilities ability) => AbilityUnlock?.Invoke(ability);
	}
}
