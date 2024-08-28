using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField]
    private List<Path> _paths;

    public Path GetFirstPath()
    {
        return _paths[0];
    }

    public Path GetPath(int pathNum)
    {
        return _paths[pathNum];
    }

    public Path GetRandomPath()
    {
        int index = Random.Range(0, _paths.Count);
        return _paths[index];
    }
}
