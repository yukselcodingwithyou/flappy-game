using UnityEngine;

/// <summary>
/// Controls level difficulty via curves.
/// </summary>
public class LevelManager : MonoBehaviour
{
    public AnimationCurve speedCurve = AnimationCurve.Linear(0, 1, 60, 3);
    private float elapsed;

    void Update()
    {
        elapsed += Time.deltaTime;
    }

    public float CurrentSpeedMultiplier()
    {
        return speedCurve.Evaluate(elapsed);
    }
}
