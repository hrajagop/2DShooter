using UnityEngine;

public class HeroScript : MortalObjectScript 
{
	public string stevesName = "Steve";

    public override int MaxHealth
    {
        get { return 5; }
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        healthMessage.text = MaxHealth.ToString();
    }

    protected override void HandleDeath ()
	{
		GetComponent<Animator>().SetBool("Die", true);
		Debug.LogError(stevesName + " died the way he lived - being a potato");
	}

	public void Start()
	{
		Debug.Log("This is " + stevesName + ". Less known Mario brother. He is an accountant. Likes cats, long walks on a beach and money. Cheese juggling aficionado. Capricorn.");
	}
}
