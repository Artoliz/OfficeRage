using System.Collections;
using UnityEngine;

public class TakeToyOnClick : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private const float GrowFactor = 0.1f;

    private bool _isAlive = true;
    private bool _isHit;

    private MeshCollider _meshCollider;

    private Coroutine _coroutine;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;

        _meshCollider = gameObject.GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if (_isCameraNotNull && !_isHit && _isAlive)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out var hit))
            {
                if (hit.collider == _meshCollider)
                {
                    _isHit = true;
                    HandleToys.Instance.toysCollected++;
                    _coroutine = StartCoroutine(ScaleDown());
                }
            }
        }
        else if (!_isAlive)
        {
            StopCoroutine(_coroutine);
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region PrivateMethods

    private IEnumerator ScaleDown()
    {
        while (_isAlive)
        {
            if (transform.localScale.x <= 0)
                _isAlive = false;
            while (transform.localScale.x > 0)
            {
                transform.localScale -= GrowFactor * Time.deltaTime * new Vector3(1f, 1f, 1f);
                yield return null;
            }
        }
    }

    #endregion
}