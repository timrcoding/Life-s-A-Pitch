using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public bool canUpgrade;
    

    void Start()
    {
        instance = this;    
    }

    public void setUpgradeText()
    {

    }
}
