using UnityEngine;
using UnityEngine.Networking;

public class CollectibleScript : NetworkBehaviour 
{
	public virtual string Name
	{
		get { return "NoNameCollectable"; }
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
	    if (!isServer)
	        return;

		var heroObject = collision.gameObject;
		var heroScript = heroObject.GetComponentInParent<HeroScript> ();

		if (heroScript == null)
			return;

		AddCollectableToHero (heroScript);
		CollectCollectible ();
        PlayCollectableSound(heroObject);
    }

	protected virtual void AddCollectableToHero (HeroScript heroScript)
	{
		Debug.Log (heroScript.stevesName + " colected " + Name);
	}

    protected virtual void PlayCollectableSound(GameObject hero)
    {

    }

    protected virtual void CollectCollectible ()
	{
		Destroy (gameObject);
	}
}
