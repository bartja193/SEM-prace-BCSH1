using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "GoldenWest/Tool")]
public class ToolData : ScriptableObject
{
    public string toolName;
    public float miningPower;
    public float miningSpeed;
    public float price;
}