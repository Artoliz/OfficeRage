using System.Collections;
using UnityEngine;

public class HandleToys : MonoBehaviour
{
    #region PrivateVariables

    private const float GrowFactor = 1f;

    private bool _isMax;
    private bool _alreadyGrowing;

    private Coroutine _coroutine;

    private Transform _transform;

    #endregion

    #region PublicVariables

    [HideInInspector] public int toysCollected;

    public GameObject penguin;

    public static HandleToys Instance;

    public AudioSource penguinInflation;
    
    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        Instance = this;

        _transform = penguin.transform;
        penguin.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!_alreadyGrowing && toysCollected == 5)
        {
            _alreadyGrowing = true;
            penguin.SetActive(true);
            penguinInflation.Play();
            _coroutine = StartCoroutine(ScaleUp());
        }
        else if (_isMax)
        {
            StopCoroutine(_coroutine);
        }
    }

    #endregion

    #region PrivateMethods

    private IEnumerator ScaleUp()
    {
        while (!_isMax)
        {
            if (_transform.localScale.x >= 1)
                _isMax = true;
            while (_transform.localScale.x < 1)
            {
                _transform.localScale += GrowFactor * Time.deltaTime * new Vector3(1f, 1f, 1f);
                yield return null;
            }
        }
    }

    #endregion
}