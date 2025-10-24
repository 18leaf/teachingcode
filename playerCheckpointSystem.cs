using UnityEngine;

public class PlayerCheckpointSystem : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public float fallThreshold = 20f;  // how far below checkpoint before respawn
    private Vector3 currentCheckpoint;
    private float yThreshold;

    private void Start()
    {
        currentCheckpoint = transform.position; // initial spawn
        yThreshold = currentCheckpoint.y - fallThreshold;
    }

    private void Update()
    {
        if (transform.position.y < yThreshold)
            Respawn();
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
        yThreshold = currentCheckpoint.y - fallThreshold;
        Debug.Log("Checkpoint updated to: " + currentCheckpoint);
    }

    private void Respawn()
    {
        transform.position = currentCheckpoint;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        Debug.Log("Respawned at checkpoint: " + currentCheckpoint);
    }
}
