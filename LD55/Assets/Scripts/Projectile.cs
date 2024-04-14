using UnityEngine;

public class Projectile : MonoBehaviour, IDamageDealer
{
	Rigidbody rb;
	float Damage = 10f;
	
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	public void Initialize(float damage)
	{
		Damage = damage;
	}
	
	public void Activate()
	{
		rb.AddForce(transform.forward * 50f, ForceMode.Impulse);
		Invoke("Deactivate", 2f);
	}	
	
	public void Deactivate()
	{
		Destroy(gameObject);
	}
	
	public float GetDamage()
	{
		return Damage;
	}
}