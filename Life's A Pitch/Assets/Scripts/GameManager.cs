using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //NEWS
    [SerializeField]
    private GameObject Teleprinter;

    public float globalSpeed;

    //ARTICLE NEWS MATCHING
    public GameObject articleHeld;
    public GameObject newsHoveredOver;
    //THIS MEANS THAT AN ARTICLE IS BEING HELD AND THUS A MATCH CAN BE MADE;
    public bool matcheable;

    void Start()
    {
        instance = this;

    }

    

    public IEnumerator checkForMatch()
    {
        yield return new WaitForEndOfFrame();
        if (matcheable) 
        {
            int articleId = articleHeld.GetComponent<ArticleBehaviour>().articleCategory;
            int newsId = newsHoveredOver.GetComponent<NewsfeedBehaviour>().newsCategory;
            if (articleId == newsId)
            {
                newsHoveredOver.GetComponent<NewsfeedBehaviour>().fadeOut();
                articleHeld.GetComponent<ArticleBehaviour>().fadeOut();
            }
            else
            { 
                Destroy(articleHeld);
            }
            resetNode();
         }
    }

    void resetNode()
    {
        int num = articleHeld.GetComponent<ArticleBehaviour>().articleNodeRef;
        ArticleManager.instance.resetNode(num);
    }

}
