using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiAssistant : MonoBehaviour
{

    #region PrivateVariables

    private int _messageIndex;

    private const int WaitBeforeLaunchScene = 5;

    private TextWriter.TextWriterSingle _textWriterSingle;
    
    [SerializeField] private Text messageText;

    [SerializeField] private Button messageButton;

    private readonly string[] _messages =
    {
        "This can be anyone:\nYour best friend.\nYour brother.\nYour sister.\nYour parents.",
        "... even you.",
        "Bullying is everywhere:\nIn the street.\nAt work.\nAt school.",
        "... even at home.",
        "You don't know what people have been trough.\nYou don't know their past.\nYou don't know what they think.",
        "Give love.\nPeople will gave you back."
    };

    #endregion

    #region MonoBehavior

    private void Awake()
    {
        messageButton.onClick.AddListener(ClickScreen);
    }

    private void Update()
    {
        if (_messageIndex == _messages.Length)
            StartCoroutine(LaunchNextSceneAfterPause());
        else if (Input.GetKeyDown(KeyCode.Escape))
            LoadSceneManager.Instance.LoadLevel("Introduction");
    }

    #endregion

    #region PrivateMethods

    private void ClickScreen()
    {
        if (_messageIndex == _messages.Length)
            LoadSceneManager.Instance.LoadLevel("Introduction");
        else if (_textWriterSingle != null && _textWriterSingle.IsActive())
            _textWriterSingle.WriteAllAndDestroy();
        else
        {
            _textWriterSingle = TextWriter.AddWriterStatic(messageText, _messages[_messageIndex], .1f, true, true);
            _messageIndex++;
        }
    }
    
    private static IEnumerator LaunchNextSceneAfterPause()
    {
        yield return new WaitForSeconds(WaitBeforeLaunchScene);
        LoadSceneManager.Instance.LoadLevel("Introduction");
    }

    #endregion
}