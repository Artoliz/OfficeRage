using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPhone : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private MeshCollider _meshCollider;

    private Animator _coffeeAnimator;

    private bool _isDialing;

    #endregion

    #region PublicVariables

    public AudioSource phoneDialingSound;
    public AudioSource phoneBusySignal;

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
        if (_isCameraNotNull && !_isDialing)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out var hit))
            {
                if (hit.collider == _meshCollider)
                {
                    _isDialing = true;
                    phoneDialingSound.Play();
                    StartCoroutine(PlayDrinkSound());
                }
            }
        }
    }

    #endregion

    #region PrivateMethods

    private IEnumerator PlayDrinkSound()
    {
        yield return new WaitForSeconds(phoneDialingSound.clip.length);
        phoneBusySignal.Play();
        _isDialing = false;
    }

    #endregion
}
