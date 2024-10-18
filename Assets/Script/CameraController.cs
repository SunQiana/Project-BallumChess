using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController
{
    float moveSpeed = 50f;
    float rotationSpeed = 100f;
    float verticalSpeed = 50f;

    Camera camera;


    public CameraController(Camera Camera)
    {
        this.camera = Camera;
    }

    public void MoveUp()
    {
        camera.transform.Translate(0, verticalSpeed * Time.deltaTime, 0, Space.World);
    }

    public void MoveDown()
    {
        camera.transform.Translate(0, -verticalSpeed * Time.deltaTime, 0, Space.World);
    }

    public void MoveFoward()
    {
        camera.transform.Translate(0, 0, moveSpeed * Time.deltaTime, Space.World);
    }

    public void MoveBackward()
    {
        camera.transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
    }

    public void MoveLeft()
    {
        camera.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0, Space.World);
    }

    public void MoveRight()
    {
        camera.transform.Translate(moveSpeed * Time.deltaTime, 0, 0, Space.World);
    }

    public void MoveRotateLeft()
    {
        camera.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
    public void MoveRotateRight()
    {

        camera.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);

    }
}
