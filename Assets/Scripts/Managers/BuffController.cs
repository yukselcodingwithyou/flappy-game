using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Tracks active buffs on the player.
/// </summary>
public class BuffController : MonoBehaviour
{
    private readonly List<IBuff> activeBuffs = new List<IBuff>();

    public void AddBuff(IBuff buff)
    {
        buff.Apply(this);
        activeBuffs.Add(buff);
    }

    public void RemoveBuff(IBuff buff)
    {
        buff.Remove(this);
        activeBuffs.Remove(buff);
    }
}

public interface IBuff
{
    void Apply(BuffController controller);
    void Remove(BuffController controller);
}
