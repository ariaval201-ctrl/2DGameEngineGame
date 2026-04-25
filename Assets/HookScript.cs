using UnityEngine;
using Unity.Cinemachine;

public class HookController : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 returnPos;
    private float speed;

    private bool goingDown = true;
    private Transform caughtPlayer;

    public float maxDistance = 5f;
    public PlatformToggle platformToggle;

    // 🎥 camera control
    public CameraManager cameraManager;
    public CinemachineCamera returnCamera;

    void Start()
    {
        platformToggle = FindObjectOfType<PlatformToggle>();
    }

    public void Initialize(float hookSpeed)
    {
        startPos = transform.position;
        returnPos = transform.position;
        speed = hookSpeed;
    }

    void Update()
    {
        if (goingDown)
        {
            // ✅ world-space movement (stable)
            transform.position += Vector3.down * speed * Time.deltaTime;

            if (Vector3.Distance(startPos, transform.position) >= maxDistance)
                goingDown = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                returnPos,
                speed * Time.deltaTime
            );

            if (caughtPlayer != null)
                caughtPlayer.position = transform.position;

            if (Vector3.Distance(transform.position, returnPos) < 0.1f)
            {
                if (caughtPlayer != null)
                    caughtPlayer.SetParent(null);

                // 🎥 return camera BEFORE destroy
                if (cameraManager != null && returnCamera != null)
                {
                    cameraManager.SwitchCamera(returnCamera);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2") &&
            platformToggle != null &&
            !platformToggle.PlatformActive)
        {
            goingDown = false;
            caughtPlayer = collision.transform;
            caughtPlayer.SetParent(transform);
        }
    }
}