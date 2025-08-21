using UnityEngine;

/// <summary>
/// Pulls nearby coins toward the player.
/// </summary>
public class Magnet : MonoBehaviour, IBuff
{
    public float duration = 5f;
    public float radius = 5f;

    public void Apply(BuffController controller)
    {
        controller.StartCoroutine(Expire(controller));
    }

    private System.Collections.IEnumerator Expire(BuffController controller)
    {
        yield return new WaitForSeconds(duration);
        controller.RemoveBuff(this);
    }

    public void Remove(BuffController controller)
    {
        // TODO: Remove magnet effect
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
