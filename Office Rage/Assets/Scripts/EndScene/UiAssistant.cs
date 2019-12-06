using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiAssistant : MonoBehaviour
{
    #region PrivateVariables

    private const int WaitBeforeLaunchScene = 9;
    private const int WaitBeforeNextText = 3;

    private static int _messageIndex;

    private static bool _isWaitingForNextText;
    
    private static GameObject _bullyingPanel;

    private static TextWriter.TextWriterSingle _textWriterSingle;

    private static Text _messageText;

    private Button _messageButton;

    private Coroutine _coroutine;
    
    private static readonly string[] Messages =
    {
        "This can be anyone:",
        "Your best friend.\nYour brother.\nYour sister.\nYour parents.",
        "... even you.",
        "Bullying is everywhere:",
        "In the street.\nAt work.\nAt school.",
        "... even at home.",
        "You don't know what people have been through.\nYou don't know their past.\nYou don't know what they think.",
        "Give love.\nPeople will gave you back.",
    };

    #endregion
    
    #region MonoBehavior

    private void Awake()
    {
        _bullyingPanel = transform.Find("Canvas").Find("PanelBullying").gameObject;
        _messageText = transform.Find("Canvas").Find("Text").GetComponent<Text>();
        _messageButton = transform.Find("Canvas").Find("Text").GetComponent<Button>();
        
        _bullyingPanel.SetActive(false);

        _messageButton.onClick.AddListener(ClickScreen);
    }

    private void Start()
    {
        WriteNextText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadSceneManager.Instance.LoadLevel("Introduction");
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            WriteNextText();
        }
        else if (_messageIndex >= Messages.Length && _textWriterSingle.IsFinished())
        {
            StartCoroutine(DisplayImageAfterPause());
            StartCoroutine(LaunchNextSceneAfterPause());
        }
        else if (_textWriterSingle != null &&
                 _messageIndex < Messages.Length &&
                 _textWriterSingle.IsFinished() &&
                 !_isWaitingForNextText)
        {
            _isWaitingForNextText = true;
            _coroutine = StartCoroutine(WriteNextTextAfterPause());
        }
    }

    #endregion

    #region PrivateMethods

    private void ClickScreen()
    {
        StopCoroutine(_coroutine);
        WriteNextText();
    }

    private static void WriteNextText()
    {
        _isWaitingForNextText = false;

        if (_messageIndex >= Messages.Length)
            LoadSceneManager.Instance.LoadLevel("Introduction");
        else if (_textWriterSingle != null && _textWriterSingle.IsActive())
            _textWriterSingle.WriteAllAndDestroy();
        else
        {
            _textWriterSingle = TextWriter.AddWriterStatic(_messageText, Messages[_messageIndex], .1f, true, true);
            _messageIndex++;
        }
    }

    private static IEnumerator LaunchNextSceneAfterPause()
    {
        yield return new WaitForSeconds(WaitBeforeLaunchScene);
        LoadSceneManager.Instance.LoadLevel("Introduction");
    }

    private static IEnumerator WriteNextTextAfterPause()
    {
        yield return new WaitForSeconds(WaitBeforeNextText);
        WriteNextText();
    }

    private static IEnumerator DisplayImageAfterPause()
    {
        yield return new WaitForSeconds(WaitBeforeNextText);
        _bullyingPanel.SetActive(true);
    }

    #endregion
}