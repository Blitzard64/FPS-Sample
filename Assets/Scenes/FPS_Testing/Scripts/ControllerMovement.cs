using UnityEngine;
using System.Collections;
public class ControllerMovement : MonoBehaviour
{
    public Vector3 movementInput;
    public Vector2 mouseInput;
    public float speed;
    public Rigidbody rb;
    public float jumpForce;
    public float x_rot;
    public Transform camera;
    public float Sensitivity;
    public int flag=0;  
    public AudioSource source;
    public AudioSource BGsource;
    public AudioClip bgMusic;
    public AudioClip footstepsSFX;
    public int audioFlag=0;
    public float walkRate;
    public float timeBetween;
    public int jumpCheck=0;
    
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "Plane" && (audioFlag == 0))
        {
            audioFlag = 1;
            BGsource.PlayOneShot(bgMusic);
        }
        if (collisionInfo.collider.tag == "jumpResetter")
        {
            flag = 0;
            jumpCheck = 0;
        }
    }
    void Update()
    {
        Debug.Log(jumpCheck);
        if (!BGsource.isPlaying)
        {
            BGsource.PlayOneShot(bgMusic);
        }
        movementInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        mouseInput = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        playerMovement();
        cameraMovement();
        footstepAudio();
    }
    void footstepAudio()
    {
        if (Input.GetKey("a") && (Time.time >= timeBetween) && (jumpCheck==0))
        {
            source.PlayOneShot(footstepsSFX);
            timeBetween = Time.time + 1f/walkRate;
        }
        if (Input.GetKey("d") && (Time.time >= timeBetween) && (jumpCheck==0))
        {
            source.PlayOneShot(footstepsSFX);
            timeBetween = Time.time + 1f/walkRate;
        }
        if (Input.GetKey("w") && (Time.time >= timeBetween) && (jumpCheck==0))
        {
            source.PlayOneShot(footstepsSFX);
            timeBetween = Time.time + 1f/walkRate;
        }
        if (Input.GetKey("s") && (Time.time >= timeBetween) && (jumpCheck==0))
        {
            source.PlayOneShot(footstepsSFX);
            timeBetween = Time.time + 1f/walkRate;
        }
    }
    void playerMovement()
    {
        Vector3 movementVector = transform.TransformDirection(movementInput)*speed;
        rb.velocity = new Vector3(movementVector.x,rb.velocity.y,movementVector.z);
        if (Input.GetKey("space") && (flag==0))
        {
            flag = 1;
            jumpCheck = 1;
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        }
    }
    void cameraMovement()
    {
        transform.Rotate(0f,mouseInput.x*Sensitivity,0f);
        x_rot-=mouseInput.y*Sensitivity;
        camera.transform.localRotation = Quaternion.Euler(x_rot,0f,0f);
    }
}