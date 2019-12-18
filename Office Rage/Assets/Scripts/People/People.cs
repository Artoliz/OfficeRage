using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class People : MonoBehaviour
{
    public Slider Health;

    public GameObject Key;

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
            if (!_agent.isStopped && Vector3.Distance(transform.position, _player.position) < 1)
            {
                _agent.isStopped = true;
                _animator.SetBool(_isWalking, false);
                _animator.SetBool(_isPunching, true);
            }
            if (_agent.isStopped && Vector3.Distance(transform.position, _player.position) >= 1)
            {
                _agent.isStopped = false;
                _animator.SetBool(_isWalking, true);
                _animator.SetBool(_isPunching, false);
            }
            transform.LookAt(new Vector3(_player.position.x, 0, _player.position.z));
        }
    }

    private void ApplyRagdoll()
    {
        Ragdoll ragdoll = GetComponent<Ragdoll>();

        ragdoll.ActivateRagdoll();

        foreach (Rigidbody bone in GetComponentsInChildren<Rigidbody>())
        {
            if (bone.gameObject.CompareTag("People"))
                continue;
            bone.AddForce(new Vector3(0, 0, -10), ForceMode.Impulse);
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
        if (_hp <= 0) {
            _agent.isStopped = true;
            _isDead = true;

            if (GetComponentInChildren<Canvas>().gameObject != null)
                Destroy(GetComponentInChildren<Canvas>().gameObject);

            if (gameObject.name == "Josh")
            {
                LoadSceneManager.Instance.LoadLevel("EndScene");
                return;
            }
            int rd = Random.Range(0, 10);

            Quaternion quat = new Quaternion();
            quat.eulerAngles = new Vector3(0, -280, 90);

            if (rd % 2 == 0)
                Instantiate(Key, new Vector3(transform.position.x, 1, transform.position.z), quat);

            ApplyRagdoll();
        }
    }

    public int GetHP()
    {
        return _hp;
    }
}
