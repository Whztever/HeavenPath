using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public static PlayerEnergy instance;
    public float ep;
    public float maxEp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        ep = maxEp;
        GetComponentInChildren<EnergyBar>().UpdateEp();
    }


}
