using UnityEngine;
using UnityEngine.Video;
 
public class LaunchSceneAfterVideo : MonoBehaviour
{
    private VideoPlayer _video;
 
    private void Awake()
    {
        _video = GetComponent<VideoPlayer>();
        _video.loopPointReached += LoadScene;
    }
 
    private void LoadScene(VideoPlayer vp)
    {
        LoadSceneManager.Instance.LoadLevel("Game");
    }
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadSceneManager.Instance.LoadLevel("Game");
    }
}