using UnityEngine;
//战利品对的随机掉落，调用LootSetting
public class LootSpawner : MonoBehaviour
{
    public static LootSpawner instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField] LootSetting[] lootSettings;

    public void Spawn(Vector3 position)
    {
        foreach (var item in lootSettings)
        {
            if (Random.Range(0f, 100f) <= item.dropPercentage)
            {Debug.Log(222);
                GameObject Gold=Instantiate(item.prefab,position,Quaternion.identity);

            }

        }
    }
}