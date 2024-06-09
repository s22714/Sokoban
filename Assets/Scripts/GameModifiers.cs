using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModifiers : MonoBehaviour
{
    public static int levelNumber;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Managers");

        if(objs.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
