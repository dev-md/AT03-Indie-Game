using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity = 3.25f;
    public float speed = 1f;
    [Range(0f, 1f)] public float crouchSpeedMultiplier = 0.5f;
    public float jumpForce = 1.25f;

    private CharacterController controller;
    private Transform trans;
    private Vector3 motion;
    private float curSpeed = 0f;
    private float velocity = 0f;
    private bool crouching = false;

    //Before Game Frame
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        trans = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        curSpeed = speed;
        //Debug.Log();
    }

    private void FixedUpdate()
    {
        if (controller != null)
        {
            motion = Vector3.zero;
            bool grounded = controller.isGrounded;
            if (grounded == true)
            {
                velocity = -gravity * Time.deltaTime;
            }
            else
            {
                velocity -= gravity * Time.deltaTime;
            }

            if (crouching == false)
            {
                if (Input.GetKey(KeyCode.LeftShift) == true)
                {
                    crouching = true;
                    trans.localScale = new Vector3(1, 0.5f, 1);
                }
                if (controller.isGrounded == true)
                {
                    curSpeed = speed;
                }
                else if (controller.isGrounded == false)
                {
                    curSpeed = speed;
                }
            }
            else if (crouching == true)
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    crouching = false;
                    curSpeed = speed;
                    trans.localScale = new Vector3(1, 1, 1);
                }

                if (controller.isGrounded == true)
                {
                    curSpeed = speed * crouchSpeedMultiplier;
                }
                else if (controller.isGrounded == false)
                {
                    curSpeed = speed;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null)
        {
            if ((controller.isGrounded == true) && (Input.GetKey(KeyCode.Space) == true))
            {
                velocity = jumpForce;

            }
            ApplyMovement();

        }
    }

    void ApplyMovement()
    {
        float inputX = Input.GetAxisRaw("Vertical") * curSpeed;
        float inputY = Input.GetAxisRaw("Horizontal") * curSpeed;

        motion += transform.forward * inputX * Time.deltaTime;
        motion += transform.right * inputY * Time.deltaTime;
        motion.y += velocity * Time.deltaTime;

        controller.Move(motion);
    }
}
