using UnityEngine;

/// <summary>
/// Chooses spawners to activate based on weighted random and level scaling.
/// </summary>
public class SpawnDirector : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEntry
    {
        public Spawner spawner;
        public float weight = 1f;
    }

    public SpawnEntry[] entries;
    public AnimationCurve levelCurve = AnimationCurve.Linear(0, 1, 60, 5);
    private float elapsed;

    void Update()
    {
        elapsed += Time.deltaTime;
    }

    public void Spawn()
    {
        if (entries == null || entries.Length == 0) return;
        float total = 0f;
        foreach (var e in entries) total += e.weight;
        float choice = Random.Range(0, total);
        foreach (var e in entries)
        {
            if (choice < e.weight)
            {
                float level = levelCurve.Evaluate(elapsed);
                for (int i = 0; i < level; i++)
                {
                    e.spawner.Spawn();
                }
                return;
            }
            choice -= e.weight;
        }
    }
}
