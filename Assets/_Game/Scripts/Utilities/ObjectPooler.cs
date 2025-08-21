using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic object pooler for frequently spawned objects.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int initialSize = 5;
    }

    [SerializeField] private Pool[] m_pools;
    private readonly Dictionary<string, Queue<GameObject>> m_poolMap = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        foreach (var pool in m_pools)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < pool.initialSize; i++)
                queue.Enqueue(Create(pool));
            m_poolMap.Add(pool.key, queue);
        }
    }

    private GameObject Create(Pool pool)
    {
        var obj = Instantiate(pool.prefab);
        obj.SetActive(false);
        return obj;
    }

    /// <summary>
    /// Spawn object by key.
    /// </summary>
    public GameObject Spawn(string key, Vector3 position, Quaternion rotation)
    {
        if (!m_poolMap.TryGetValue(key, out var queue))
            return null;
        GameObject obj = queue.Count > 0 ? queue.Dequeue() : Create(System.Array.Find(m_pools, p => p.key == key));
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        if (obj.TryGetComponent<IPooled>(out var pooled))
            pooled.OnSpawned();
        return obj;
    }

    /// <summary>
    /// Despawn object back to pool.
    /// </summary>
    public void Despawn(string key, GameObject obj)
    {
        if (!m_poolMap.TryGetValue(key, out var queue))
            return;
        if (obj.TryGetComponent<IPooled>(out var pooled))
            pooled.OnDespawned();
        obj.SetActive(false);
        queue.Enqueue(obj);
    }
}
