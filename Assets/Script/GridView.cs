using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField]
        GameObject nodeView;

        [SerializeField]
        [ColorUsage(false, true)]
        Color onSelectColor = Color.red;

        [SerializeField]
        [ColorUsage(false, true)]
        Color defaultColor = Color.white;

        [SerializeField]
        [ColorUsage(false, true)]
        Color blockColor = Color.blue;

        GridManager gridManager;
        GameObject gridBase;
        Dictionary<Vector2, GameObject> nodes = new();

        public void InitGridView(Dictionary<Vector2, Vector3> posDic, GridManager gridManager)
        {
            this.gridManager = gridManager;
            gridBase = new("GridBase");

            foreach (var item in posDic)
                SpawnNode(item.Key, item.Value);
        }

        void SpawnNode(Vector2 XZ, Vector3 pos)
        {
            GameObject go = Instantiate(nodeView, pos, Quaternion.identity);
            go.transform.SetParent(gridBase.transform);
            go.name = "Node" + XZ;

            nodes.Add(XZ, go);
        }

        public void ChangeNodeState(Vector2 nodeKey, int state)
        {
            if (IfKeyInvalid(nodeKey, out GameObject node))
                return;

            var renderer = node?.GetComponentInChildren<Renderer>();

            if (renderer == null || renderer.material == null)
            {
                Debug.LogError($"Can't Find Material On Node{nodeKey}");
                return;
            }

            Material mat = renderer.material;

            if (!mat.HasProperty("_BaseMap"))
            {
                Debug.LogError($"Can't Find Material Property On Node{nodeKey}");
                return;
            }

            SetColor(state, mat);
        }

        void SetColor(int value, Material mat)
        {
            GridManager.NodeState state = gridManager.GetStateByInt(value);

            switch (state)
            {
                case GridManager.NodeState.Default:
                    {
                        mat.SetColor("_BaseMap", defaultColor);
                        break;
                    }

                case GridManager.NodeState.OnSelect:
                    {
                        mat.SetColor("_BaseColor", onSelectColor);
                        break;
                    }

                case GridManager.NodeState.Block:
                    {
                        mat.SetColor("_BaseMap", blockColor);
                        break;
                    }
            }
        }


        bool IfKeyInvalid(Vector2 nodeKey, out GameObject node)
        {
            if (nodes.ContainsKey(nodeKey))
            {
                node = nodes[nodeKey];
                return false;
            }

            Debug.LogError("Invalid Node Key" + nodeKey);
            node = null;
            return true;
        }

    }
}
