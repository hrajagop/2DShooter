using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class MortalObjectScript : NetworkBehaviour 
{
    public Text healthMessage;

    [SyncVar(hook = "OnHealth")]
    public float health;

    void OnHealth(float currentHealth)
    {
		if (healthMessage != null)
			healthMessage.text = currentHealth.ToString();

        if (currentHealth <= 0) 
			HandleDeath ();
    }

    public virtual int MaxHealth
	{
		get { return 1; }
	}

	public void Awake()
	{
		health = MaxHealth;
	}
   
	public virtual void ReceiveDamage(float damage)
	{
		if (!isServer)
			return;
		
		health = health - damage < 0 ? 0 : health - damage;

		RpcDamage(health);
	}

	public virtual void AddHealth(float additionalHealth)
	{
		if (!isServer)
			return;

		health = health + additionalHealth > MaxHealth ? MaxHealth : health + additionalHealth;

		RpcDamage(health);
	}

    [ClientRpc]
    void RpcDamage(float amount)
    {
        Debug.Log("Current health:" + amount);
    }
    
	protected virtual void HandleDeath()
	{
		Debug.LogError(gameObject.name + ": Oh, no! I'm dead!");
	}
}
