using UnityEngine;

public class WeaponShopManager : MonoBehaviour 
{
    public static WeaponShopManager Instance;
    public WeaponData[] avaibleWeapons;
    private WeaponData currentWeapon;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (avaibleWeapons.Length > 0)
            currentWeapon = avaibleWeapons[0];
    }

    public int GetCurrentDMG()
    {
        if (currentWeapon == null) return 1;
        return currentWeapon.weaponPower;
    }

    public float GetCurrentRange()
    {
        if (currentWeapon == null) return 1.5f;
        return currentWeapon.weaponRange;
    }
    public float GetCurrentPrice(int index)
    {
        if (avaibleWeapons[index] == null) return 0;
        else
        {
           return avaibleWeapons[index].price;
        }
        

    }

    public void BuyWeapon(int index)
    {
        if (index >= avaibleWeapons.Length) return;

        WeaponData weapon = avaibleWeapons[index];

        if (InventoryManager.Instance.money < weapon.price)
        {
            Debug.Log("Nemáš dost peněz! Potřebuješ $" + weapon.price);
            return;
        }

        InventoryManager.Instance.SpendMoney(weapon.price);
        currentWeapon = weapon;
        Debug.Log("Koupeno: " + weapon.weaponName + " | síla: " + weapon.weaponPower + " | dosah: " + weapon.weaponRange);
    }
}
