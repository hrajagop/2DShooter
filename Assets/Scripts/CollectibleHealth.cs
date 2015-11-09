using UnityEngine;

public class CollectibleHealth : CollectibleScript
{
	public float additionalHealth = 1.0f;

	public override string Name
	{
		get { return "Healt +" + additionalHealth; }
	}
	
	protected override void AddCollectableToHero(HeroScript heroScript)
	{
		heroScript.AddHealth(additionalHealth);
	}

    protected override void PlayCollectableSound(GameObject hero)
    {
        var script = hero.GetComponent<HeroController>();
        script.playHealthPickupSound();
    }
}
