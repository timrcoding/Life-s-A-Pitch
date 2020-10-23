using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoad : MonoBehaviour
{
    public static AsyncLoad instance;
    private UnityEngine.AsyncOperation async;
    void Start()
    {
        instance = this;
        //STARTS ASYCHRONOUSLY LOADING LEVEL AS SOON AS SCENE STARTS    
        StartCoroutine(loadAsyncNextLevel());
    }
    IEnumerator loadAsyncNextLevel()
    {
        //WAITS FOR SCENE TO START
        yield return new WaitForSeconds(0.1f);
        //SELECTS NEXT SCENE TO LOAD
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        //HOLDS BACK LOADING THIS SCENE
        async.allowSceneActivation = false;
        //RETURNS AS AN ASYNC OPERATION
        yield return async;

    }

    IEnumerator reLoadLevel()
    {
        //WAITS FOR SCENE TO START
        yield return new WaitForSeconds(0.1f);
        //SELECTS NEXT SCENE TO LOAD
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //HOLDS BACK LOADING THIS SCENE
        async.allowSceneActivation = false;
        //RETURNS AS AN ASYNC OPERATION
        yield return async;

    }

    public void switchChangeLevel()
    {
        //ADVANCES ASYNC OPERATION
        async.allowSceneActivation = true;
        //LevelManager.instance.setLevelDifficulty();
    }
}
