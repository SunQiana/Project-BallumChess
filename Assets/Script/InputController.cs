using System;
using UnityEngine;


public class InputController
{
    Action OnKeyDownW;
    Action OnKeyDownS;
    Action OnKeyDownD;
    Action OnKeyDownA;
    Action OnKeyDownQ;
    Action OnKeyDownE;
    Action OnMouseWheelUp;
    Action OnMouseWheelDown;
    Action OnMouseLeftBtnDown;
    Action OnMouseRightBtnDown;

    public void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
            OnKeyDownW?.Invoke();

        if (Input.GetKey(KeyCode.S))
            OnKeyDownS?.Invoke();

        if (Input.GetKey(KeyCode.A))
            OnKeyDownA?.Invoke();

        if (Input.GetKey(KeyCode.D))
            OnKeyDownD?.Invoke();

        if (Input.GetKey(KeyCode.Q))
            OnKeyDownQ?.Invoke();

        if (Input.GetKey(KeyCode.E))
            OnKeyDownE?.Invoke();

        if (Input.mouseScrollDelta.y >= 0.1f)
            OnMouseWheelDown?.Invoke();

        if (Input.mouseScrollDelta.y <= -0.1f)
            OnMouseWheelUp?.Invoke();

        if (Input.GetMouseButtonDown(0))
            OnMouseLeftBtnDown?.Invoke();

        if (Input.GetMouseButtonDown(1))
            OnMouseLeftBtnDown?.Invoke();
    }

    public void RigsterInputAction(Action action, KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.W:
                OnKeyDownW += action;
                break;
            case KeyCode.S:
                OnKeyDownS += action;
                break;
            case KeyCode.A:
                OnKeyDownA += action;
                break;
            case KeyCode.D:
                OnKeyDownD += action;
                break;
            case KeyCode.Q:
                OnKeyDownQ += action;
                break;
            case KeyCode.E:
                OnKeyDownE += action;
                break;
        }
    }

    public void RigsterInputAction(Action action, MouseInputType type)
    {
        if (type == MouseInputType.WheelUp)
            OnMouseWheelUp += action;

        if (type == MouseInputType.WheelDown)
            OnMouseWheelDown += action;

        if (type == MouseInputType.LeftButton)
            OnMouseLeftBtnDown += action;

        if (type == MouseInputType.RightButton)
            OnMouseRightBtnDown += action;
    }

    public bool GetMouseRay(out Ray ray)
    {
        if (Camera.main == null)
        {
            ray = new Ray();
            return false;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return true;
    }


    public enum MouseInputType
    {
        WheelUp,
        WheelDown,
        RightButton,
        LeftButton,
    }


}
