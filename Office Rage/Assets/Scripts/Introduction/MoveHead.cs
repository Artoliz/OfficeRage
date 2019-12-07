using UnityEngine;

public class MoveHead : MonoBehaviour
{
    #region PrivateVariables

    #region Enums

    private enum LookDirectionVertical
    {
        Middle,
        Up,
        Down
    }

    private enum LookDirectionHorizontal
    {
        Center,
        Left,
        Right
    }

    #endregion

    private LookDirectionVertical _lookDirectionVertical = LookDirectionVertical.Middle;
    private LookDirectionHorizontal _lookDirectionHorizontal = LookDirectionHorizontal.Center;

    private Transform _transform;
    private Quaternion _targetRotation;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _transform = gameObject.transform;
        _targetRotation = new Quaternion();
    }

    private void Update()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _targetRotation, 60 * Time.deltaTime);
    }

    #endregion

    #region PublicMethods

    public void LookUp()
    {
        _lookDirectionHorizontal = LookDirectionHorizontal.Center;

        if (_lookDirectionVertical == LookDirectionVertical.Middle)
        {
            _lookDirectionVertical = LookDirectionVertical.Up;
            _targetRotation = Quaternion.Euler(-30f, 0, 0f);
        }
        else if (_lookDirectionVertical == LookDirectionVertical.Down)
        {
            _lookDirectionVertical = LookDirectionVertical.Middle;
            _targetRotation = Quaternion.Euler(10f, 0, 0f);
        }
    }

    public void LookDown()
    {
        _lookDirectionHorizontal = LookDirectionHorizontal.Center;

        if (_lookDirectionVertical == LookDirectionVertical.Middle)
        {
            _lookDirectionVertical = LookDirectionVertical.Down;
            _targetRotation = Quaternion.Euler(50f, 0, 0f);
        }
        else if (_lookDirectionVertical == LookDirectionVertical.Up)
        {
            _lookDirectionVertical = LookDirectionVertical.Middle;
            _targetRotation = Quaternion.Euler(10f, 0, 0f);
        }
    }

    public void LookLeft()
    {
        _lookDirectionVertical = LookDirectionVertical.Middle;

        if (_lookDirectionHorizontal == LookDirectionHorizontal.Center)
        {
            _lookDirectionHorizontal = LookDirectionHorizontal.Left;
            _targetRotation = Quaternion.Euler(10f, -70f, 0f);
        }
        else if (_lookDirectionHorizontal == LookDirectionHorizontal.Right)
        {
            _lookDirectionHorizontal = LookDirectionHorizontal.Center;
            _targetRotation = Quaternion.Euler(10f, 0, 0f);
        }
    }

    public void LookRight()
    {
        _lookDirectionVertical = LookDirectionVertical.Middle;

        if (_lookDirectionHorizontal == LookDirectionHorizontal.Center)
        {
            _lookDirectionHorizontal = LookDirectionHorizontal.Right;
            _targetRotation = Quaternion.Euler(10f, 70f, 0f);
        }
        else if (_lookDirectionHorizontal == LookDirectionHorizontal.Left)
        {
            _lookDirectionHorizontal = LookDirectionHorizontal.Center;
            _targetRotation = Quaternion.Euler(10f, 0, 0f);
        }
    }

    #endregion
}