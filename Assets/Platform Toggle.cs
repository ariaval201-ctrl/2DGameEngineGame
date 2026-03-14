using UnityEngine;
using Unity.Cinemachine;

public class PlatformToggle : MonoBehaviour
{
    public GameObject platformPrefab;
    private GameObject currentPlatform;

    public bool PlatformActive => currentPlatform != null;

    public CameraManager cameraManager;
    public CinemachineCamera myCamera; 
    public CharacterMovement characterMovement;

    [Header("Platform Spawn Settings")]
    public float spawnDistance = 1.44f;   // <-- editable in Unity
    public float spawnHeight = 0f;        // optional vertical offset

    void Update()
    {
        if (cameraManager.currentCam == myCamera)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentPlatform == null)
                {
                    float direction = characterMovement.FacingRight ? 1f : -1f;

                    Vector3 spawnPosition = transform.position +
                        new Vector3(direction * spawnDistance, spawnHeight, 0f);

                    currentPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

                    characterMovement.canMove = false; 
                }
                else
                {
                    Destroy(currentPlatform);
                    currentPlatform = null;

                    characterMovement.canMove = true; 
                }
            }
        }
    }
}