using UnityEngine;

public class DisplayWallPaper : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private bool _isWallPaperDisplayed;

    private SphereCollider _sphereCollider;

    #endregion

    #region PublicVariables

    public SpriteRenderer wallpaper;

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
                    if (!_isWallPaperDisplayed)
                    {
                        _isWallPaperDisplayed = true;
                        wallpaper.enabled = true;
                    }
                    else
                    {
                        _isWallPaperDisplayed = false;
                        wallpaper.enabled = false;
                    }
                }
            }
        }
    }

    #endregion
}