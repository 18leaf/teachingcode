using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollaPlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float acceleration = 25f;       // force per second
    [SerializeField] float maxSpeed = 8f;            // optional cap
    [SerializeField] float airControlScale = 0.6f;   // 1.0 if you want full control mid-air
    [SerializeField] float groundRay = 0.6f;         // slightly > radius

    [Header("References")]
    [SerializeField] Camera cam;                     // assign Main Camera in inspector

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!cam) cam = Camera.main;
    }

    void FixedUpdate()
    {
        // 1) Read input (classic Input Manager)
        float ix = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float iz = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        // no input: early out (optional)
        if (Mathf.Approximately(ix, 0f) && Mathf.Approximately(iz, 0f))
            return;

        // 2) Build camera-relative axes on the ground plane (XZ)
        Vector3 camFwd = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        // Project onto ground plane to ignore camera tilt
        camFwd = Vector3.ProjectOnPlane(camFwd, Vector3.up).normalized;
        camRight = Vector3.ProjectOnPlane(camRight, Vector3.up).normalized;

        // 3) Desired direction in world space relative to camera
        Vector3 desiredDir = (camFwd * iz + camRight * ix);
        if (desiredDir.sqrMagnitude > 1f) desiredDir.Normalize();

        // 4) Optional ground check for reduced air control
        bool grounded = Physics.Raycast(transform.position, Vector3.down, groundRay);
        float control = grounded ? 1f : airControlScale;

        // 5) Apply acceleration as force (Acceleration mode is mass-independent)
        rb.AddForce(desiredDir * (acceleration * control), ForceMode.Acceleration);

        // 6) Optional horizontal speed cap
        Vector3 v = rb.velocity;
        Vector3 vHoriz = new Vector3(v.x, 0f, v.z);
        if (vHoriz.magnitude > maxSpeed)
        {
            vHoriz = vHoriz.normalized * maxSpeed;
            rb.velocity = new Vector3(vHoriz.x, v.y, vHoriz.z);
        }
    }
}
