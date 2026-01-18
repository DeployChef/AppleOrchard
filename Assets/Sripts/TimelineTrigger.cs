using UnityEngine;

public class TimelineTrigger : MonoBehaviour
{
    [Header("Timeline")]
    public UnityEngine.Playables.PlayableDirector timeline;

    [Header("Trigger Settings")]
    public bool triggerOnce = true;
    public string playerTag = "Player";

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!triggerOnce || !hasTriggered)
            {
                PlayTimeline();
                hasTriggered = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!triggerOnce || !hasTriggered)
            {
                PlayTimeline();
                hasTriggered = true;
            }
        }
    }

    public void PlayTimeline()
    {
        if (timeline != null)
        {
            timeline.Play();
            Debug.Log("Таймлайн запущен!");
        }
        else
        {
            Debug.LogWarning("Timeline не назначен!");
        }
    }
}