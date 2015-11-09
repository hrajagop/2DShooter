using UnityEngine;

public class CollectibleAmmo: CollectibleScript
{
	public int additionalAmmo = 100;

	public override string Name
	{
		get { return "Ammo +" + additionalAmmo; }
	}

	protected override void AddCollectableToHero(HeroScript heroScript)
	{
		var weapon = heroScript.gameObject.GetComponent<WeaponHandlerScript> ();

		if (weapon == null)
			return;

		weapon.AddAmmo (additionalAmmo);
	}

    protected override void PlayCollectableSound(GameObject hero)
    {
        var script = hero.GetComponent<HeroController>();
        script.playAmmoPickupSound();
    }
}

