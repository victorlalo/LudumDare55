public interface IDamageDealer
{
	public float GetDamage();
	
	public void Initialize(float damage);
	public void Activate();
	public void Deactivate();
	
	// public void OnHit();
}