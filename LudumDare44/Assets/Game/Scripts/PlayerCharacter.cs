using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    public UnityEvent _shootEvent;
    public UnityEvent _grenadeEvent;

    public float moveSpeed = 10.0f;
    public float gravity = 20f;
    public float jumpForce = 30f;

    public bool active = true;

	public AudioClip deathSound;

    public GameObject JumpChecker;

    private bool isJumping = false;
    private bool canJump = false;
    private float jumpTimer = 0.0f;
    private float jumpVelocity = 0.0f;

	public int killCount = 0;

	private AudioSource _audioSource;
	private CharacterHealth _characterHealth = null;
    private CharacterController _characterController = null;
    private Camera _camera = null;
    private Gun _gun = null;

    private RaycastHit groundRaycast;

    private Buyer _buyer;

    private bool CanInteract = true;

    // Start is called before the first frame update
    void Start()
    {
		_audioSource = GetComponent<AudioSource>();
		_characterHealth = GetComponent<CharacterHealth>();
        _characterController = GetComponent<CharacterController>();
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _gun = GetComponentInChildren<Gun>();
        _buyer = GetComponent<Buyer>();

        if (_gun)
        {
            _shootEvent = new UnityEvent();
            _shootEvent.AddListener(Shoot);
            _grenadeEvent = new UnityEvent();
            _grenadeEvent.AddListener(ThrowGrenade);
        }

        JumpChecker = GameObject.Find("JumpCheckerTarget").gameObject;
    }

    protected void UpdateMovement()
    {
        if (_characterController == null) { return; }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * vertical;
        Vector3 right = transform.right * horizontal;

        Vector3 movement = (forward + right) * moveSpeed;

        if (canJump && !isJumping && Input.GetAxis("Jump") > 0f)
        {
            isJumping = true;
            jumpTimer = Time.time;
            jumpVelocity = jumpForce;
        }

        if (isJumping)
        {
            movement.y += jumpVelocity;
        }
        movement.y -= gravity;

        if (this._characterController)
        {
            _characterController.Move(movement * Time.deltaTime);
        }
    }

    protected void UpdateWeapon()
    {
        if (_gun)
        {
            if (Input.GetButton("Fire1"))
            {
                _shootEvent.Invoke();
            }
            else if (Input.GetButton("Fire2"))
            {
                _grenadeEvent.Invoke();
            }

            _gun.transform.SetPositionAndRotation(_gun.transform.position, _camera.transform.rotation);
        }
    }

    protected void UpdateInteractions()
    {
        if (_buyer)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                float cost = 0f;
                if (_buyer.Purchase(out cost))
                {
                    // Purchase successful;
                    if (_characterHealth)
                    {
                        _characterHealth.Damage(cost);
                        _gun.grenadeCount++;
                    }
                }
                CanInteract = false;
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                CanInteract = true;
            }
        }

    }

    protected void Shoot()
    {
        if (_gun)
        {
            _gun.ShootLaser();
        }
    }

    protected void ThrowGrenade()
    {
        if (_gun)
        {
            _gun.ThrowGrenade();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) { return; }

        Debug.DrawRay(JumpChecker.transform.position, new Vector3(0f, -1f, 0f) * 0.1f, Color.red);

        if (Physics.Raycast(JumpChecker.transform.position, new Vector3(0f, -1f, 0f),0.1f))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (isJumping)
        {
            jumpVelocity -= gravity * Time.deltaTime;
            Mathf.Clamp(jumpVelocity, 0f, jumpVelocity);

            if (jumpVelocity <= gravity && canJump)
            {
                isJumping = false;
            }
        }

        UpdateMovement();
        UpdateWeapon();

        UpdateInteractions();
    }

	protected void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Killable"))
		{
			EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
			_characterHealth.Damage(enemy.damage);

			enemy.GetComponent<Knockback>().knockback(collision.GetContact(0).normal);
			GetComponent<Knockback>().knockback(collision.GetContact(0).normal);
		}
	}

	public void enemyKilled()
	{
        HoldPlayerStats.Score += 1;
		playDeathSound();
	}

	public string getEnemiesKilledUI()
	{
		return "Kills: " + HoldPlayerStats.Score.ToString();
	}

	public void playDeathSound()
	{
		_audioSource.PlayOneShot(deathSound);
	}
}
