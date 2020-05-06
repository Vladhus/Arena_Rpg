using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SingleMagicMoba;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private Animation mainMenuAnimator;
   [SerializeField] private AnimationClip fadeOutAnimation;
   [SerializeField] private AnimationClip fadeInAnimation;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void OnFadeOutComplete()
    {
        OnMainMenuFadeComplete.Invoke(true);
    }

    public void OnFadeInComplete()
    {
        OnMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
    }

    private void HandleGameStateChanged(GameManager.GameState currentGameState ,GameManager.GameState previousGameState)
    {
        if (previousGameState == GameManager.GameState.PREGAME && currentGameState == GameManager.GameState.RUNNING)
        {
            FadeOut();
        }

        if (previousGameState != GameManager.GameState.PREGAME && currentGameState == GameManager.GameState.PREGAME)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadeInAnimation;
        mainMenuAnimator.Play();
    }

    public void FadeOut()
    {

        UIManager.Instance.SetDummyCameraActive(false);

        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadeOutAnimation;
        mainMenuAnimator.Play();
        
    }
}
