using UnityEngine;

/// <summary>
/// Behaviour for the post-run summary menu.
/// </summary>
public class RunSummaryMenu : MonoBehaviour
{
    [SerializeField] private UIManager m_ui;
    [SerializeField] private GameManager m_gameManager;

    public void PlayAgain()
    {
        m_gameManager?.StartRun();
    }

    public void MainMenu()
    {
        m_ui?.ShowMainMenu();
    }
}
