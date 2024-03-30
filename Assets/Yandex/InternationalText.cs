using UnityEngine;
using Zenject;
using TMPro;

public class InternationalText : MonoBehaviour
{
    [SerializeField] private string en;
    [SerializeField] private string ru;
    [SerializeField] private string tr;

    private TMP_Text _text;

    private Language _language;

    [Inject]
    private void Construct(Language language)
    {
        _text = GetComponent<TMP_Text>();
        _language = language;
        language.onSetLanguage += SetText;
        SetText();
    }

    private void OnDestroy()
    {
        _language.onSetLanguage -= SetText;
    }

    private void SetText()
    {
        if (_language.CurrentLanguage == "en")
        {
            _text.text = en;
        }

        else if (_language.CurrentLanguage == "ru")
        {
            _text.text = ru;
        }

        else if (_language.CurrentLanguage == "tr")
        {
            _text.text = tr;
        }

        else
        {
            _text.text = en;
        }
    }
}
