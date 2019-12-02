using UnityEngine;

public class MoveHead : MonoBehaviour
{
    private float _xRotation;
    private float _yRotation;
    private Transform _transform;
    
    public float mouseSensitivity = 100f;

/*    private void Awake()
    {
        _transform = gameObject.transform;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 50f);

        _yRotation += mouseX;
        _yRotation = Mathf.Clamp(_yRotation, -90f, 90f);

        _transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }*/
}
