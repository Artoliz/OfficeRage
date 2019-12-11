using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class People : MonoBehaviour
{
    public Slider Health;

    private const string _isWalking = "isWalking";
    private const string _isPunching = "isPunching";

    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _lastPosition;

    private float _angularSpeed = 2;

    private Transform _player = null;

    private int _hp = 10;
    private bool _isDead = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        Health.value = _hp;
    }

    void Update()
    {
        if (_isDead)
            return;
        if (_player != null)
        {
            _agent.SetDestination(_player.position);
            if (!_agent.isStopped && Vector3.Distance(transform.position, _player.position) < 2)
            {
                _agent.isStopped = true;
                _animator.SetBool(_isWalking, false);
                _animator.SetBool(_isPunching, true);
            }
            if (_agent.isStopped && Vector3.Distance(transform.position, _player.position) >= 2)
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

    public bool IsDead()
    {
        return _isDead;
    }

    public void RemoveOneHP()
    {
        _hp -= 1;
        Health.value = _hp;
        if (_hp == 0) {
            // Set animation die ?
            Destroy(this.gameObject); // To be removed
            //_agent.isStopped = true;
            //_isDead = true;
        }
    }

    public int GetHP()
    {
        return _hp;
    }
}
