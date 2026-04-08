using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "GoldenWest/Tool")]
public class ToolData : ScriptableObject
{
    public string toolName;
    public float miningSpeed;
    public float price;
}