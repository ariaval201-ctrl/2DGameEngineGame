using UnityEngine;
using Unity.Cinemachine;

public class HookController : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 returnPos;
    private float speed;

    private bool goingDown = true;

    private Transform HookedCroc;
    private Animator HookedCrocAnimator;

    public float maxDistance = 5f;
    public PlatformToggle platformToggle;

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
            //math to make it go down
            transform.position += Vector3.down * speed * Time.deltaTime;

            if (Vector3.Distance(startPos, transform.position) >= maxDistance)
                goingDown = false;

            return;
        }

        MoveBack();

            //
        if (Vector3.Distance(transform.position, returnPos) <= 0.1f)
        {
            FinishHook();
        }
    }

    void MoveBack()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            returnPos,
            speed * Time.deltaTime
        );

        // 🎯 move hooked player with hook
        if (HookedCroc != null)
            HookedCroc.position = transform.position;

        // 🎬 animation while being carried
        if (HookedCrocAnimator != null)
            HookedCrocAnimator.SetBool("IsCaught", true);
    }

    void FinishHook()
    {
        if (HookedCroc != null)
        {
            //code to make sure if it hits player 2 it sets the hook as parent and croc follows the hook
            HookedCroc.SetParent(null);
        }

        // code to stop animationn
        if (HookedCrocAnimator != null)
            HookedCrocAnimator.SetBool("IsCaught", false);

        // code to bring camera back to chop
        if (cameraManager != null && returnCamera != null)
        {
            cameraManager.SwitchCamera(returnCamera);
        }
        //gets rid of the hook
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // code for error noise
        if (!collision.CompareTag("Player2"))
            return;

        if (platformToggle != null && platformToggle.PlatformActive)
            return;

        goingDown = false;

        HookedCroc = collision.transform;
        HookedCroc.SetParent(transform);

        HookedCrocAnimator = collision.GetComponent<Animator>();
    }
}