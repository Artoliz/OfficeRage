using UnityEngine;

public class DisplayMenu : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private MeshCollider _meshCollider;

    #endregion

    #region PublicVariables

    [HideInInspector]
    public bool isMenuDisplayed;
    
    public static DisplayMenu Instance;
    
    public GameObject windowsDesktop;
    public GameObject screenOff;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        Instance = this;
        
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;

        _meshCollider = gameObject.transform.GetComponent<MeshCollider>();
        windowsDesktop.SetActive(false);
        screenOff.SetActive(false);
    }

    private void Update()
    {
        if (_isCameraNotNull && PowerComputer.Instance.computerIsOn)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0))
            {
                if (hit.collider == _meshCollider)
                {
                    if (!isMenuDisplayed)
                    {
                        isMenuDisplayed = true;
                        windowsDesktop.SetActive(true);
                        screenOff.SetActive(false);
                    }
                    else
                    {
                        isMenuDisplayed = false;
                        windowsDesktop.SetActive(false);
                        screenOff.SetActive(true);
                    }
                }
            }
        }
        else
        {
            isMenuDisplayed = false;
            windowsDesktop.SetActive(false);
            screenOff.SetActive(false);
        }
    }

    #endregion
}