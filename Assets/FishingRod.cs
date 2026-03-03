using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class FishingRod : MonoBehaviour
{
    [Header("Hook Settings")]
    public GameObject HookPrefab;
    public Vector3 spawnOffset = Vector3.zero;
    private GameObject currentHook;

    [Header("Rope Settings")]
    public GameObject ropeSegmentPrefab;
    public int segmentCount = 15;
    private List<GameObject> ropeSegments = new List<GameObject>();

    [Header("References")]
    public CameraManager cameraManager;
    public CinemachineCamera myCamera;
    public CharacterMovement characterMovement;
    public GameObject otherPlayer;

    void Update()
    {
        if (cameraManager.currentCam == myCamera)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentHook == null)
                {
                    SpawnHook();
                    characterMovement.canMove = false;
                }
                else
                {
                    DestroyHook();
                    characterMovement.canMove = true;
                }
            }
        }

        // Continuously update rope positions
        if (currentHook != null)
        {
            UpdateRope();
        }
    }

    private void SpawnHook()
{
    // Determine direction
    float direction = characterMovement.FacingRight ? 1f : -1f;

    // Flip the X offset depending on facing direction
    Vector3 adjustedOffset = new Vector3(
        spawnOffset.x * direction,
        spawnOffset.y,
        spawnOffset.z
    );

    Vector3 spawnPosition = transform.position + adjustedOffset;

    currentHook = Instantiate(HookPrefab, spawnPosition, Quaternion.identity);

    // Optional: visually flip the hook
    currentHook.transform.localScale = new Vector3(direction, 1f, 1f);

    HookScript hookScript = currentHook.GetComponent<HookScript>();
    if (hookScript != null)
    {
        hookScript.player1 = this.gameObject;
        hookScript.player2 = otherPlayer;
    }

    CreateRope();
}

    private void CreateRope()
    {
        ropeSegments.Clear();

        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = Instantiate(ropeSegmentPrefab);
            ropeSegments.Add(segment);
        }
    }

    private void UpdateRope()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = currentHook.transform.position;

        for (int i = 0; i < ropeSegments.Count; i++)
        {
            float t = i / (float)(ropeSegments.Count - 1);
            Vector3 position = Vector3.Lerp(startPoint, endPoint, t);

            ropeSegments[i].transform.position = position;

            // Rotate to face next segment for better look
            if (i < ropeSegments.Count - 1)
            {
                Vector3 dir = ropeSegments[i + 1].transform.position - position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                ropeSegments[i].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void DestroyHook()
    {
        if (currentHook != null)
        {
            Destroy(currentHook);
            currentHook = null;
        }

        // Destroy rope segments
        foreach (GameObject segment in ropeSegments)
        {
            Destroy(segment);
        }

        ropeSegments.Clear();
    }
}