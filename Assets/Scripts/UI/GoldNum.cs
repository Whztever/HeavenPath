using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold: MonoBehaviour
{
    public Text goldText;
    public float lifeTime;
    public float floatSpeed;

    private void Start()
    {
        Destroy(gameObject, lifeTime);

    }

    private void Update()
    {
        transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);
    }

    public void ShowGold(int _amount)
    {
        goldText.text = _amount.ToString();
    }

}
