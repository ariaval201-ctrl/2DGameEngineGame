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

    void Update()
    {
        if (cameraManager.currentCam == myCamera)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentPlatform == null)
                {
                    float direction = characterMovement.FacingRight ? 1f : -1f;
                    Vector3 spawnPosition = transform.position + new Vector3(direction * 1.44f, 0f, 0f);
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