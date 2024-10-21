using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Grid
{
    public class GridView
    {
        GameObject nodeView;
        GameObject gridBase;
        Dictionary<Vector2, GameObject> nodes = new();

        public GridView(Dictionary<Vector2, Vector3> posDic, string nodeAssetName)
        {
            gridBase = new("GridBase");
            nodeView = GameManager.Instance.GetAssetByName<GameObject>(nodeAssetName);

            Debug.Log(nodeView);

            foreach (var item in posDic)
                SpawnNodeView(item.Key, item.Value);
        }

        void SpawnNodeView(Vector2 XZ, Vector3 pos)
        {
            GameManager.Instance.TryInstantiateObject(nodeView, out Object result, pos);
            GameObject go = result as GameObject;
            if (go == null)
            {
                Debug.LogError("Faild To InstantiateObject");
                return;
            }

            go.transform.SetParent(gridBase.transform);
            go.name = "Node" + XZ;

            nodes.Add(XZ, go);
        }

        public void ChangeNodeColor(Vector2 nodeKey, Color color)
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

            if (color == null)
            {
                Debug.LogError($"No Color Assigned When Trying To Change Color On Node{nodeKey}");
                return;
            }

            mat.SetColor("_BaseMap", color);
        }

        public void ChangeNodeText(string textName, string text)
        {
            var texts = nodeView?.GetComponents<Text>();

            foreach (var item in texts)
            {
                if (item.name == textName)
                    item.text = text;
                    
                break;
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
