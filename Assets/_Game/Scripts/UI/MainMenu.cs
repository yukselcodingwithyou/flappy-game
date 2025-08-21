using UnityEngine;

/// <summary>
/// Behaviour for the main menu buttons.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIManager m_ui;
    [SerializeField] private GameManager m_gameManager;

    public void Play()
    {
        m_gameManager?.StartRun();
    }

    public void OpenShop()
    {
        m_ui?.OpenShop();
    }

    public void OpenCharacterSelect()
    {
        m_ui?.OpenCharacterSelect();
    }

    public void OpenSettings()
    {
        m_ui?.OpenSettings();
    }
}
