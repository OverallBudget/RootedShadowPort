
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float sensitivity;
    [SerializeField] float sprintSpeed;

    [SerializeField] float jumpForce;
    [SerializeField] float gravity = 9.81f;
    private float moveFB;

    private float moveLR;
    private float rotX;
    private float rotY;

    private Vector3 jumpVelocity = Vector3.zero;
    private Camera playerCam;
    private CharacterController cc;

    private float originalFOV;
    public bool isMoving;
    public Vector3 initialPos;




    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
        playerCam = transform.Find("Camera").GetComponent<Camera>();
        originalFOV = playerCam.fieldOfView; // Store the original FOV
        isMoving = false;
        initialPos = transform.position; // Store the initial position of the player
    }

    void Update()
    {

        Move();
        Shoot();
        if(transform.position.y < initialPos.y)
        {
            speed += 15f * Time.deltaTime; // Increase speed when falling
            sprintSpeed += 15f * Time.deltaTime; // Increase sprint speed when falling
        }


    }


    private void Move()
    {

        float movementSpeed = speed;



        if (Input.GetKey(KeyCode.LeftShift) && isMoving)
        {

            movementSpeed = sprintSpeed;
            playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, originalFOV + 10f, Time.deltaTime * 4); // Increase FOV when sprinting
        }

        else
        {

            movementSpeed = speed;
            playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, originalFOV, Time.deltaTime * 4); // Reset FOV
        }



        moveFB = Input.GetAxis("Vertical") * movementSpeed;
        moveLR = Input.GetAxis("Horizontal") * movementSpeed;
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;



        if (moveFB == 0 && moveLR == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }




        rotY = Mathf.Clamp(rotY, -60f, 60f);

        Vector3 movement = new Vector3(moveLR, 0, moveFB).normalized * movementSpeed;


        if (cc.isGrounded)
        {

            if (jumpVelocity.y < 0)
            {
                jumpVelocity.y = -2f;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {

                jumpVelocity.y = jumpForce;
            }
        }


        if (!cc.isGrounded)
        {
            jumpVelocity.y -= gravity * Time.deltaTime;
        }



        transform.Rotate(0, rotX, 0);


        playerCam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        movement = transform.rotation * movement;
        cc.Move((movement + jumpVelocity) * Time.deltaTime);
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 100f))
            {
                Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * hit.distance, Color.red); // Draw a ray in the scene view
                Debug.Log($"Hit: {hit.collider.name}"); // Log the name of the object hit
                hit.collider.GetComponent<Damage>()?.TakeDamage(25); // Call the TakeDamage method on the hit object if it has a Damage component




            }
        }
    }
}