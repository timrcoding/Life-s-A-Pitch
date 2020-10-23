using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArticleManager : MonoBehaviour
{
    public static ArticleManager instance;

    //OBJECTS
    [SerializeField]
    private GameObject article;
    [SerializeField]
    private List<GameObject> articleNodes;
    [SerializeField]
    private bool[] nodeUsed;
    [SerializeField]
    GameObject articleParent;

    //VARIABLES
    [SerializeField]
    public float articleSpawnSpeed;

    //ARTICLE MATERIALS
    public TextAsset descriptionsText;
    public List<string> descriptions;
    public List<TextAsset> articleCategories;
    public Color[] articleColors;

    

    //PENDING ARTICLES;
    public GameObject[] pendingNodes;
    public List<int> pending;

    //TIMER
    public float timer;
    [SerializeField]
    private bool startTimer;
    [SerializeField]
    Image timerBar;
    [SerializeField]
    Image timerBarBackground;


    void Start()
    {
        instance = this;
        nodeUsed = new bool[articleNodes.Count];
        descriptions = new List<string>(descriptionsText.text.Split('\n'));
        StartCoroutine(createArticle());
        setQueue();
    }

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
        }

   
        
    }


    IEnumerator createArticle()
    {
        startTimer = false;
        timer = 0;
        if(articleNodes.All(x => x)){
            int num = checkFreeArticleNode();
            Vector3 nodePos = articleNodes[num].transform.position;
            GameObject Article = Instantiate(article, article.transform.position, Quaternion.identity);
            Article.transform.SetParent(articleParent.transform,false);
            Article.transform.position = nodePos;
            Article.GetComponent<ArticleBehaviour>().articleNodeRef = num;
            if (pending.Count != 0)
            {
                Article.GetComponent<ArticleBehaviour>().articleCategory = pending[0];
            }
            else
            {
                Article.GetComponent<ArticleBehaviour>().articleCategory = Random.Range(0, articleCategories.Count-1);
            }
            
            removeFromQueue();
            setQueue();
        }
        StartCoroutine(setBar());
        yield return new WaitForSeconds(articleSpawnSpeed);
        StartCoroutine(createArticle());
        
    }

    int checkFreeArticleNode()
    {
        int num = 0;

        for(int i = 0; i < articleNodes.Count; i++)
        {
            if (!nodeUsed[i])
            {
                num = i;
                break;
            }
        }
        nodeUsed[num] = true;
        return num;
    }

    public void resetNode(int i)
    {
        nodeUsed[i] = false;
    }

    public void setQueue()
    {
        foreach(GameObject g in pendingNodes)
        {
            g.GetComponent<QueueNode>().text.text = "";
            g.GetComponent<Image>().color = Color.clear;
        }

        for(int i = 0; i < pending.Count; i++)
        {
            QueueNode qn = pendingNodes[i].GetComponent<QueueNode>();
            qn.text.text = GameManager.instance.categories[pending[i]];
            qn.GetComponent<Image>().color = articleColors[pending[i]];
        }
    }

    public void removeFromQueue()
    {
        if (pending.Count != 0)
        {
            setQueue();
            pending.RemoveAt(0);
            
        }
    }

    IEnumerator setBar()
    {
        
        timerBar.rectTransform.sizeDelta = new Vector2(0, 20);
        timerBarBackground.rectTransform.sizeDelta = new Vector2((articleSpawnSpeed-1) * 100 + 20,30);
        for (int i = 0; i < articleSpawnSpeed; i++)
        {
            timerBar.rectTransform.sizeDelta = new Vector2(i* 100, 20);
            yield return new WaitForSeconds(1);
        }
        AudioManager.instance.playClip("Finished",1);

        
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
