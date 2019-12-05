using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private string _scene;
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    public Animator anim;

    public static LoadSceneManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadLevel(string scene)
    {
        _scene = scene;
        anim.SetTrigger(FadeOut);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_scene);
    }
}