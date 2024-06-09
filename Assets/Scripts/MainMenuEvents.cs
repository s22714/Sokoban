using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _startGameButton;
    private Button _tutorialButton;
    private Button _quitGameButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _startGameButton = _document.rootVisualElement.Q("StartGameButton") as Button;
        _startGameButton.RegisterCallback<ClickEvent>(StartGame);

        _tutorialButton = _document.rootVisualElement.Q("TutorialButton") as Button;
        _tutorialButton.RegisterCallback<ClickEvent>(OpenTutorial);

        _quitGameButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitGameButton.RegisterCallback<ClickEvent>(QuitGame);
    }
    private void StartGame(ClickEvent evt)
    {
        GameModifiers.levelNumber = 0;
        SceneManager.LoadScene(1);
    }

    private void OpenTutorial(ClickEvent evt)
    {
        Debug.Log("tutorial button pressed.");
    }

    private void QuitGame(ClickEvent evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
