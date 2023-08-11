using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "LevelCreation")]
public class LevelCreation : ScriptableObject
{
    public List<LevelData> levels = new();
}

[System.Serializable]
public class LevelData
{
    public string levelName;
    public int levelNum;
    public Vector2 playerPos;
    public List<int> linesActiveInLevel;
    public List<PencilDirection> levelDirection;
}
