using UnityEngine;

public class CharacterSleep : MonoBehaviour
{
    [Header("Sleep Settings")]
    public Animator animator;     
    public float idleTime = 5f;   

    private float idleTimer = 0f;
    private CharacterMovement movement;

    void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        
        bool isMoving = Mathf.Abs(movement.HorizontalInput) > 0.1f;

        if (isMoving)
        {
           
            idleTimer = 0f;
        }
        else
        {
            
            idleTimer += Time.deltaTime;

           
            if (idleTimer >= idleTime)
            {
                animator.SetTrigger("FallAsleep");
                idleTimer = 0f; 
            }
        }
    }
}
