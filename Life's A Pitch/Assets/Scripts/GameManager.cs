using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public int score { get; private set; }
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private float colorCounter;
    [SerializeField]
    private Image background;

    [SerializeField]
    private GameObject feedback;



    void Start()
    {
        instance = this;
        setScore();

    }

    private void Update()
    {
        setBackground();
        
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
                AudioManager.instance.playClip("Success_02", 1);
                createFeedback("Excellent!");
            }
            else
            {
                AudioManager.instance.playClip("Failure", 1);
                createFeedback("No Good!");
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
        scoreText.text = "Published: " + '\n' + score.ToString();
        UpgradeManager.instance.setUpgradeText();
    }

    void setBackground()
    {
        background.color = Color.HSVToRGB(colorCounter, 0.1f, 1);
        colorCounter += Time.deltaTime * Time.deltaTime;
        if (colorCounter >= 1)
        {
            colorCounter = 0;
        }
    }

    void createFeedback(string s)
    {
        GameObject fdck = Instantiate(feedback, Input.mousePosition, Quaternion.identity);
        GameObject parent = GameObject.FindGameObjectWithTag("Feedback");
        fdck.transform.SetParent(parent.transform);
        fdck.transform.position = new Vector2(parent.transform.position.x, Input.mousePosition.y);
        fdck.GetComponent<FeedbackBehaviour>().text.text = s;
    }

}
