using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setToQueue(int i)
    {
        ArticleManager.instance.pending.Add(i);
        ArticleManager.instance.setQueue();
    }
}
