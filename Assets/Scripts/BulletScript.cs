using UnityEngine;
using UnityEngine.Networking;

[DisallowMultipleComponent]
public class BulletScript : NetworkBehaviour 
{
	public float damage = 1.0f;
	public float lifeTime = 3.0f;
	public float speed = 50f;
    public GameObject explode;
	private GameObject shooterObject;

	void OnCollisionEnter2D(Collision2D collision)
	{
	    if (!isServer)
	        return;

		if(CollidedWithOriginalShooter(collision.gameObject) || CollidedWithAFriendlyBullet(collision.gameObject))
			return;

		CmdDealDamage(collision.gameObject);
		BilletHit();
	}

	private bool CollidedWithOriginalShooter(GameObject collisionObject)
	{
		return collisionObject.Equals(shooterObject);
	}

	private bool CollidedWithAFriendlyBullet(GameObject collisionObject)
	{
		var bullet = collisionObject.GetComponent<BulletScript>();

		if(bullet == null)
			return false;

		return shooterObject.Equals(bullet.shooterObject);
	}
	
	[Command]
	protected virtual void CmdDealDamage (GameObject go)
	{
        var mortalScript = go.GetComponentInParent<MortalObjectScript>();

		if (mortalScript != null)
			mortalScript.ReceiveDamage (damage);
    }

	protected virtual void BilletHit()
	{
        OnExplode();
		Destroy (gameObject);
	}
	
    void OnExplode()
    {
        var exposion = Instantiate(explode, transform.position, transform.rotation) as GameObject;
        Destroy(exposion, 0.5f);
        NetworkServer.Spawn(exposion);
    }

	public static void SpawnBullet(GameObject shooter, GameObject bulletPrefab, Vector3 spawnPosition, Vector3 shootingDirection)
	{
		var bullet = (GameObject)Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

		var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
		var bulletConstroller = bullet.GetComponent<BulletScript>();

		bulletConstroller.shooterObject = shooter;
        bulletRigidbody.velocity = bulletConstroller.speed * shootingDirection;
		var currentScale = bullet.transform.localScale;
		currentScale.x *= Mathf.Sign (shootingDirection.x);
		bullet.transform.localScale = currentScale;

		Destroy(bullet, bulletConstroller.lifeTime);

		NetworkServer.SpawnWithClientAuthority(bullet, shooter);
	}
}
