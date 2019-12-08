using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhone : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private BoxCollider _boxCollider;

    private int _index;
    
    private bool _isVibrating;

    private List<AudioSource> _evilSoundList;
    
    private Animator _cellPhoneAnimator;
    private static readonly int AnswerCellPhone = Animator.StringToHash("AnswerCellPhone");

    #endregion

    #region PublicVariables

    public AudioSource vibrationSound;
    public GameObject evilSound;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;

        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _evilSoundList = new List<AudioSource>(evilSound.GetComponents<AudioSource>());
        _cellPhoneAnimator = gameObject.GetComponent<Animator>();
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
                        _cellPhoneAnimator.SetBool(AnswerCellPhone, true);
                        StartCoroutine(PlayAnswerSound());
                    }
                }
            }
        }
    }

    #endregion

    #region PrivateMethods

    private IEnumerator PlayVibratingSound()
    {
        yield return new WaitForSeconds(30);
        _isVibrating = true;
        vibrationSound.Play();
    }

    private IEnumerator PlayAnswerSound()
    {
        yield return new WaitForSeconds(1.5f);
        _index = Random.Range(0, _evilSoundList.Count);
        _evilSoundList[_index].Play();
        StartCoroutine(ResetCellPhonePosition());
    }
    
    private IEnumerator ResetCellPhonePosition()
    {
        yield return new WaitForSeconds(_evilSoundList[_index].clip.length);
        _cellPhoneAnimator.SetBool(AnswerCellPhone, false);
    }

    #endregion
}