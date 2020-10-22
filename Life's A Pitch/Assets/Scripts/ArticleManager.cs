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
    private float articleSpawnSpeed;

    //ARTICLE MATERIALS
    public TextAsset NamesText;
    public List<string> names;
    public List<TextAsset> articleCategories;
    public Color[] articleColors;

    //PENDING ARTICLES;
    public GameObject[] pendingNodes;
    public List<int> pending;

    void Start()
    {
        instance = this;
        nodeUsed = new bool[articleNodes.Count];
        names = new List<string>(NamesText.text.Split('\n'));
        StartCoroutine(createArticle());
    }

    IEnumerator createArticle()
    {
        yield return new WaitForSeconds(articleSpawnSpeed);
        if(articleNodes.All(x => x)){
            int num = checkFreeArticleNode();
            Vector3 nodePos = articleNodes[num].transform.position;
            GameObject Article = Instantiate(article, article.transform.position, Quaternion.identity);
            Article.transform.SetParent(articleParent.transform,false);
            Article.transform.position = nodePos;
            Article.GetComponent<ArticleBehaviour>().articleNodeRef = num; 
        }
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
        for(int i = 0; i < pending.Count; i++)
        {
            //pendingNodes[i].GetComponent<QueueNode>().text.text = 
        }
    }
}
