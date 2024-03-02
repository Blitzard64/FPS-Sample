using UnityEngine;
public class BulletMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player;
    public Vector3 offset;
    void FixedUpdate()
    {
        offset = new Vector3 (0f,0.5f,1f);
        transform.position = player.position + offset;
        if (Input.GetKey("space"))
        {
            rb.AddForce(0,0,10000);
        }
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Ouch");
    }
}