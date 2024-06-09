using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameGuiManager : MonoBehaviour
{
    public static GameGuiManager Instance;
    [SerializeField] private GameObject _pauseScreen;
    public static UnityEvent PauseEvent;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        PauseEvent = new UnityEvent();
        PauseEvent.AddListener(PauseUnpause);
    }

    public void PauseUnpause()
    {
        _pauseScreen.SetActive(!_pauseScreen.activeSelf);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
