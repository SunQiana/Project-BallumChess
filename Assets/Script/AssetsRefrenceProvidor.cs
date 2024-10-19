using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[CreateAssetMenu(fileName = "AssetsRefrenceProvidor", menuName = "AssetsRefrenceProvidor", order = 1)]
public class AssetsRefrenceProvidor : ScriptableObject
{
    public GameObject nodeView;

    public bool TryGetAsset(string assetName, out object asset)
    {
        asset = null;

        FieldInfo fieldInfo = GetType().GetField(assetName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (fieldInfo != null)
        {
            asset = fieldInfo.GetValue(this);

            if (asset != null)
                return true;
        }

        PropertyInfo propertyInfo = GetType().GetProperty(assetName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (propertyInfo != null)
        {
            asset = propertyInfo.GetValue(this);
            if (asset != null)
                return true;
        }

        return false;
    }
}
