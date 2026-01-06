using UnityEngine;
using UnityEngine.InputSystem;

public class DroneFpsCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 12f;
    public float sprintMultiplier = 2f;
    public float verticalSpeed = 8f;
    public float acceleration = 10f;
    public float damping = 10f;

    [Header("Look")]
    public float mouseSensitivity = 0.12f;
    public float minPitch = -85f;
    public float maxPitch = 85f;

    [Header("Cursor")]
    public bool lockCursorOnStart = true;

    private InputSystem_Actions input;

    private float yaw;
    private float pitch;
    private Vector3 velocity;

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    void OnEnable()
    {
        input.Enable();

        yaw = transform.eulerAngles.y;
        pitch = NormalizeAngle(transform.eulerAngles.x);

        if (lockCursorOnStart)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        //read inputs every frame
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();
        Vector2 lookInput = input.Player.Look.ReadValue<Vector2>();
        bool sprintHeld = input.Player.Sprint.IsPressed();
        float upDownInput = input.Player.UpDown.ReadValue<float>();

        HandleLook(lookInput);
        HandleMove(moveInput, upDownInput, sprintHeld);

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            bool locked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = locked;
        }
    }

    void HandleLook(Vector2 lookInput)
    {
        yaw += lookInput.x * mouseSensitivity;
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleMove(Vector2 moveInput, float upDownInput, bool sprintHeld)
    {
        
        moveInput = Vector2.ClampMagnitude(moveInput, 1f);//equalize diagonal speed 

        float speed = moveSpeed * (sprintHeld ? sprintMultiplier : 1f);

        Vector3 targetVelocity =
            (transform.forward * moveInput.y + transform.right * moveInput.x) * speed
            + Vector3.up * (upDownInput * verticalSpeed);

        float accel = targetVelocity.sqrMagnitude > 0.01f ? acceleration : damping;
        velocity = Vector3.Lerp(velocity, targetVelocity, accel * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
