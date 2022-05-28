using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public GameObject evenObj;
    public Button btnA;
    public Button btnB;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.evenObj);

        btnA.onClick.AddListener(loadSceneA);
        btnB.onClick.AddListener(loadSceneB);
    }

    private void loadSceneB()
    {
        StartCoroutine(Loadscene(3));
    }

    private void loadSceneA()
    {
        StartCoroutine(Loadscene(2));
    }

    IEnumerator Loadscene(int index)
    {
        animator.SetBool("FadeIn", true);
        animator.SetBool("FadeOut", false);

        yield return new WaitForSeconds(1);


        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadedScene;
    }

    private void OnLoadedScene(AsyncOperation obj)
    {
        animator.SetBool("FadeIn", false);
        animator.SetBool("FadeOut", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
