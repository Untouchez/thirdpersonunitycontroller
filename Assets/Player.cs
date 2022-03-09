using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Animator rigAnim;
    public Rigidbody rb;
    public float acceleration;
    public float decceleration;

    public Vector3 rawInput;
    public Vector3 calculatedInput;

    public float speed;

    public bool canRotate;
    public float turnSpeed;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        HandleInputs();
        anim.SetFloat("Vertical", calculatedInput.z);
        anim.SetFloat("Horizontal", calculatedInput.x);
            
        rigAnim.SetBool("gunup", rawInput.magnitude == 0);
        Vector3 moveVector = (calculatedInput * speed);
        rb.velocity = transform.TransformDirection(moveVector);

    }

    void Update()
    {
        HandleRotation();
    }

    //CALCULATES AND SETS RAW INPUT AND CALCULATED INPUT
    //CHANGES ANIMATOR TO PLAY CORRECT ANIMATION
    void HandleInputs()
    {
        rawInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (rawInput.magnitude != 0)
            calculatedInput = new Vector3(Mathf.MoveTowards(calculatedInput.x, rawInput.x, acceleration * Time.deltaTime), 0, Mathf.MoveTowards(calculatedInput.z, rawInput.z, acceleration * Time.deltaTime));
        else
            calculatedInput = new Vector3(Mathf.MoveTowards(calculatedInput.x, 0, acceleration * Time.deltaTime), 0, Mathf.MoveTowards(calculatedInput.z, 0, decceleration * Time.deltaTime));

       calculatedInput = Vector3.ClampMagnitude(calculatedInput, 1.5f);
    }

    public float angleDiff;
    void HandleRotation()
    {
        if (!canRotate)
            return;
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        float playerheading = (int)(transform.eulerAngles.y);
        Vector3 cameraDirForward = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized;
        Debug.DrawRay(transform.position, transform.forward.normalized * 5f, Color.green);
        Debug.DrawRay(transform.position, new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized * 5f, Color.red);
        angleDiff = Vector3.Angle(transform.forward.normalized, cameraDirForward);
        if (rawInput.magnitude != 0 || angleDiff >= 25)
        {
            anim.SetBool("turn", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
        }else
            anim.SetBool("turn", false);
    }
}
