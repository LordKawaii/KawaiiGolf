using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {
    public float desiredAngle = 90f;
    public float rotateSpeed = 5;
    
    //Vector3 offset;
    private bool hasTarget = false;
    private bool hasSetup = false;
    private GameObject target;


    void Start()
    {
        
    }

    void LateUpdate()
    {

        if (hasTarget)
        { 
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            target.transform.Rotate(0, horizontal, 0);

            //float desiredAngle = target.transform.eulerAngles.y;
        
            //Quaternion rotationY = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            //Quaternion rotationX = Quaternion.Euler(transform.eulerAngles.x, 0, 0);
            //transform.position = target.transform.position - (rotationY * offset);
        
            transform.RotateAround(target.transform.position, Vector3.up, horizontal);
            if ((transform.position.y >= 1 || vertical < 0) && (transform.position.y <= 6 || vertical > 0))
            { 
                transform.RotateAround(target.transform.position, Vector3.right, -vertical);
            }
                
            transform.LookAt(target.transform);

            if (!hasSetup)
            {
                transform.position = new Vector3(transform.position.x + 5, transform.position.y + 5);
                //offset = target.transform.position - transform.position;
                hasSetup = true;
            }
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        hasTarget = true;
    }

}

