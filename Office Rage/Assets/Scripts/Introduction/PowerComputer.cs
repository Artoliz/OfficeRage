using UnityEngine;

public class PowerComputer : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private MeshCollider _meshCollider;
    
    #endregion

    #region PublicVariables
    
    [HideInInspector]
    public bool computerIsOn;

    public GameObject wallpaperNoPower;
    
    public static PowerComputer Instance;
    
    public AudioSource windowsStartSound;
    public AudioSource windowsStopSound;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        Instance = this;
        
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;
        wallpaperNoPower.SetActive(true);

        _meshCollider = gameObject.transform.GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if (_isCameraNotNull)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0))
            {
                if (hit.collider == _meshCollider)
                {
                    if (!computerIsOn)
                    {
                        windowsStartSound.Play();
                        computerIsOn = true;
                        DisplayMenu.Instance.isMenuDisplayed = true;
                        DisplayMenu.Instance.windowsDesktop.SetActive(true);
                        wallpaperNoPower.SetActive(false);
                    }
                    else
                    {
                        windowsStopSound.Play();
                        computerIsOn = false;
                        wallpaperNoPower.SetActive(true);
                    }
                }
            }
        }
    }

    #endregion
}