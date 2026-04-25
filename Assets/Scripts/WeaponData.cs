using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "GoldenWest/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int weaponPower;
    public float weaponRange;
    public float price;
}