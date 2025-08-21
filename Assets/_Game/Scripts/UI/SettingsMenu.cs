using UnityEngine;

/// <summary>
/// Behaviour for the settings menu.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private UIManager m_ui;

    public void Back()
    {
        m_ui?.ShowMainMenu();
    }
}
