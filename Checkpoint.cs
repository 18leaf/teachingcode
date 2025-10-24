using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCheckpointSystem player = other.GetComponent<PlayerCheckpointSystem>();
            if (player != null)
                player.UpdateCheckpoint(transform.position);
        }
    }
}
