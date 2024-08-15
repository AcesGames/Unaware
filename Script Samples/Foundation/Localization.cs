using Assets.SimpleLocalization.Scripts;
using UnityEngine;


public class Localization : MonoBehaviour
{
    private void Start()
    {
        LocalizationManager.Read();

        switch (Application.systemLanguage)
        {
            case SystemLanguage.Spanish:
                LocalizationManager.Language = "Spanish";
                break;
            case SystemLanguage.French:
                LocalizationManager.Language = "French";
                break;
            case SystemLanguage.German:
                LocalizationManager.Language = "German";
                break;
            case SystemLanguage.Russian:
                LocalizationManager.Language = "Russian";
                break;
            case SystemLanguage.Portuguese:
                LocalizationManager.Language = "Portuguese";
                break;
            case SystemLanguage.Japanese:
                LocalizationManager.Language = "Japanese";
                break;
            case SystemLanguage.Chinese:
                LocalizationManager.Language = "Chinese";
                break;
            case SystemLanguage.Korean:
                LocalizationManager.Language = "Korean";
                break;
            default:
                LocalizationManager.Language = "English";
                break;
        }
    }

    public void SetLocalization(string localization)
    {
        LocalizationManager.Language = localization;
    }

    public string GetLocalization(string key)
    {
        return LocalizationManager.Localize(key);
    }
}
