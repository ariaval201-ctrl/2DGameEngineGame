using UnityEngine;
using Unity.Cinemachine;

public class PlatformToggle : MonoBehaviour
{
    public GameObject platformPrefab;
    private GameObject currentPlatform;

    public CameraManager cameraManager;
    public CinemachineCamera myCamera; // assign the crocCamera here

    public CharacterMovement characterMovement;

    void Update()
    {
        // Check if this camera is currently active
        if (cameraManager.currentCam == myCamera)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentPlatform == null)
                {
                    Vector3 spawnPosition = transform.position + Vector3.right * 1.44f;
                    currentPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

                    characterMovement.canMove = false; // STOP movement
                }
                else
                {
                    Destroy(currentPlatform);
                    currentPlatform = null;

                    characterMovement.canMove = true; // ALLOW movement
                }
            }
        }
    }
}