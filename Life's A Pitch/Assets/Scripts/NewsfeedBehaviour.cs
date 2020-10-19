using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsfeedBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector2 newTarget;
    [SerializeField]
    private float speed;
    public TextMeshProUGUI newsText;

    void Start()
    {
        newTarget = transform.position;
        StartCoroutine(moveFeed());
        StartCoroutine(destroyObject());
        //AudioManager.instance.playClip("Teleprinter");
        
    }
    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, newTarget, step);
    }

    IEnumerator moveFeed()
    {
        Debug.Log("MOVING");
        newTarget = new Vector2(transform.position.x, transform.position.y + 1f);
        yield return new WaitForSeconds(GameManager.instance.newsSpawnSpeed);
        StartCoroutine(moveFeed());
    }

    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }
}
