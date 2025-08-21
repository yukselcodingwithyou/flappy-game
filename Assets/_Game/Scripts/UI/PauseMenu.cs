using UnityEngine;

/// <summary>
/// Behaviour for the pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIManager m_ui;
    [SerializeField] private GameManager m_gameManager;

    public void Resume()
    {
        m_ui?.ResumeGame();
    }

    public void QuitToMenu()
    {
        m_gameManager?.EndRun();
        m_ui?.ShowMainMenu();
    }
}
