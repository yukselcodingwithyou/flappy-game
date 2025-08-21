using UnityEngine;

/// <summary>
/// Behaviour for the in-game shop menu.
/// </summary>
public class ShopMenu : MonoBehaviour
{
    [SerializeField] private UIManager m_ui;

    public void Back()
    {
        m_ui?.ShowMainMenu();
    }
}
