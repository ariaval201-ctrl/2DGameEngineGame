using UnityEngine;
using Unity.Cinemachine;
using System.Collections; // needed for coroutines

public class PlatformToggle : MonoBehaviour
{
    public GameObject platformPrefab;
    private GameObject currentPlatform;

    public bool PlatformActive => currentPlatform != null;

    public CameraManager cameraManager;
    public CinemachineCamera myCamera; 
    public CharacterMovement characterMovement;

    [Header("Platform Spawn Settings")]
    public float spawnDistance = 1.44f;
    public float spawnHeight = 0f;

    [Header("Animation Settings")]
    public Animator spawnAnimator;     
    public Animator despawnAnimator; 
    public string spawnTrigger = "Spawn"; 
    public string despawnTrigger = "despawn";
    public float spawnAnimDuration = 0.5f; 
    public float despawnAnimDuration = 0f;

    void Update()
    {
        //makes sure your on croc
        if (cameraManager.currentCam == myCamera && Input.GetKeyDown(KeyCode.E))
        {
            if (currentPlatform == null)
            {
                // starts animation THEN spawns platform
                StartCoroutine(SpawnPlatformAnimation());
            }
            else
            {
                StartCoroutine(DespawnPlatformAnimation());
            }
        }
    }

    private IEnumerator SpawnPlatformAnimation()
    {
        
        if (spawnAnimator != null)
            spawnAnimator.SetTrigger(spawnTrigger);

        
        yield return new WaitForSeconds(spawnAnimDuration);

        // spawns the platform
        float direction = characterMovement.FacingRight ? 1f : -1f;
        Vector3 spawnPosition = transform.position + new Vector3(direction * spawnDistance, spawnHeight, 0f);
        currentPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        characterMovement.canMove = false; 
    }

    private IEnumerator DespawnPlatformAnimation()
{
    
    if (currentPlatform != null)
        Destroy(currentPlatform);

    currentPlatform = null;
    characterMovement.canMove = true;

    
    if (despawnAnimator != null)
        despawnAnimator.SetTrigger(despawnTrigger);

    
    yield return new WaitForSeconds(despawnAnimDuration);
}


}