using UnityEngine;

public class Line
{
    EOrientation _orientation;
    public EOrientation Orientation { get => _orientation; set => _orientation = value; }
    
    Vector2Int _coordinates;
    public Vector2Int Coordinates { get => _coordinates; set => _coordinates = value; }

    public Line(EOrientation orientation, Vector2Int coordinates)
    {
        _orientation = orientation;
        _coordinates = coordinates;
    }
}

public enum EOrientation
{
    Horizontal = 0,
    Vertical = 1
}