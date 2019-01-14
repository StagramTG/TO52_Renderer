using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float distance = 20.0f;
    public float xSpeed = 1f;
    public float ySpeed = 1f;
    private float x = 0.0f;
    private float y = 0.0f;
    private UnityEngine.Vector3 distanceVector;

    void Start()
    {
        distanceVector = new UnityEngine.Vector3(0.0f, 0.0f, -distance);
        Vector2 angles = this.transform.localEulerAngles;
        x = angles.x;
        y = angles.y;
        this.Rotate(x, y);
    }
    void LateUpdate()
    {
        if (target)
        {
            this.RotateControls();
        }
    }
    void RotateControls()
    {    
        if (Input.GetButton("Fire1") || Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            distance += Input.GetAxis("Mouse ScrollWheel") * 4f;
            distanceVector = new UnityEngine.Vector3(0.0f, 0.0f, -distance);

            x += Input.GetAxis("Mouse X") * xSpeed;
            y += -Input.GetAxis("Mouse Y") * ySpeed;
            this.Rotate(x, y);
        }

    }
    void Rotate(float x, float y)
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
        UnityEngine.Vector3 position = rotation * distanceVector + target.position;
        transform.rotation = rotation;
        transform.position = position;
    }
}