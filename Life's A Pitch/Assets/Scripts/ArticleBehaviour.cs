using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArticleBehaviour : MonoBehaviour
{
    private bool draggable;
    public int articleCategory;
    public int articleNodeRef;

    //ARTICLE MATERIALS
    [SerializeField]
    private TextMeshProUGUI articleText;
    [SerializeField]
    public Image Background;

    void Start()
    {
        setText();
    }

    private void Update()
    {
        if (draggable)
        {
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,transform.position.z);
        }
    }

    public void dragArticle()
    {
        draggable = true;
        GameManager.instance.articleHeld = gameObject;
        GameManager.instance.matcheable = true;
        Debug.Log("ARTICLE: " + articleCategory);
    }

    public void dropArticle()
    {
        draggable = false;
        StartCoroutine(GameManager.instance.checkForMatch());
        
    }

    void setText()
    {
        int num = 0;
        if (ArticleManager.instance.pending.Count != 0)
        {
            num = ArticleManager.instance.pending[0];
        }
        else
        {
            num = Random.Range(0, ArticleManager.instance.articleCategories.Count);
        }
        articleCategory = num;
        Background.color = ArticleManager.instance.articleColors[articleCategory];
        List<string> possibleText = new List<string>(ArticleManager.instance.articleCategories[articleCategory].text.Split('\n'));
        string description = ArticleManager.instance.descriptions[Random.Range(0, ArticleManager.instance.descriptions.Count)];
        string gameName = possibleText[Random.Range(0, possibleText.Count)].ToString();
        string combined  = gameName + '\n' + description;
        articleText.text = combined;
    }

    public void destroyArticle()
    {
        Destroy(gameObject);
    }

    public void fadeOut()
    {
        GetComponent<Animator>().SetTrigger("Fade");
    }
}

