using UnityEngine;

/// <summary>
/// Character selection menu behaviour.
/// </summary>
public class CharacterSelectMenu : MonoBehaviour
{
    [SerializeField] private UIManager m_ui;

    public void Back()
    {
        m_ui?.ShowMainMenu();
    }
}
