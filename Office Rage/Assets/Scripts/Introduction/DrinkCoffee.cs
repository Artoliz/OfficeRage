using System.Collections;
using UnityEngine;

public class DrinkCoffee : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private MeshCollider _meshCollider;

    private Animator _coffeeAnimator;

    private bool _isDrinking;
    
    private static readonly int Drink = Animator.StringToHash("Drink");

    #endregion

    #region PublicVariables

    public AudioSource slurpSound;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;

        _meshCollider = gameObject.GetComponent<MeshCollider>();

        _coffeeAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isCameraNotNull && !_isDrinking)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out var hit))
            {
                if (hit.collider == _meshCollider)
                {
                    _isDrinking = true;
                    _coffeeAnimator.SetBool(Drink, true);
                    StartCoroutine(PlayDrinkSound());
                }
            }
        }
    }

    #endregion

    #region PrivateMethods

    private IEnumerator PlayDrinkSound()
    {
        yield return new WaitForSeconds(1.5f);
        slurpSound.Play();
        StartCoroutine(ResetMugPosition());
    }

    private IEnumerator ResetMugPosition()
    {
        yield return new WaitForSeconds(slurpSound.clip.length);
        _isDrinking = false;
        _coffeeAnimator.SetBool(Drink, false);
    }

    #endregion
}
