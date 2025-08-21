using UnityEngine;

/// <summary>
/// Slows down time for a short duration.
/// </summary>
public class SlowTime : MonoBehaviour, IBuff
{
    public float duration = 5f;
    public float timeScale = 0.5f;

    public void Apply(BuffController controller)
    {
        Time.timeScale = timeScale;
        controller.StartCoroutine(Expire(controller));
    }

    private System.Collections.IEnumerator Expire(BuffController controller)
    {
        yield return new WaitForSecondsRealtime(duration);
        controller.RemoveBuff(this);
    }

    public void Remove(BuffController controller)
    {
        Time.timeScale = 1f;
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
