using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        int height = 50; //z 

        [SerializeField]
        int width = 50; //x

        [SerializeField]
        float cellSize;

        Grid grid;
        GridView gridView;

        void Awake()
        {
            grid = new Grid(20, 20, 10f);
            TryGetComponent<GridView>(out gridView);
        }

        void Start()
        {
            if (grid.TryGetAllPos(out Dictionary<Vector2, Vector3> posDic))
            {
                gridView?.InitGridView(posDic,this);
            }
        }

        public void ChangeNodeState(Vector3 pos, NodeState state)
        {
            if (grid.TryGetXZ(pos, out int x, out int z))
            {
                Vector2 nodeXZ = new Vector2(x, z);
                grid.SetValue(nodeXZ, 1);
                gridView.ChangeNodeState(nodeXZ, 1);
            }
        }

        public NodeState GetStateByInt(int value)
        {
            return value switch
            {
                0 => NodeState.Default,
                1 => NodeState.OnSelect,
                2 => NodeState.Block,
                _ => NodeState.Default,
            };
        }

        public enum NodeState
        {
            Default, //0
            OnSelect, //1
            Block, //2
        }
    }
}
