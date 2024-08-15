using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{    
    private bool _isLoading;


    public void ChangeLevel(LevelCoordinates coordinates)
    {
        if (!_isLoading)
        {
            StartCoroutine(LoadLevel(coordinates));
        }
    }

    IEnumerator LoadLevel(LevelCoordinates coordinates)
    {
        _isLoading = true;
        GameInstance.UI.Fade.FadeToBlack();
        yield return new WaitForSeconds(1f);
        GameInstance.UI.LoadingScreen.Toggle(true);
        yield return new WaitForSeconds(1.7f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(coordinates.LevelToLoad.ToString());

        while (!operation.isDone)
        {
            GameInstance.UI.LoadingScreen.LoadingScreen(operation);
            yield return null;
        }

        _isLoading = false;

        GameInstance.Player.SetPlayerPosition(coordinates.PlayerPosition,coordinates.PlayerRotation);
        GameInstance.UI.GameMenu.LoadingComplete();
        GameInstance.UI.Fade.FadeToWhite();
        GameInstance.UI.LoadingScreen.Toggle(false);
    }
}

