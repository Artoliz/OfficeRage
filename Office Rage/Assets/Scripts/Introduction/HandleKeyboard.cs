using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleKeyboard : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private int _keyPressed;

    #endregion

    #region PublicVariables

    public MeshCollider keyGame;
    public MeshCollider keyOther;
    public MeshCollider keyQuit;

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
                else if (hit.collider == keyOther)
                    LaunchOther();
                else if (hit.collider == keyQuit)
                    QuitGame();
            }
            else if ((_keyPressed = HandleKeys()) != 0)
            {
                if (_keyPressed == 1)
                    LaunchGame();
                else if (_keyPressed == 2)
                    LaunchOther();
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
        PowerComputer.Instance.computerIsOn = false;
        DisplayMenu.Instance.isMenuDisplayed = false;
        SceneManager.LoadScene("Game");
    }

    private void LaunchOther()
    {
        //Do Something
    }

    private void QuitGame()
    {
        PowerComputer.Instance.computerIsOn = false;
        DisplayMenu.Instance.isMenuDisplayed = false;
        Application.Quit();
    }

    #endregion
}