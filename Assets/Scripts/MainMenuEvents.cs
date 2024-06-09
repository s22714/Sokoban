using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _startGameButton;
    private Button _tutorialButton;
    private Button _quitGameButton;
    private Button _quitTutorialButton;

    private VisualElement _mainView;
    private VisualElement _tutorialView;


    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _startGameButton = _document.rootVisualElement.Q("StartGameButton") as Button;
        _startGameButton.RegisterCallback<ClickEvent>(StartGame);

        _tutorialButton = _document.rootVisualElement.Q("TutorialButton") as Button;
        _tutorialButton.RegisterCallback<ClickEvent>(OpenTutorial);

        _quitGameButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitGameButton.RegisterCallback<ClickEvent>(QuitGame);
        _quitTutorialButton = _document.rootVisualElement.Q("QuitTutorialButton") as Button;
        _quitTutorialButton.RegisterCallback<ClickEvent>(CloseTutorial);

        _mainView = _document.rootVisualElement.Q("MainView") as VisualElement;
        _tutorialView = _document.rootVisualElement.Q("TutorialView") as VisualElement;

        _tutorialView.style.display = DisplayStyle.None;
    }
    private void StartGame(ClickEvent evt)
    {
        GameModifiers.levelNumber = 0;
        SceneManager.LoadScene(1);
    }

    private void OpenTutorial(ClickEvent evt)
    {
        _tutorialView.style.display = DisplayStyle.Flex;
        _mainView.style.display = DisplayStyle.None;
    }

    private void CloseTutorial(ClickEvent evt)
    {
        _tutorialView.style.display = DisplayStyle.None;
        _mainView.style.display = DisplayStyle.Flex;
    }

    private void QuitGame(ClickEvent evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
