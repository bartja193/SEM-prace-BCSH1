using UnityEngine;

public class PlotManager : MonoBehaviour
{
    public static PlotManager Instance;

    public GameObject[] plotBlockers; 

    private int[] plotPrices = { 1000, 2000, 4000, 8000 };
    private int plotsBought = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public int GetNextPlotPrice()
    {
        if (plotsBought >= plotPrices.Length) return -1;
        return plotPrices[plotsBought];
    }

    public bool CanBuyMore()
    {
        return plotsBought < plotPrices.Length;
    }

    public bool BuyNextPlot()
    {
        if (!CanBuyMore()) return false;

        int price = plotPrices[plotsBought];

        if (InventoryManager.Instance.money < price)
        {
            FloatingTextManager.Instance.Show("Nemáš dost peněz!", Color.red);
            return false;
        }

        InventoryManager.Instance.SpendMoney(price);
        plotBlockers[plotsBought].SetActive(false);
        plotsBought++;
        return true;
    }
}