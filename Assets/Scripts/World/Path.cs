using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[CreateAssetMenu(fileName = "newPath", menuName = GameConstants.AssetMenuBasePath + "Path")]
public class Path : ScriptableObject
{
    [SerializeField]
    private List<Vector2> _points;

    public Vector2 GetFirstPoint()
    {
        return _points[0];
    }

    public Vector2? GetNextPoint(int currPoint)
    {
        if(currPoint < _points.Count - 1) //If there are still points to be given
        {
            return _points[currPoint + 1];
        }
        else // If there are no further points
        {
            return null;
        }
    }
}
