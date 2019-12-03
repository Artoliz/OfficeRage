using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class People : MonoBehaviour
{
    private const string _isWalking = "isWalking";
    private const string _isPunching = "isPunching";

    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _lastPosition;

    private float _angularSpeed = 2;

    private Transform _player = null;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_player != null)
        {
            _agent.SetDestination(_player.position);
            if (!_agent.isStopped && Vector3.Distance(transform.position, _player.position) < 1.5f)
            {
                _agent.isStopped = true;
                _animator.SetBool(_isWalking, false);
                _animator.SetBool(_isPunching, true);
            }
            if (_agent.isStopped && Vector3.Distance(transform.position, _player.position) >= 1.5f)
            {
                _agent.isStopped = false;
                _animator.SetBool(_isWalking, true);
                _animator.SetBool(_isPunching, false);
            }
            transform.LookAt(new Vector3(_player.position.x, 0, _player.position.z));
        }
    }

    public void SetTarget(Transform player)
    {
        _agent.isStopped = false;
        _player = player;
        _animator.SetBool(_isWalking, true);
    }

    public void RemoveTarget()
    {
        _agent.isStopped = true;
        _player = null;
        _animator.SetBool(_isWalking, false);
        _animator.SetBool(_isPunching, false);
    }
}
