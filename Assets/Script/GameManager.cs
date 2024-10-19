using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;
using UnityEngine.EventSystems;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void InitGameManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    CameraController cameraController;
    InputController input;
    GridManager grid;
    AssetsRefrenceProvidor assets;

    void Awake()
    {
        assets = (AssetsRefrenceProvidor)AssetDatabase.LoadAssetAtPath("Assets/Art/AssetsRefrenceProvidor.asset", typeof(AssetsRefrenceProvidor));

        if (assets == null)
        {
            Debug.LogError("Can't Load AssetRefrenceProvidor From Data Base, GameManager Can't Be Initialize");
            return;
        }

        InitGameManager();
        cameraController = new();
        input = new();
        grid = new(20, 30, 10);
    }

    void Start()
    {
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

        if (info.collider == null)
            return;

        if (info.collider.tag == "Grid")
            grid.ChangeNodeState(info.point, GridManager.NodeState.OnSelect);
    }

    public bool TryInstantiateObject(Object o, out Object result, Vector3 pos = new(), Quaternion rot = new())
    {
        if (o != null)
        {
            result = Instantiate(o, pos, rot);
            return true;
        }

        Debug.LogError("Object Is Null");
        result = null;
        return false;
    }

    public bool TrySetMainCamera(CameraController controller)
    {
        if (controller.camera != null)
        {
            Debug.LogError("Another Camera Is Already Exist");
            return false;
        }

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (mainCamera == null)
        {
            Debug.Log("No Main Camera found: " + mainCamera.name);
            return false;
        }

        if (mainCamera.TryGetComponent<Camera>(out Camera cam))
        {
            controller.camera = cam;
            return true;
        }


        Debug.LogError("Faild To Set MainCamera Up");
        return false;
    }

    public T GetAssetByName<T>(string assetName) where T : class
    {
        if (assets != null && assets.TryGetAsset(assetName, out object asset))
        {
            return asset as T;
        }

        Debug.LogError($"Asset with name '{assetName}' not found or is not of type {typeof(T)}.");
        return null;
    }
}
