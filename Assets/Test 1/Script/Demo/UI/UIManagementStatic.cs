using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static - class does not need to be instantiated for use
public static class UIManagementStatic
{
    // create copy of prefab, place under parent
    public static GameObject AddChild(GameObject parent, GameObject prefab)
    {
        if (parent == null)
            throw new ArgumentNullException("parent");
        if (prefab == null)
            throw new ArgumentNullException("prefab");

        var go = GameObject.Instantiate(prefab);
        go.layer = parent.layer;

        var tc = go.GetComponent<RectTransform>();
        var tp = parent.GetComponent<RectTransform>();
        tc.SetParent(tp, false);

        return go;
    }
}
