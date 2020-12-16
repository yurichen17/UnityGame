using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombScript : MonoBehaviour
{
    public static int bombNum = 0;
    Text bombN;
    // Start is called before the first frame update
    void Start()
    {
        bombN = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bombN.text == null)
        {
            Debug.Log("the bomb text is null");
        }
        else
        {
            bombN.text = "Bombs: " + bombNum;
        }
    }
}
