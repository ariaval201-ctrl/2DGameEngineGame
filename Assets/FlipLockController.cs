using UnityEngine;

public class FlipLockController : MonoBehaviour
{
    public CharacterMovement movement;      
    public PlatformToggle platformToggle;   

    void Update()
    {
        if (movement == null || platformToggle == null) return;

        
        movement.canFlip = !platformToggle.PlatformActive;
    }
}