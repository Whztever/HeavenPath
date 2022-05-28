using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneName;//场景转换的场景名字
    [SerializeField] public string password;//离开场景时赋的//当前场景的名字

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //场景转换前保存所有NPC委派任务的当前状态
            #region 
            //Questable[] questables = FindObjectsOfType<Questable>();
            //if(questables != null)
            //{
            //    foreach(Questable questable in questables)
            //    {
            //        questable.SaveData();
            //    }
            //}
            #endregion

            PlayerController.instance.scenePassword = password;
            //异步场景切换，Unity会在后台线程中加载所有资源，并保存之前场景的游戏对象，我们在加载过程中获取到加载的进度
            //todo可以播放一个真实的进度条，来看加载进度。
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

}
