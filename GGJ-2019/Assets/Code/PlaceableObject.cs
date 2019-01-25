using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public List<BoxPos> BoundingBoxes;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var trs = transform.localToWorldMatrix;
        foreach (var b in BoundingBoxes)
        {
            Gizmos.matrix = trs * Matrix4x4.TRS(b.Position, Quaternion.AngleAxis(b.Rotation, Vector3.up), Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, b.Extents);
        }
    }
}


[Serializable]
public struct BoxPos
{
    public Vector3 Position;
    public Vector3 Extents;
    public float Rotation;
}