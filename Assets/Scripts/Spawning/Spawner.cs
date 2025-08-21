using UnityEngine;

/// <summary>
/// Spawns a random prefab from a list at fixed intervals.
/// </summary>
public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float interval = 3f;
    public Transform parent;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), interval, interval);
    }

    public void Spawn()
    {
        if (prefabs == null || prefabs.Length == 0) return;
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        Instantiate(prefab, transform.position, Quaternion.identity, parent);
    }
}
