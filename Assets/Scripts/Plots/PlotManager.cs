using UnityEngine;

public class PlotManager : MonoBehaviour
{
    public static PlotManager Instance;

    public GameObject[] plotBlockers;
    public GameObject[] minerPrefabs; 
    public Transform[] minerSpawnPoints;

    private int[] plotPrices = { 500, 750, 1000, 1500 };
    private int[] minerPrices = { 500, 750, 1000, 1500 };

    private int plotsBought = 0;
    private bool[] minersBought = new bool[4];
    public Transform[] minePoints;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name != "Level2") return;

        plotBlockers = new GameObject[4];
        minePoints = new Transform[4];
        minerSpawnPoints = new Transform[4];

        for (int i = 0; i < 4; i++)
        {
            plotBlockers[i] = GameObject.Find("PlotBlocker" + (i + 1));
            GameObject mine = GameObject.Find("MinePoint" + (i + 1));
            GameObject spawn = GameObject.Find("Spawnpoint" + (i + 1));

            if (mine != null) minePoints[i] = mine.transform;
            if (spawn != null) minerSpawnPoints[i] = spawn.transform;

            if (i < plotsBought && plotBlockers[i] != null)
                plotBlockers[i].SetActive(false);
        }
    }

    // PLOTY
    public int GetNextPlotPrice()
    {
        if (plotsBought >= plotPrices.Length) return -1;
        return plotPrices[plotsBought];
    }

    public bool CanBuyPlot()
    {
        return plotsBought < plotPrices.Length;
    }

    public bool BuyNextPlot()
    {
        if (!CanBuyPlot()) return false;

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

    // TĚŽAŘI
    public bool CanBuyMiner()
    {
        for (int i = 0; i < plotsBought; i++)
        {
            if (!minersBought[i]) return true;
        }
        return false;
    }

    public int GetNextMinerIndex()
    {
        for (int i = 0; i < plotsBought; i++)
        {
            if (!minersBought[i]) return i;
        }
        return -1;
    }

    public int GetNextMinerPrice()
    {
        int index = GetNextMinerIndex();
        if (index == -1) return -1;
        return minerPrices[index];
    }

    public bool BuyNextMiner()
    {
        int index = GetNextMinerIndex();
        if (index == -1) return false;

        int price = minerPrices[index];

        if (InventoryManager.Instance.money < price)
        {
            FloatingTextManager.Instance.Show("Nemáš dost peněz!", Color.red);
            return false;
        }

        InventoryManager.Instance.SpendMoney(price);
        minersBought[index] = true;
        PassiveIncome.Instance.AddWorker(1f);

        GameObject miner = Instantiate(minerPrefabs[index], minerSpawnPoints[index].position, Quaternion.identity);
        MinerAI ai = miner.GetComponent<MinerAI>();
        ai.minePoint = minePoints[index];
        ai.depositPoint = minerSpawnPoints[index];
        return true;
    }
}