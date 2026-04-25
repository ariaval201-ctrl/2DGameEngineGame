using UnityEngine;

public class CameraLookDown : MonoBehaviour
{
    [Header("Vertical Look Settings")]
    public float verticalOffset = 0.75f;
    public float smoothSpeed = 8f;

    private Vector3 startLocalPos;
    private Vector3 targetLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
        targetLocalPos = startLocalPos;
    }

    void Update()
    {
        bool holdingDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool holdingUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        Vector3 offset = Vector3.zero;

        // prioritize canceling if both pressed
        if (holdingDown && !holdingUp)
        {
            offset = Vector3.down * verticalOffset;
        }
        else if (holdingUp && !holdingDown)
        {
            offset = Vector3.up * verticalOffset;
        }

        targetLocalPos = startLocalPos + offset;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetLocalPos,
            Time.deltaTime * smoothSpeed
        );
    }
}