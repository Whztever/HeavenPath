using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword;//玩家从哪个房间来的房间的名字

    private void Start()
    {
        if(PlayerController.instance.scenePassword == entrancePassword)
        {
            PlayerController.instance.transform.position = transform.position;
        }
        else
        {
           // Debug.LogError("Wrong PW. Please Check your Scene name and Entrance password");
        }
    }
}
