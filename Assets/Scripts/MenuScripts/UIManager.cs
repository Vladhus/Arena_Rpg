using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SingleMagicMoba;

public class UIManager : myManager<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private Camera dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFaidComplete);
    }

    private void HandleMainMenuFaidComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    private void HandleGameStateChanged(GameManager.GameState currentGameState, GameManager.GameState previousGameState)
    {
        _pauseMenu.gameObject.SetActive(currentGameState == GameManager.GameState.PAUSED);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameManager.Instance.Start_Game();
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        dummyCamera.gameObject.SetActive(active);
    }
}
