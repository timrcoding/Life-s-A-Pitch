using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public static NewsManager instance;

    //OBJECT
    [SerializeField]
    private GameObject news;
    [SerializeField]
    private GameObject newsParent;
    //NODES
    public GameObject[] newsNodes;
    //CATEGORIES OF NEWS
    [SerializeField]
    private int newsSpawnSpeed;
    public List<TextAsset> newsCategories;
    public TextAsset publicationsText;
    public List<string> publications;


    void Start()
    {
        instance = this;
        StartCoroutine(createNews());
        publications = new List<string>(publicationsText.text.Split('\n'));
    }

    IEnumerator createNews()
    {        
        GameObject newsObj = Instantiate(news, newsNodes[0].transform.position, Quaternion.identity);
        newsObj.transform.SetParent(newsParent.transform, false);
        newsObj.transform.position = newsNodes[0].transform.position;
        incrementNewsTarget();
        //AudioManager.instance.playClip("Notification",.5f);
        yield return new WaitForSeconds(3);
        StartCoroutine(createNews());
    }

    void incrementNewsTarget()
    {
        GameObject[] news = GameObject.FindGameObjectsWithTag("News");
        foreach(GameObject g in news)
        {
            g.GetComponent<NewsfeedBehaviour>().incrementTarget();
        }
    }
}
