using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public float speed;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out RaycastHit hit,100f))
        {
            transform.position = Vector3.Lerp(transform.position,hit.point, speed*Time.deltaTime);
        }
    }
}
