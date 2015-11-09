using UnityEngine;
using UnityEngine.Networking;

public class HeroController : NetworkBehaviour 
{
    private AudioSource heroAudSrc;
    [SerializeField]
    private AudioClip walking;
    [SerializeField]
    private AudioClip jumping;
    [SerializeField]
    private AudioClip landing;
    [SerializeField]
    private AudioClip laughing;
    [SerializeField]
    private AudioClip ammoPickUp;
    [SerializeField]
    private AudioClip healthPickUp;

    private Rigidbody2D body;
	private float movementSpeed = 10;
    private float jumpForce = 365;

    private Transform groundCheck;

    private Vector3 leftTransform;
    private Vector3 rightTransform;

    private Animator anim;
    private NetworkTransform netTransform;

    [SyncVar(hook = "OnFacing")]
    private bool facingLeft;

    void OnFacing(bool facing)
    {
        transform.localScale = facing ? leftTransform : rightTransform;
    }

    void Awake()
    {
        rightTransform = transform.localScale;
        leftTransform = transform.localScale;
        leftTransform.x *= -1;
    }

    public override void OnStartLocalPlayer()
	{
		base.OnStartLocalPlayer();
		body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        netTransform = GetComponent<NetworkTransform>();
        groundCheck = transform.Find("groundCheck");
        heroAudSrc = body.GetComponent<AudioSource>();
    }

    private void PlayLandingSound()
    {
        heroAudSrc.clip = landing;
        heroAudSrc.Play();
    }

    public void playHealthPickupSound()
    {

        if (heroAudSrc.isPlaying)
        {
            heroAudSrc.Stop();
        }
        heroAudSrc.clip = healthPickUp;
        heroAudSrc.Play();

    }

    public void playAmmoPickupSound()
    {

        if (heroAudSrc.isPlaying)
        {
            heroAudSrc.Stop();
        }
        heroAudSrc.clip = ammoPickUp;
        heroAudSrc.Play();

    }

    private void PlayJumpSound()
    {
        if (!heroAudSrc.isPlaying)
        {
            heroAudSrc.clip = jumping;
            heroAudSrc.Play();
        }
    }

    private void PlayLaughingSound()
    {
        if (!heroAudSrc.isPlaying)
        {
            heroAudSrc.clip = laughing;
            heroAudSrc.Play();
        }
    }

    private void PlayWalkingSound()
    {

        if (!heroAudSrc.isPlaying)
        {
            heroAudSrc.clip = walking;
            heroAudSrc.Play();
        }
    }

    [ClientCallback]
	void FixedUpdate()
	{
		if (!isLocalPlayer)
			return;
		
		var move = Input.GetAxis("Horizontal");
		body.velocity = new Vector2(move * movementSpeed, body.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        if (body.velocity.x < 0 && !facingLeft)
            CmdFlipToLeft();

        if (body.velocity.x > 0)
            CmdFlipToRight();

        if (body.velocity.x != 0)
            PlayWalkingSound();

        if (Input.GetKeyDown(KeyCode.L))
            PlayLaughingSound();

        netTransform.grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetKeyDown(KeyCode.UpArrow) && netTransform.grounded)
        {
            anim.SetBool("Jump", true);
            body.AddForce(new Vector2(0f, jumpForce));
            PlayJumpSound();
        }
    }

    [Command]
    void CmdFlipToLeft()
    {
        facingLeft = true;
    }
	//
    [Command]
    void CmdFlipToRight()
    {
        facingLeft = false;
    }
}
