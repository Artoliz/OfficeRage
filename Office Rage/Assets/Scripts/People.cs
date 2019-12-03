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

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500))
            {
                _lastPosition = hit.point;
                _agent.destination = hit.point;
                _animator.SetBool(_isWalking, true);
            }
        }
        if (!_agent.isStopped && Vector3.Distance(transform.position, _lastPosition) < 0.1)
        {
            _agent.isStopped = true;
            _animator.SetBool(_isWalking, false);
        }
        else if (!_agent.isStopped)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lastPosition - transform.position), _angularSpeed * Time.deltaTime);
    }
}
