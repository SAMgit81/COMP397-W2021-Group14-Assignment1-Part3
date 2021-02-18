using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float playerSpeed = 12.0f;
    public CharacterController controller;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal") ;
        float z = Input.GetAxis("Vertical") ;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * playerSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }



    //[SerializeField] Transform playerCamera = null;
    //[SerializeField] float mouseSensitivity = 3.5f;
    //[SerializeField] bool lockCursor = true;
    //[SerializeField] float walkSpeed = 6.0f;
    //[SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    //[SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    //[SerializeField] float gravity = -13.0f;
    //float cameraPitch = 0.0f;
    //CharacterController controller = null;

    //Vector2 currentDir = Vector2.zero;
    //Vector2 currentDirVelocity = Vector2.zero;

    //float velocityY = 0.0f;

    //Vector2 currentMouseDelta = Vector2.zero;
    //Vector2 currentMouseDeltaVelocity = Vector2.zero;
    //private void Start()
    //{
    //    controller = GetComponent<CharacterController>();
    //    if (lockCursor)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //    }
    //}
    //void Update()
    //{
    //    UpdateMouseLook();
    //    UpdateMovement();
    //}
    //void UpdateMouseLook()
    //{
    //    Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    //    currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

    //    cameraPitch -= currentMouseDelta.y * mouseSensitivity;
    //    cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
    //    playerCamera.localEulerAngles = Vector3.right * cameraPitch;
    //    transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity); 

    //}

    //void UpdateMovement()
    //{
    //    Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //    targetDir.Normalize();

    //    currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
    //    if (controller.isGrounded)
    //    {
    //        velocityY = 0.0f;
    //    }

    //    velocityY += gravity * Time.deltaTime;
    //    Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

    //    controller.Move(velocity * Time.deltaTime);
    //}
}
