using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody rb;
    InputAction moveAction, jumpAction, sprintAction;

    public Vector2 movement;
    public bool isSprinting;
    public float speed = 3;
    public float sprintSpeed = 5;
    public float jumpForce = 10;
    public float jump2Force = 20;
    public float grav = 5;
    private float currentSpeed;
    public float smoothTime = 0.05f;
    public float rotationSpeed = 10f;
    public Vector3 moveVec;

    public Transform foot;
    public bool grounded;
    public bool jump;
    public bool jump2;
    public bool sprint;

    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

    Animator animator;

    public LayerMask mask;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
    }

    // Update is called once per frame
    void Update()
    {
        movement = moveAction.ReadValue<Vector2>();
        bool isMov = movement.magnitude > 0;
        animator.SetBool("IsMoving", isMov);
        //float horMove = 0;
        //if(Keyboard.current.aKey.isPressed) { horMove = -1; }
        //if(Keyboard.current.dKey.isPressed) { horMove = 1;  }

        movement = moveAction.ReadValue<Vector2>();

        //Vector3 moveVec = new Vector3(horMove, 0, 0) * Time.deltaTime * speed;
        //rb.MovePosition(moveVec + transform.position);

        sprint = sprintAction.IsPressed();

        jump = (jumpAction.WasPressedThisFrame() || jump);
        rb.AddForce(Vector3.down * grav);

        

    }

    private void FixedUpdate()
    {

        //Movement calcs
        Ray ray = new Ray(transform.position, -transform.up);
        Vector3 mover = new Vector3(0,0,0);
        if (Physics.Raycast(ray, out RaycastHit hit, .2f))
        {
            if (hit.collider.CompareTag("platMov"))
            {
                PlatMove plat = hit.collider.GetComponent<PlatMove>();
                if (plat != null)
                {
                    mover = plat.movement;
                }
            }
        }

        Vector3 forComponent = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized * movement.y;
        Vector3 horComponent = Camera.main.transform.right * movement.x;
        moveVec = (forComponent + horComponent) * currentSpeed * Time.deltaTime;

        rb.MovePosition(moveVec + transform.position + mover);

        if (movement.sqrMagnitude != 0)
        {
            float targetAngle = Mathf.Atan2(moveVec.x, moveVec.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentSpeed, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        //grounded :(
        grounded = Physics.CheckSphere(foot.position, .2f, mask);

        //Jumpy time!!

        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (jump && coyoteTimeCounter > 0f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
            jump2 = true;
            coyoteTimeCounter = 0f;
        }
        if (jump && !grounded && jump2)
        {
            rb.AddForce(Vector3.up * jump2Force, ForceMode.Impulse);
            jump2 = false;
        }

        //i am speed
        if (sprint)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(foot.position, .2f);
    }
}
