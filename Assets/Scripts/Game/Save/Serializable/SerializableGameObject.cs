using System;

[Serializable]
public class SerializableGameObject
{
    public SerializableVector3 Position { get; set; }
    public SerializableVector3 EulerAngles { get; set; }
    public SerializableVector3 LocalScale { get; set; }
}