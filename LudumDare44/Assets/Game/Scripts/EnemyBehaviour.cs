using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
	public float damage = 1f;
    protected GameObject _player;
    protected int _rotateSpeed;
    protected int _speed;
    protected NavMeshAgent _nav;

    private GameObject _target;

    // Start is called before the first frame update
    protected void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _target = _player.transform.Find("Offset").gameObject;
        if (!_player) throw new System.Exception("Can't find GameObject with tag \"Player\"");

        _nav = GetComponent<NavMeshAgent>();
        if (!_nav) throw new System.Exception("NavMeshAgent not found");
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_nav.enabled)
        {
            _nav.SetDestination(_target.transform.position);
        }
    }
}
