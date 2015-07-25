using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {
    public float desiredAngle = 90f;
    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        //target.transform.Rotate(0, horizontal, 0);

        //float desiredAngle = target.transform.eulerAngles.y;
        
        Quaternion rotationY = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        Quaternion rotationX = Quaternion.Euler(transform.eulerAngles.x, 0, 0);
        transform.position = target.transform.position - (rotationY * offset);
        
        transform.RotateAround(target.transform.position, Vector3.up, horizontal);
        transform.RotateAround(target.transform.position, Vector3.right, vertical);

        transform.LookAt(target.transform);
    }
}

