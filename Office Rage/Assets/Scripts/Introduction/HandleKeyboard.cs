using UnityEngine;
using UnityEngine.Video;

public class HandleKeyboard : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private int _keyPressed;

    private bool _tvIsRunning;

    #endregion

    #region PublicVariables

    public MeshCollider keyGame;
    public MeshCollider keyTv;
    public MeshCollider keyQuit;
    
    public AudioSource keyCapSound;

    public VideoPlayer tvVideo;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;
    }

    private void Update()
    {
        if (_isCameraNotNull && PowerComputer.Instance.computerIsOn && DisplayMenu.Instance.isMenuDisplayed)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out var hit))
            {
                if (hit.collider == keyGame)
                    LaunchGame();
                else if (hit.collider == keyTv)
                    LaunchTv();
                else if (hit.collider == keyQuit)
                    QuitGame();
            }
            else if ((_keyPressed = HandleKeys()) != 0)
            {
                if (_keyPressed == 1)
                    LaunchGame();
                else if (_keyPressed == 2)
                    LaunchTv();
                else if (_keyPressed == 3)
                    QuitGame();
            }
        }
    }

    #endregion

    #region PrivateMethods

    private int HandleKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            return 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            return 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            return 3;
        return 0;
    }

    private void LaunchGame()
    {
        keyCapSound.Play();
        PowerComputer.Instance.computerIsOn = false;
        DisplayMenu.Instance.isMenuDisplayed = false;
        LoadSceneManager.Instance.LoadLevel("IntroductionVideo");
    }

    private void LaunchTv()
    {
        keyCapSound.Play();
        if (_tvIsRunning)
        {
            _tvIsRunning = false;
            tvVideo.Stop();
        }
        else
        {
            _tvIsRunning = true;
            tvVideo.Play();
        }
    }

    private void QuitGame()
    {
        keyCapSound.Play();
        PowerComputer.Instance.computerIsOn = false;
        DisplayMenu.Instance.isMenuDisplayed = false;
        Application.Quit();
    }

    #endregion
}