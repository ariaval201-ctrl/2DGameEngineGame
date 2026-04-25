using UnityEngine;
using Unity.Cinemachine;

public class FishingRod : MonoBehaviour
{
    public GameObject hookPrefab;
    public Transform hookSpawnPoint;
    public float hookSpeed = 5f;

    public Vector2 spawnOffset = Vector2.right; 

    public CameraManager cameraManager;
    public CinemachineCamera myCamera;

    private GameObject currentHook;

    void Update()
    {   
        if (cameraManager.currentCam == myCamera)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentHook == null)
                {
                    SpawnHook();
                }
            }
        }
    }

    void SpawnHook()
    {
        // Convert local offset into world space based on rod rotation
        Vector3 worldOffset = hookSpawnPoint.TransformDirection(spawnOffset);

        Vector3 spawnPos = hookSpawnPoint.position + worldOffset;

        currentHook = Instantiate(hookPrefab, spawnPos, Quaternion.identity);

        HookController hookController = currentHook.GetComponent<HookController>();
        hookController.Initialize(transform, hookSpeed);

        cameraManager.hookCamera.Follow = currentHook.transform;
        cameraManager.hookCamera.LookAt = currentHook.transform;

        cameraManager.SwitchCamera(cameraManager.hookCamera);
    }   
}