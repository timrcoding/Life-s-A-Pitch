using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public bool canUpgrade = true;
    public GameObject button;
    public int upgradeCount;
    public GameObject[] typeWriters;
    

    void Start()
    {
        instance = this;
        setUpgradeText();
        setTypeWriters();
    }

    public void setUpgradeText()
    {
        int num = GameManager.instance.score;
        if (num % 5 == 0 && num != 0)
        {
            num = num / 10;
            button.GetComponent<Button>().interactable = true;
            canUpgrade = true;
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
            canUpgrade = false;
        }
    }

    public void createIntern()
    {
        if (canUpgrade)
        {
            if (ArticleManager.instance.articleSpawnSpeed > 1)
            {
                ArticleManager.instance.articleSpawnSpeed -= 1;
            }
            canUpgrade = false;
            button.GetComponent<Button>().interactable = false;
            upgradeCount++;
            setTypeWriters();
            AudioManager.instance.playClip("Type", 1);
        }
    }

    public void setTypeWriters()
    {
        for(int i = 0; i < typeWriters.Length; i++)
        {
            if(i < upgradeCount)
            {
                typeWriters[i].SetActive(true);
            }
            else
            {
                typeWriters[i].SetActive(false);
            }
        }
    }
}
