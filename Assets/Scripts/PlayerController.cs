using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] Camera _camera;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position += Speed * Time.deltaTime * new Vector3(horizontal, 0, vertical);
        CameraRotationAroundPlayer();
    }
    void CameraRotationAroundPlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up, mouseX);
        _camera.transform.RotateAround(transform.position, Vector3.right, -mouseY);
    }
}
