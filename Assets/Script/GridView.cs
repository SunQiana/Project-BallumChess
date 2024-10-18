using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        Dictionary<Vector2, GameObject> nodeDic = new();

        public void InitGridView(int widthX, int heightZ, float cellSize, Vector3 origPos = new())
        {
            GameObject gridBase = new("GridBase");

            for (int x = 0; x < widthX; x++)
            {
                for (int z = 0; z < heightZ; z++)
                {
                    Vector3 spawnPos = new Vector3(x * cellSize + origPos.x, 0, z * cellSize + origPos.z);
                    Vector2 indexKey = new Vector2(x, z);

                    SpawnNode(spawnPos, indexKey);
                }
            }

            void SpawnNode(Vector3 pos, Vector2 indexKey)
            {
                GameObject go = Instantiate(nodeView, pos, Quaternion.identity);
                go.transform.SetParent(gridBase.transform);
                go.name = "Node" + indexKey;

                nodeDic.Add(indexKey, go);
            }
        }

        public void ChangeNodeState(Vector2 nodeKey, GridManager.NodeState state)
        {
            if (IfKeyInvalid(nodeKey, out GameObject node))
                return;

            if (!node.TryGetComponent<Renderer>(out Renderer renderer) || renderer.material == null)
            {
                Debug.LogError($"Can't Find Material On Node{nodeKey}");
                return;
            }

            Material mat = renderer.material;

            switch (state)
            {
                case GridManager.NodeState.Default:
                    SetDefaultColor();
                    break;

                case GridManager.NodeState.OnSelect:
                    SetOnSelectColor();
                    break;

                case GridManager.NodeState.Block:
                    SetBlockColor();
                    break;
            }

            void SetDefaultColor()
            {
                if (mat.HasProperty("_BaseMap"))
                {
                    mat.SetColor("_BaseMap", defaultColor);
                }
            }

            void SetOnSelectColor()
            {

                if (mat.HasProperty("_BaseMap"))
                {
                    mat.SetColor("_BaseColor", onSelectColor);
                }
            }

            void SetBlockColor()
            {
                if (mat.HasProperty("_BaseMap"))
                {
                    mat.SetColor("_BaseMap", blockColor);
                }
            }
        }


        bool IfKeyInvalid(Vector2 nodeKey, out GameObject node)
        {
            if (nodeDic.ContainsKey(nodeKey))
            {
                node = nodeDic[nodeKey];
                return false;
            }

            Debug.LogError("Invalid Node Key" + nodeKey);
            node = null;
            return true;
        }

    }
}
