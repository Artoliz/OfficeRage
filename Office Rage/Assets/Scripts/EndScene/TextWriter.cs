using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    #region PrivateVariables

    private static TextWriter _instance;

    private List<TextWriterSingle> _textWriterSingleList;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _instance = this;
        _textWriterSingleList = new List<TextWriterSingle>();
    }

    private void Update()
    {
        for (int i = 0; i < _textWriterSingleList.Count; i++)
        {
            bool destroyInstance = _textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                _textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    #endregion

    #region PrivateMethods

    private TextWriterSingle AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        TextWriterSingle textWriterSingle =
            new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters);
        _textWriterSingleList.Add(textWriterSingle);
        
        return textWriterSingle;
    }

    public static void RemoveWriterStatic(Text uiText)
    {
        _instance.RemoveWriter(uiText);
    }

    private void RemoveWriter(Text uiText)
    {
        for (int i = 0; i < _textWriterSingleList.Count; i++)
        {
            if (_textWriterSingleList[i].GetUiText() == uiText)
            {
                _textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    #endregion

    #region PublicMethods
    
    public static TextWriterSingle AddWriterStatic(Text uiText, string textToWrite, float timePerCharacter,
        bool invisibleCharacters, bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd)
            _instance.RemoveWriter(uiText);
        return _instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters);
    }

    #endregion

    // Represents a single TextWriter instance
    public class TextWriterSingle
    {
        private readonly Text _uiText;
        private readonly string _textToWrite;
        private int _characterIndex;
        private readonly float _timePerCharacter;
        private float _timer;
        private readonly bool _invisibleCharacters;

        // Returns true on complete
        public bool Update()
        {
            _timer -= Time.deltaTime;
            while (_timer <= 0f)
            {
                // Display next character
                _timer += _timePerCharacter;
                _characterIndex++;
                string text = _textToWrite.Substring(0, _characterIndex);
                if (_invisibleCharacters)
                {
                    text += "<color=#00000000>" + _textToWrite.Substring(_characterIndex) + "</color>";
                }

                _uiText.text = text;

                if (_characterIndex >= _textToWrite.Length)
                {
                    return true;
                }
            }
            return false;
        }

        public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
        {
            _uiText = uiText;
            _textToWrite = textToWrite;
            _timePerCharacter = timePerCharacter;
            _invisibleCharacters = invisibleCharacters;
            _characterIndex = 0;
        }
        
        public Text GetUiText()
        {
            return _uiText;
        }

        public bool IsActive()
        {
            return _characterIndex < _textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            _uiText.text = _textToWrite;
            _characterIndex = _textToWrite.Length;
            RemoveWriterStatic(_uiText);
        }
    }
}