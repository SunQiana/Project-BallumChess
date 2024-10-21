using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Grid
{
    public class GridManager
    {
        Grid<NavNodeState> navGrid;
        GridView navGridView;

        public GridManager(int width, int height, float cellSize)
        {
            navGrid = new Grid<NavNodeState>(width, height, cellSize);
            navGrid.TryGetAllPos(out Dictionary<Vector2, Vector3> posDic);

            navGridView = new GridView(posDic, "NavNodeView");
        }

        public void NavChangeNodeState(Vector3 pos, NavNodeState state, Color color)
        {
            if (navGrid.TryGetXZ(pos, out int x, out int z))
            {
                Vector2 nodeXZ = new Vector2(x, z);
                navGrid.SetValue(nodeXZ, state);
                navGridView.ChangeNodeColor(nodeXZ, color);
            }
        }

        public void NavChangeNodeValue(Vector3 pos, NavNodeState state, string valueName, float value)
        {
            if (navGrid.TryGetXZ(pos, out int x, out int z))
            {
                Vector2 nodeXZ = new Vector2(x, z);
                navGrid.SetValue(nodeXZ, state);
                navGridView.ChangeNodeText(valueName, value.ToString());
            }
        }
        
        public enum NavNodeState
        {
            Default, //0
            OnSelect, //1
            Block, //2
        }
    }
}
