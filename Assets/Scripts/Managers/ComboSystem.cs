using UnityEngine;

/// <summary>
/// Tracks combos based on successive successful actions.
/// </summary>
public class ComboSystem : MonoBehaviour
{
    public float resetTime = 2f;
    private int combo = 0;
    private float timer;

    void Update()
    {
        if (combo > 0)
        {
            timer += Time.deltaTime;
            if (timer > resetTime)
            {
                combo = 0;
                timer = 0;
            }
        }
    }

    public void RegisterAction()
    {
        combo++;
        timer = 0;
    }

    public int CurrentCombo => combo;
}
