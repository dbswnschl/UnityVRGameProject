using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed = 3f;
    public GameObject cameraObject;
    public float y;
    public float cameraY;
    public bool isgrounded;
    public CharacterController characterController;
    public bool isJump = false;
    public float gravity = 20f;
    float yVelocity = 0f;
    public float jumpSpeed = 10f;
    public Vector3 moveDirection;
    public GameObject waveObject;
    public enum PlayerState
    {
        idle, walking, jumping
    };
    public PlayerState playerState;
    private void Awake()
    {
        y = transform.position.y;
        cameraY = cameraObject.transform.position.y;
        moveDirection = Vector3.zero;
    }
    private void Start()
    {

    }
    private void Update()
    {
        
        if (!characterController.isGrounded)
        {
            isgrounded = false;
            //cameraObject.transform.Translate(Vector3.down * 9.8f * Time.deltaTime);
            //characterController.Move(Vector3.down * 9.8f * Time.deltaTime);

        }
        else
        {
            isgrounded = true;
            playerState = PlayerState.idle;
            isJump = false;
            //moveDirection = new Vector3(cameraObject.transform.position.x - transform.position.x, 0f, cameraObject.transform.position.z - transform.position.z);
            moveDirection.x = cameraObject.transform.position.x - transform.position.x;
            moveDirection.y = 0f;
            moveDirection.z = cameraObject.transform.position.z - transform.position.z;




        }
        float z = cameraObject.transform.position.z - waveObject.transform.position.z;
        if (Input.GetButton("Fire1"))
        {

            if (playerState != PlayerState.walking)
            {
                playerState = PlayerState.walking;
            }
            cameraObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);

            cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, cameraObject.transform.position.y, cameraObject.transform.position.z);

            //characterController.Move(new Vector3(cameraObject.transform.position.x - transform.position.x, yVelocity, cameraObject.transform.position.z - transform.position.z));
            //transform.position = cameraObject.transform.position;

            //waveObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            waveObject.transform.position = new Vector3(waveObject.transform.position.x, waveObject.transform.position.y, cameraObject.transform.position.z+13);

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (playerState == PlayerState.walking)
            {
                playerState = PlayerState.idle;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!isJump)
            {
                playerState = PlayerState.jumping;
                isJump = true;
                moveDirection.y += jumpSpeed;

            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

    }


}
