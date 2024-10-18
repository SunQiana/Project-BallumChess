using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GridManager gridManager;

    [SerializeField]
    Camera camera;

    CameraController cameraController;
    InputController input;

    void Awake()
    {
        cameraController = new(camera);

        input = new();
        RigsterInputAction();

    }

    void Update()
    {
        input.GetInput();
    }

    void RigsterInputAction()
    {
        input.RigsterInputAction(cameraController.MoveFoward, KeyCode.W);
        input.RigsterInputAction(cameraController.MoveBackward, KeyCode.S);
        input.RigsterInputAction(cameraController.MoveRight, KeyCode.D);
        input.RigsterInputAction(cameraController.MoveLeft, KeyCode.A);
        input.RigsterInputAction(cameraController.MoveRotateRight, KeyCode.Q);
        input.RigsterInputAction(cameraController.MoveRotateLeft, KeyCode.E);
        input.RigsterInputAction(cameraController.MoveUp, InputController.MouseInputType.WheelUp);
        input.RigsterInputAction(cameraController.MoveDown, InputController.MouseInputType.WheelDown);
        input.RigsterInputAction(OnGridSelection, InputController.MouseInputType.LeftButton);
    }

    void OnGridSelection()
    {
        if (input.GetMouseRay(out Ray ray) == false)
            return;

        Physics.Raycast(ray, out RaycastHit info);

        if(info.collider == null)
            return;

        if (info.collider.tag == "Grid")
            gridManager.ChangeNodeState(info.point, GridManager.NodeState.OnSelect);
    }



}
