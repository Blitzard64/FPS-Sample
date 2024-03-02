using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
	public Rigidbody rb;
	public float fwd_force = 500f;
	public float side_force = 500f;
	void FixedUpdate()
	{
		rb.AddForce(0,0,fwd_force*Time.deltaTime);
		if (Input.GetKey("a"))
		{
			rb.AddForce(-side_force*Time.deltaTime,0,0,ForceMode.VelocityChange);
		}
		if (Input.GetKey("d"))
		{
			rb.AddForce(side_force*Time.deltaTime,0,0,ForceMode.VelocityChange);
		}
	}
}