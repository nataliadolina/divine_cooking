using UnityEngine;
using Zenject;
using TMPro;

public class InternationalText : MonoBehaviour
{
    [SerializeField] private string en;
    [SerializeField] private string ru;

    [Inject]
    private Language language;

    [Inject]
    private void Construct()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        if (language.CurrentLanguage == "en")
        {
            text.text = en;
        }

        else if (language.CurrentLanguage == "ru")
        {
            text.text = ru;
        }

        else
        {
            text.text = en;
        }
    }
}
