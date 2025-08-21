using UnityEngine;

/// <summary>
/// Behaviour for the in-game shop menu.
/// </summary>
public class ShopMenu : MonoBehaviour
{
    [SerializeField] private UIManager m_ui;
    [SerializeField] private SaveSystem m_saveSystem;

    private void Awake()
    {
        m_saveSystem?.Load();
    }

    public void UnlockCharacter(string id)
    {
        if (m_saveSystem != null && !m_saveSystem.IsCharacterUnlocked(id))
        {
            m_saveSystem.UnlockCharacter(id);
            m_saveSystem.Save();
        }
    }

    public void PurchaseUpgrade(string id)
    {
        if (m_saveSystem != null && !m_saveSystem.HasUpgrade(id))
        {
            m_saveSystem.PurchaseUpgrade(id);
            m_saveSystem.Save();
        }
    }

    public void Back()
    {
        m_ui?.ShowMainMenu();
    }
}
