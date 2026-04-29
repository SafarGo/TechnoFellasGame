using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DontDestroyController : MonoBehaviour
{
    public GameObject Card;
    public GameObject Money;
    public Transform PointToSpawn;
    public Transform Inventory;
    public TMP_Text Tasks;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLoaded;
    }

    void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnpoint = GameObject.Find("SpawnPoint");
        transform.position = spawnpoint.transform.position;

        if (scene.name == "BusScene")
        {
            Instantiate(Card, PointToSpawn.transform.position, PointToSpawn.rotation);
            Tasks.text = "1. Сядь в нужный автобус\n" + "2. Возьми карту из инвентаря\n" + "3. Приложи карту к валидатору";
        }

        if (scene.name == "ATMScene")
        {
            Instantiate(Card, PointToSpawn.transform.position, PointToSpawn.rotation);
            Tasks.text = "1. Возьми карту\n" + "2. ВВеди пин-код на банкомате\n" + "3. Выбери нужную опцию\n";
        }

        if (scene.name == "ShopScene")
        {
            Instantiate(Money, PointToSpawn.transform.position, PointToSpawn.rotation);
            Tasks.text = "1. Возьми корзину с продуктами\n" + "2. Положи ее на кассу\n" + "3. Оплати товары";
        }
    }
}
