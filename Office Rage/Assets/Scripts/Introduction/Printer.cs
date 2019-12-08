using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private BoxCollider _boxCollider;

    private Animator _coffeeAnimator;

    private bool _isPrinting;

    #endregion

    #region PublicVariables

    public AudioSource printerSound;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;

        _boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (_isCameraNotNull && !_isPrinting)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out var hit))
            {
                if (hit.collider == _boxCollider)
                {
                    _isPrinting = true;
                    printerSound.Play();
                    StartCoroutine(ResettingPrinterSound());
                }
            }
        }
    }

    #endregion

    #region PrivateMethods

    private IEnumerator ResettingPrinterSound()
    {
        yield return new WaitForSeconds(printerSound.clip.length);
        _isPrinting = false;
    }

    #endregion
}
