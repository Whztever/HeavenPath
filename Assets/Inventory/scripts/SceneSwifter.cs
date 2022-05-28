using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwifter : MonoBehaviour
{
    public Button btnA;
    public Animator anmit;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        btnA.onClick.AddListener(Load);
    }

    private void Load()
    {
        StartCoroutine(Loadscene());
    }

    IEnumerator Loadscene()
    {
        anmit.SetBool("Click", true);

        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
