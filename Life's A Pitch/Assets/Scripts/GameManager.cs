using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //NEWS
    [SerializeField]
    private GameObject Teleprinter;

    public float globalSpeed;
    public List<string> categories;

    //ARTICLE NEWS MATCHING
    public GameObject articleHeld;
    public GameObject newsHoveredOver;
    //THIS MEANS THAT AN ARTICLE IS BEING HELD AND THUS A MATCH CAN BE MADE;
    public bool matcheable;

    [SerializeField]
    private int score;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start()
    {
        instance = this;
        setScore();

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
                
                score++;
                setScore();
            }
            else
            { 
               // Destroy(articleHeld);
            }
            articleHeld.GetComponent<ArticleBehaviour>().fadeOut();
            resetNode();
         }
    }

    void resetNode()
    {
        int num = articleHeld.GetComponent<ArticleBehaviour>().articleNodeRef;
        ArticleManager.instance.resetNode(num);
    }

    public void setScore()
    {
        scoreText.text = "Score" + '\n' + score.ToString();
    }

}
