using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //NEWS
    [SerializeField]
    private GameObject Teleprinter;
    [SerializeField]
    public float newsSpawnSpeed;

    void Start()
    {
        instance = this;
        StartCoroutine(createNews());
    }

    IEnumerator createNews()
    {
        yield return new WaitForSeconds(newsSpawnSpeed);
        GameObject NewsPrinter = GameObject.FindGameObjectWithTag("NewsPrinter");
        GameObject news = Instantiate(Teleprinter, NewsPrinter.transform.position, Teleprinter.transform.rotation);
        news.transform.SetParent(NewsPrinter.transform, false);
        //news.GetComponent<NewsfeedBehaviour>().newsText.text = generateGenericFeed();

        StartCoroutine(createNews());
    }

}
