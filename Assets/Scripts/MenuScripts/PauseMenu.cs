using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button Resume_Button;
    [SerializeField] private Button Quit_Buttom;
    [SerializeField] private Button Restart_Button;

    private void Start()
    {
        Resume_Button.onClick.AddListener(HandleResumeClicked);
        Restart_Button.onClick.AddListener(HandleRestartClicked);
        Quit_Buttom.onClick.AddListener(HandleQuitClicked);
    }

    private void HandleResumeClicked()
    {
        GameManager.Instance.Pause_Game();
    }

    private void HandleRestartClicked()
    {
        GameManager.Instance.Restart_Game();
    }

    private void HandleQuitClicked()
    {
        GameManager.Instance.Quit_Game();
    }
}
