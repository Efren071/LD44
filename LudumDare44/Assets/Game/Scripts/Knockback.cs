using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knockback : MonoBehaviour
{
	public float knockbackTime = 1f;
	protected Rigidbody _rigidbody;
	protected NavMeshAgent _nav;

	protected void Start()
	{
		_nav = gameObject.GetComponent<NavMeshAgent>();
		_rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	public void knockback(Vector3 normal)
	{
		normal.y = 0;
		_rigidbody.drag = 10f;
		_rigidbody.AddForce(-normal * 50, ForceMode.VelocityChange);
		StartCoroutine("KnockbackTimer");
	}

	IEnumerable KnockbackTimer()
	{
		if (_nav) _nav.enabled = false;

		yield return new WaitForSeconds(knockbackTime);

		if (_nav) _nav.enabled = true;
		_rigidbody.drag = Mathf.Infinity;
	}
}
