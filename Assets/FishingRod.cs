using UnityEngine;
using Unity.Cinemachine;

public class FishingRod : MonoBehaviour
{
    public GameObject hookPrefab;
    public Transform hookSpawnPoint;
    public float hookSpeed = 5f;
    public CameraManager cameraManager;
    public CinemachineCamera myCamera;

    void Update()
    {   
    if(cameraManager.currentCam == myCamera)
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnHook();
        }
    }

    void SpawnHook()
    {
        GameObject hook = Instantiate(hookPrefab, hookSpawnPoint.position, Quaternion.identity);
        hook.GetComponent<HookController>().Initialize(transform, hookSpeed);
    }
}