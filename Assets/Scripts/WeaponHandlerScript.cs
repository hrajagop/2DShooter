using UnityEngine;
using UnityEngine.Networking;

public class WeaponHandlerScript : NetworkBehaviour 
{
	public GameObject bulletPrefab;
	private int ammoAmount = 1000;
    private Animator anim;

    [ClientCallback]
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
	[ClientCallback]
	void Update()
	{
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown(KeyCode.Space))	
		{
			anim.SetBool("Shoot", true);		
			CmdSpawnBullet();
		}
	}

	[Command]
	private void CmdSpawnBullet()
	{
		if (ammoAmount <= 0) 
		{
			Debug.Log ("Oh, sh~! You're out of bullets. Do you have a knife? Sh**! Run!");
			return;
		}

		ammoAmount--;

		var spawnPosition = transform.position + Mathf.Sign(transform.localScale.x) * transform.right + 0.1f * transform.up;
		var shootingDirection = Mathf.Sign(transform.localScale.x) * gameObject.transform.right;

		BulletScript.SpawnBullet(gameObject, bulletPrefab, spawnPosition, shootingDirection);
	}

	public void AddAmmo(int additionalAmmo)
	{
		ammoAmount += additionalAmmo;
	}
}
