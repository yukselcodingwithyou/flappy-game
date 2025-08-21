using UnityEngine;

/// <summary>
/// Doubles coin collection for a duration.
/// </summary>
public class DoubleCoins : MonoBehaviour, IBuff
{
    public float duration = 5f;

    public void Apply(BuffController controller)
    {
        // TODO: Double coin value
        controller.StartCoroutine(Expire(controller));
    }

    private System.Collections.IEnumerator Expire(BuffController controller)
    {
        yield return new WaitForSeconds(duration);
        controller.RemoveBuff(this);
    }

    public void Remove(BuffController controller)
    {
        // TODO: Restore coin value
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuffController buffs = collision.GetComponent<BuffController>();
        if (buffs != null)
        {
            buffs.AddBuff(this);
            gameObject.SetActive(false);
        }
    }
}
