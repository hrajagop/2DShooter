using UnityEngine;
using UnityEngine.Networking;

public class Destroyer : NetworkBehaviour 
{
	public void DestroyGameObject()
	{
		Destroy(gameObject);
	}

	public void DisbleGameObject()
	{
		gameObject.SetActive(false);
	}
}
