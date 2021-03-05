using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float SpeedRun;
    public float JumpForce;
    public float Sensifity;
    public GameObject Camera;
    public bool IsGrounded;



    float VerticalLookRotation;
    float SpeedBank;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SpeedBank = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CameraRotate();
    }

    void Move()
    {

        //Movement
        float Horiz = Input.GetAxisRaw("Horizontal");
        float Vert = Input.GetAxisRaw("Vertical");

        Vector3 Pos = new Vector3(Horiz, Vert, transform.position.z).normalized;

        transform.Translate(Vector3.forward * Vert * Speed * Time.deltaTime);
        transform.Translate(Vector3.right * Horiz * Speed * Time.deltaTime);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Speed = SpeedRun;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = SpeedBank;
        }

        //Jump
        if(Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            rb.velocity = new Vector3(0, JumpForce, 0);
        }
    }

    void CameraRotate()
    {
        float PosX = Input.GetAxisRaw("Mouse X") * Sensifity;
        VerticalLookRotation += Input.GetAxisRaw("Mouse Y") * Sensifity;

        transform.Rotate(Vector3.up * PosX);


        VerticalLookRotation = Mathf.Clamp(VerticalLookRotation, -70f, 90f);
        Camera.transform.localEulerAngles = Vector3.left * VerticalLookRotation;
    }

}
