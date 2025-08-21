using UnityEngine;

/// <summary>
/// Defines per-level curves for difficulty scaling.
/// </summary>
[CreateAssetMenu(menuName = "_Game/Config/DifficultyCurve")]
public class DifficultyCurve : ScriptableObject
{
    public AnimationCurve scrollSpeed = AnimationCurve.Linear(0, 1, 10, 5);
    public AnimationCurve obstacleSpawnInterval = AnimationCurve.Linear(0, 2, 10, 0.5f);
}
