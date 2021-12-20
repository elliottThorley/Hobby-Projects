using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicalSpace : MonoBehaviour
{
    public float torque = 550;
    public float thrust = 10000f;
    private float glide;
    private Rigidbody rb;
    public bool throttle;
    public bool reverse;
    public int lastUsed;
    private bool landing = false;
    private bool reached = false;
    private bool reached1 = false;

    void Start()
    {
        glide = 0f;
        rb = GetComponent<Rigidbody>();
    }
    void Update() {
        if (Input.GetKeyDown("g")){landing = true;}
    }
    void FixedUpdate()
    {
        if (landing == true)
        {
            if (transform.eulerAngles.z > -30 && transform.eulerAngles.z < 40 || reached1 == true)
            {
                reached1 = true;
                if (transform.eulerAngles.x > 60 && transform.eulerAngles.x < 90)
                {
                    reached = true;
                }
                else if (reached == false)
                {
                    rb.AddRelativeTorque(Vector3.right * torque * -3);
                }
                if (reached == true)
                {
                    rb.AddRelativeForce(Vector3.forward * thrust * 3);
                    glide = thrust;
                    lastUsed = 0;
                }
            }
            else
            {
                rb.AddRelativeTorque(Vector3.back * torque * 2f);
            }
        }
        else {
            float roll = Input.GetAxisRaw("Horizontal");
            float pitch = Input.GetAxis("Vertical");
            throttle = Input.GetKey("space");
            reverse = Input.GetKey(KeyCode.LeftControl);
            bool left = Input.GetKey("a");
            bool right = Input.GetKey("d");

            rb.AddRelativeTorque(Vector3.back * torque * roll);
            rb.AddRelativeTorque(Vector3.right * torque * pitch);

           

            if (left == true) {
                rb.AddRelativeTorque(Vector3.down * torque);
            }
            if (right == true)
            {
                rb.AddRelativeTorque(Vector3.up * torque);
            }

            if (throttle == true && reverse == false)
            {
                rb.AddRelativeForce(Vector3.forward * thrust);
                glide = thrust;
                lastUsed = 0;
            }
            else if (throttle == false && reverse == true)
            {
                rb.AddRelativeForce(Vector3.back * thrust);
                glide = thrust;
                lastUsed = 1;
            }
            else
            {
                if (lastUsed == 0)
                    rb.AddRelativeForce(Vector3.forward * glide);
                if (lastUsed == 1)
                    rb.AddRelativeForce(Vector3.back * glide);
                glide *= 0.9f;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
