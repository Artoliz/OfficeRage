using System;
using System.Collections;
using UnityEngine;

public class CellPhone : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private BoxCollider _boxCollider;

    private bool _isVibrating;

    #endregion

    #region PublicVariables

    public AudioSource vibrationSound;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;

        _boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(PlayVibratingSound());
    }

    private void Update()
    {
        if (_isCameraNotNull)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out var hit))
            {
                if (hit.collider == _boxCollider)
                {
                    if (_isVibrating)
                    {
                        _isVibrating = false;
                        vibrationSound.Stop();
                    }
                }
            }
        }
    }

    #endregion

    #region PrivateMethods

    private IEnumerator PlayVibratingSound()
    {
        yield return new WaitForSeconds(30f);
        _isVibrating = true;
        vibrationSound.Play();
    }

    #endregion
}