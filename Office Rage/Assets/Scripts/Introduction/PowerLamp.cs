using UnityEngine;

public class PowerLamp : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private bool _isLampPowered = true;

    private SphereCollider _sphereCollider;

    #endregion

    #region PublicVariables

    public Light spotLight;
    
    public Renderer bulb;

    public Material on;
    public Material off;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;

        _sphereCollider = gameObject.transform.GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (_isCameraNotNull)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0))
            {
                if (hit.collider == _sphereCollider)
                {
                    if (!_isLampPowered)
                    {
                        _isLampPowered = true;
                        spotLight.enabled = true;
                        bulb.material = on;
                    }
                    else
                    {
                        _isLampPowered = false;
                        spotLight.enabled = false;
                        bulb.material = off;
                    }
                }
            }
        }
    }

    #endregion
}