using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float Force = 1000.0f;
    public float Damage = 10f;

    public ParticleSystem particleSystem;
    private SphereCollider hitTrigger;

    public float duration = 5f;

    private Rigidbody _rigidbody;
    private SphereCollider grenadeCollider;

    private float DurationTimer;

    private bool Exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.AddForce(transform.forward * Force);

        particleSystem = Instantiate<ParticleSystem>(particleSystem, transform);

        grenadeCollider = GetComponent<SphereCollider>();

        hitTrigger = gameObject.AddComponent<SphereCollider>();
        hitTrigger.isTrigger = true;
        hitTrigger.radius = 3f;
        hitTrigger.enabled = false;

        DurationTimer = Time.time;

        Explode();
    }

    protected void Explode()
    {
        if (!Exploded)
        {
            DurationTimer = Time.time;
            if (particleSystem)
            {
                particleSystem.Play();
            }

            hitTrigger.enabled = true;
            Exploded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Exploded)
        {
            if (Time.time >= DurationTimer + duration)
            {
                Destroy(gameObject);
            }

            float scale = ((DurationTimer + duration) - Time.time);
            Mathf.Clamp(scale, 0.0f, 1.0f);
            transform.localScale = new Vector3(scale, scale, scale);
            particleSystem.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitTrigger && hitTrigger.enabled)
        {
            if (other.CompareTag("Killable"))
            {
                CharacterHealth characterHealth = other.GetComponent<CharacterHealth>();
                if (characterHealth)
                {
                    characterHealth.Damage(Damage);
                }
            }
        }
    }
}
