using UnityEngine;
using Unity.Cinemachine;

public class FishingRod : MonoBehaviour
{
    public GameObject hookPrefab;
    public Transform hookSpawnPoint;
    public float hookSpeed = 5f;
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
        currentHook = Instantiate(hookPrefab, hookSpawnPoint.position, Quaternion.identity);
        currentHook.GetComponent<HookController>().Initialize(transform, hookSpeed);
    }
}