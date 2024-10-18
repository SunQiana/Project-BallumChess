using System.Collections;
using System.Collections.Generic;
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
            grid = new Grid(20, 30, 1f);
            TryGetComponent<GridView>(out gridView);
        }

        void Start()
        {
            gridView?.InitGridView(width, height, cellSize);
            gridView.ChangeNodeState(new Vector2(20,14), NodeState.OnSelect);
            gridView.ChangeNodeState(new Vector2(121,12), NodeState.OnSelect);
            gridView.ChangeNodeState(new Vector2(12,12), NodeState.Block);
            gridView.ChangeNodeState(new Vector2(30,14), NodeState.OnSelect);
            gridView.ChangeNodeState(new Vector2(15,26), NodeState.OnSelect);
            gridView.ChangeNodeState(new Vector2(15,26), NodeState.Block);
        }

        public enum NodeState
        {
            Default,
            OnSelect,
            Block,
        }
    }
}
