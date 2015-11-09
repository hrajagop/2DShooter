using UnityEngine;
using System.Collections;

public class EnemyScript : MortalObjectScript 
{
	public string enemyName = "Dumbface";

	public override int MaxHealth
	{
		get { return 3; }
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		healthMessage.text = MaxHealth.ToString();
	}

	protected override void HandleDeath ()
	{
		Debug.Log(enemyName + " was killeficated. In a name of SCIENCE!!!");
	}
}
