using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//andeling player movment
public class BasicPlayerScript : MonoBehaviour
{
    #region Variables
    public float playerMovementSpeed = 5.0f;
    public float playerJumpPower = 5.0f;
    [Tooltip("What should be considered as ground")]
    [SerializeField] LayerMask platformLayerMask;
    /// <summary>
    /// The mouse movment on the X axis
    /// </summary>
    float mouseX = 0f;
    /// <summary>
    /// The mouse movment on the Y axis
    /// </summary>
    float mouseY = 0f;
    public float mouseSeneitiivity = 100f;
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        PlayerMovment();
        PlayerJump();
        CameraMouseLook();
    }

    /// <summary>
    /// Basic player movment using Arrows or WASD
    /// </summary>
    public void PlayerMovment()
    {
        //sides
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * playerMovementSpeed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * playerMovementSpeed;
        transform.Translate(x, 0, z);
    }

    /// <summary>
    /// Rotate the Camera Around the player form mouse movment
    /// </summary>
    void CameraMouseLook()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSeneitiivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSeneitiivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// Jump if pressed Space button
    /// </summary>
    public void PlayerJump()
    {
        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                this.GetComponent<Rigidbody>().AddForce(transform.up * playerJumpPower);
            }
        }
    }

    /// <summary>
    /// Is the player on the ground (Using Raycast)
    /// </summary>
    public bool IsGrounded()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f, platformLayerMask))
        {
            return true;
        }
        return false;
    }
}
