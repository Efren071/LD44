using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Grenade grenade;
    public LaserBeam laserBeam;

    public float range = 50.0f;
    public float laserDelay = 0.5f;

	public AudioClip gunSound;
	public AudioClip fireballSound;
	protected AudioSource _audioSource;

    public float grenadeDelay = 1.0f;

    public int grenadeCount = 3;

    protected float grenadeDelayTimer = 0f;
    public float laserDelayTimer = 0f;

    private Camera camera = null;

    // Start is called before the first frame update
    void Start()
    {
        camera = transform.parent.GetComponentInChildren<Camera>();
        if (camera == null)
        {
            Debug.Log("No Camera Attached to Parent");
        }

		_audioSource = GetComponent<AudioSource>();
    }

    public void ShootLaser()
    {
        if (Time.time >= laserDelayTimer + laserDelay)
        {
			_audioSource.PlayOneShot(gunSound);

            laserDelayTimer = Time.time;

            Vector3 forward = transform.forward;
            Vector3 gunBarrel = transform.position;
            Vector3 rayEnd = forward * range;

            Debug.DrawRay(gunBarrel, rayEnd, Color.red);
            RaycastHit hit;

            LaserBeam beamInstance = Instantiate(laserBeam, transform.position, transform.rotation);
            beamInstance.Set(gunBarrel, gunBarrel + rayEnd);

            //Detect hits
            if (Physics.Raycast(gunBarrel, rayEnd, out hit, range))
            {
                HitWithLaser(hit);
            }
        }
    }

    public void ThrowGrenade()
    {
        if (grenadeCount > 0 && grenade && (Time.time >= grenadeDelayTimer + grenadeDelay))
        {
			_audioSource.PlayOneShot(fireballSound);

            grenadeDelayTimer = Time.time;
            Grenade grenadeInstance = Instantiate(grenade, transform.position, transform.rotation);
            grenadeCount--;

            Debug.Log("Grenade Thrown");
        }
    }

    // Fire spawn a projectile
    public void Shoot()
    {

    }

    protected void HitWithLaser(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Killable"))
        {
            CharacterHealth health = hit.transform.gameObject.GetComponent<CharacterHealth>();
            health.Damage(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 gunBarrel = transform.position;
        Vector3 rayEnd = forward * range;

        Debug.DrawRay(gunBarrel, rayEnd, Color.red);
    }
}
