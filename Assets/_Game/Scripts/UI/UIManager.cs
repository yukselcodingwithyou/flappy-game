using UnityEngine;

/// <summary>
/// Central controller for UI menus and HUD visibility.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private AdsManager m_adsManager;
    [SerializeField] private HUDController m_hud;

    [Header("Player")]
    [SerializeField] private Health m_playerHealth;
    [SerializeField] private BuffController m_playerBuffs;

    [Header("Menus")]
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private GameObject m_runSummaryMenu;
    [SerializeField] private GameObject m_shopMenu;
    [SerializeField] private GameObject m_characterSelectMenu;
    [SerializeField] private GameObject m_settingsMenu;

    private void OnEnable()
    {
        if (m_gameManager != null)
        {
            m_gameManager.OnRunStarted += HandleRunStarted;
            m_gameManager.OnRunEnded += HandleRunEnded;
        }
    }

    private void OnDisable()
    {
        if (m_gameManager != null)
        {
            m_gameManager.OnRunStarted -= HandleRunStarted;
            m_gameManager.OnRunEnded -= HandleRunEnded;
        }
    }

    private void Start()
    {
        if (m_hud != null)
            m_hud.Bind(m_gameManager, m_playerHealth, m_playerBuffs);
        ShowMainMenu();
    }

    private void HandleRunStarted()
    {
        HideAllMenus();
        if (m_hud != null)
            m_hud.gameObject.SetActive(true);
        m_adsManager?.HideBanner();
    }

    private void HandleRunEnded()
    {
        ShowRunSummary();
    }

    private void HideAllMenus()
    {
        if (m_hud != null)
            m_hud.gameObject.SetActive(false);
        m_mainMenu?.SetActive(false);
        m_pauseMenu?.SetActive(false);
        m_runSummaryMenu?.SetActive(false);
        m_shopMenu?.SetActive(false);
        m_characterSelectMenu?.SetActive(false);
        m_settingsMenu?.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllMenus();
        m_mainMenu?.SetActive(true);
        m_adsManager?.ShowBanner();
    }

    public void ShowRunSummary()
    {
        HideAllMenus();
        m_runSummaryMenu?.SetActive(true);
        m_adsManager?.ShowBanner();
    }

    public void PauseGame()
    {
        if (m_gameManager == null) return;
        m_gameManager.Pause();
        HideAllMenus();
        m_pauseMenu?.SetActive(true);
        m_adsManager?.ShowBanner();
    }

    public void ResumeGame()
    {
        if (m_gameManager == null) return;
        m_gameManager.Resume();
        HideAllMenus();
        if (m_hud != null)
            m_hud.gameObject.SetActive(true);
        m_adsManager?.HideBanner();
    }

    public void OpenShop()
    {
        HideAllMenus();
        m_shopMenu?.SetActive(true);
        m_adsManager?.ShowBanner();
    }

    public void OpenCharacterSelect()
    {
        HideAllMenus();
        m_characterSelectMenu?.SetActive(true);
        m_adsManager?.ShowBanner();
    }

    public void OpenSettings()
    {
        HideAllMenus();
        m_settingsMenu?.SetActive(true);
        m_adsManager?.ShowBanner();
    }
}
