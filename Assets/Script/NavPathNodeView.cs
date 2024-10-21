using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavPathNodeView : MonoBehaviour
{
    [SerializeField]
    Text gCost;
    [SerializeField]
    Text hCost;
    [SerializeField]
    Text fCost;

    private void Awake()
    {
        if (gCost == null || hCost == null || fCost == null)
        {
            Debug.LogError($"Text Component Missing On {name}");
        }
    }

    public void SetValue(float g, float h, float f)
    {
        gCost.text = g.ToString();
        hCost.text = h.ToString();
        fCost.text = f.ToString();
    }

}
