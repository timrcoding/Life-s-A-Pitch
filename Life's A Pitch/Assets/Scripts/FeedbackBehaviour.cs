using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackBehaviour : MonoBehaviour
{

    public TextMeshProUGUI text;
    public void destroyObj()
    {
        Destroy(gameObject);
    }
}
