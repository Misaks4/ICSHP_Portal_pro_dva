using System;
using UnityEngine;

[Serializable]
public class SerializableVector3
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        return new Vector3(rValue.X, rValue.Y, rValue.Z);
    }

    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3
        {
            X = rValue.x,
            Y = rValue.y,
            Z = rValue.z
        };
    }
}