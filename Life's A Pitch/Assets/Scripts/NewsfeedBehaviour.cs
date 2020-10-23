using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsfeedBehaviour : MonoBehaviour
{
    [SerializeField]
    private int targetVector;

    public TextMeshProUGUI newsText;
    [SerializeField]
    private Image Background;
    [SerializeField]
    private Color origColor;
    public int newsCategory { get; private set; }

    void Start()
    {
        setNews();
        Background.color = origColor;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, NewsManager.instance.newsNodes[targetVector].transform.position, 0.1f);
        if(targetVector == NewsManager.instance.newsNodes.Length - 3)
        {
            fadeOut();
        }
    }

    public void setNewsCategory()
    {
            GameManager.instance.newsHoveredOver = gameObject;
            Debug.Log("NEWS: " + newsCategory);
    }

    public void incrementTarget()
    {
        targetVector++;
    }

    void setNews()
    {
        newsCategory = Random.Range(0, NewsManager.instance.newsCategories.Count);
        List<string> possibleText = new List<string>(NewsManager.instance.newsCategories[newsCategory].text.Split('\n'));
        string publication = NewsManager.instance.publications[Random.Range(0, NewsManager.instance.publications.Count)];
        string body = possibleText[Random.Range(0, possibleText.Count)].ToString();
        newsText.text = publication + '\n' + body;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        setNewsCategory();
        Debug.Log("COLL");
        Background.color = Color.green;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Background.color = origColor;
    }

    public void destroyNews()
    {
        Destroy(gameObject);
    }

    public void fadeOut()
    {
        GetComponent<Animator>().SetTrigger("Fade");
    }
}
