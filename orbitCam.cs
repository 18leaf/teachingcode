// SETUP -> On MainCamera -- Look at Target
using UnityEngine;

public class OrbitCamera : MonoBehavior
{
    [SerializeField] private Transform target;
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private float maxOrbitDistance = 10f;
    [SerializeField] private float minOrbitDistance = 2f;

    private float orbitRadius = 5f;

    private bool isCamActive = true;
    private float mouseX = 0;
    private float mouseY = 0;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            // toggle on key c
            isCamActive = !isCamActive;

            // reset on deactivate
            if (!isCamActive)
            {
                transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                transform.rotation = Quaternion.identity;
            }
            else
            {
                // reset to default
                orbitRadius = 5f;
            }
        }

        if (isCamActive)
        {
            // if player is clicing and looking
            if (Input.GetMouseButton(0))
            {
                // look at the target
                transform.LookAt(target);

                // conv mosue pos to rotation
                mouseX = Input.GetAxis("Mouse X");
                mouseY = Input.GetAxis("Mouse Y");
                transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);
            }

            // radius hwo far up/down
            orbitRadius -= Input.mouseScrollDelta.y / sensitivity;
            orbitRadius = Mathf.Clamp(orbitRadius, minOrbitDistance, maxOrbitDistance);

            transform.position = target.position - transform.forward * orbitRadius;
        }
    }

}