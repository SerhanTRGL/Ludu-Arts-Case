using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5f;
    public float Acceleration = 20f;
    public float Drag = 5f;
    public float RotationSpeed = 720f; //degrees per second

    [SerializeField] private float m_InputDeadzone = 0.01f;


    private Rigidbody m_PlayerRigidbody;
    private InputAction m_MoveAction;
    private InputAction m_InteractAction;

    [Header("Input Action References")]
    [SerializeField] private InputActionReference m_MoveActionReference;
    [SerializeField] private InputActionReference m_InteractActionReference;

    private void Awake() {
        m_PlayerRigidbody = GetComponent<Rigidbody>();
        m_PlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezePositionY;
        m_PlayerRigidbody.linearDamping = Drag;
    }

    private void OnEnable() {
        BindActions();
        m_MoveAction?.Enable();
        m_InteractAction?.Enable();
    }

    private void FixedUpdate() {
        Vector2 input = m_MoveAction.ReadValue<Vector2>();

        if (input.sqrMagnitude < m_InputDeadzone) return;


        Vector3 desiredMovementDir = new Vector3(input.x, 0, input.y).normalized;

        MovePlayer(desiredMovementDir);
        RotatePlayer(desiredMovementDir);

    }

    private void OnDisable() {
        m_MoveAction?.Disable();
        m_InteractAction?.Disable();
    }

    private void MovePlayer(Vector3 movementDir) {
        Vector3 targetVelocity = movementDir * MoveSpeed;
        Vector3 currentVelocity = m_PlayerRigidbody.linearVelocity;
        Vector3 velocityChange = targetVelocity - currentVelocity;

        Vector3 force = velocityChange * Acceleration;
        m_PlayerRigidbody.AddForce(force, ForceMode.Acceleration);
    }

    private void RotatePlayer(Vector3 movementDir) {
        if (movementDir.sqrMagnitude > 0.001f) {
            Quaternion targetRot = Quaternion.LookRotation(movementDir, Vector3.up);
            Quaternion newRot = Quaternion.RotateTowards(
                m_PlayerRigidbody.rotation,
                targetRot,
                RotationSpeed * Time.fixedDeltaTime
            );

            m_PlayerRigidbody.MoveRotation(newRot);
        }
    }

    private void BindActions() {
        if (m_MoveActionReference != null) {
            m_MoveAction = m_MoveActionReference.action;
        }
        else {
            Debug.LogError("Missing Move Input Action Reference!");
        }

        if (m_InteractActionReference != null) {
            m_InteractAction = m_InteractActionReference.action;
        }
        else {
            Debug.LogError("Missing Interact Input Action Reference!");
        }
    }
}
