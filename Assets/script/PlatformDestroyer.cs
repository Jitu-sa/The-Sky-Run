using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="chest")
        {
            Destroy(gameObject);
        }
    }
}
