using UnityEngine;

/// <summary>
/// Grants temporary invulnerability.
/// </summary>
public class ShieldBoost : MonoBehaviour, IBuff
{
    public float duration = 5f;

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
        // TODO: Remove invulnerability
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
