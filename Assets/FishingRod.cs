using UnityEngine;
using Unity.Cinemachine;

public class FishingRod : MonoBehaviour
{
    public GameObject hookPrefab;
    public Transform hookSpawnPoint;
    public float hookSpeed = 5f;

    public Vector2 spawnOffset = Vector2.zero;

    public CameraManager cameraManager;
    public CinemachineCamera myCamera;
    public CinemachineCamera hookCamera;

    public Animator playerAnimator;
    public SpriteRenderer spriteRenderer;
    public CharacterMovement movement;

    private GameObject currentHook;
    private bool isCasting;

    void Awake()
    {
        if (playerAnimator == null)
            playerAnimator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (movement == null)
            movement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (cameraManager.currentCam == myCamera)
        {
            if (Input.GetKeyDown(KeyCode.E) &&
                !isCasting &&
                currentHook == null &&
                movement.IsGrounded())
            {
                SpawnHook();
            }
        }

        SetCasting(currentHook != null || isCasting);
    }

    void SpawnHook()
    {
        isCasting = true;

        float direction = spriteRenderer.flipX ? -1f : 1f;

        Vector3 offset = new Vector3(spawnOffset.x * direction, spawnOffset.y, 0f);
        Vector3 spawnPos = hookSpawnPoint.position + hookSpawnPoint.TransformDirection(offset);

        currentHook = Instantiate(hookPrefab, spawnPos, Quaternion.identity);

        HookController hookController = currentHook.GetComponent<HookController>();

        hookController.Initialize(hookSpeed);

        // 🎥 camera setup
        hookCamera.Follow = currentHook.transform;
        hookCamera.LookAt = currentHook.transform;

        hookController.cameraManager = cameraManager;
        hookController.returnCamera = cameraManager.chopCamera;

        cameraManager.SwitchCamera(hookCamera);

        isCasting = false;
    }

    void SetCasting(bool value)
    {
        if (playerAnimator != null)
            playerAnimator.SetBool("IsCasting", value);
    }
}