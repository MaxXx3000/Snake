using UnityEngine;

public class BtnSwitchLang : MonoBehaviour
{
    [SerializeField]
    public LocalizationManager localizationManager;

    [System.Obsolete]
    public void OnButtonClick()
    {
        localizationManager.CurrentLanguage = name;
    }
}