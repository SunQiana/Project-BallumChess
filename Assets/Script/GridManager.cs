using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Grid
{
    public class GridManager
    {
        Grid grid;
        GridView view;

        public GridManager(int width, int height, float cellSize)
        {
            grid = new Grid(width, height, cellSize);
            grid.TryGetAllPos(out Dictionary<Vector2, Vector3> posDic);

            view = new GridView(posDic, this);
        }

        public void ChangeNodeState(Vector3 pos, NodeState state)
        {
            if (grid.TryGetXZ(pos, out int x, out int z))
            {
                Vector2 nodeXZ = new Vector2(x, z);
                grid.SetValue(nodeXZ, 1);
                view.ChangeNodeViewByState(nodeXZ, 1);
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
