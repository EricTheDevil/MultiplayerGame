using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] float vertInput = 5f;
    private bool isJumping;
    public Animator anim;

    public bool fellDown;
    public GameObject A;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        charController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (!fellDown)
        {
            PlayerMovement();
        }
    }

    public void PlayerMovement()
    {
        if (!fellDown)
        {
            float horizInput = Input.GetAxis("Horizontal");
            //float vertInput = Input.GetAxis(verticalInputName);

            Vector3 forwardMovement = transform.right * vertInput;
            Vector3 rightMovement = -transform.forward * horizInput * 5f;

            // ramp up the speed until you get hit
            charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

            if ((horizInput != 0) && OnSlope())
                charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);

            JumpInput();
        }
    }

    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 10f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }
}
