using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorageScript : MonoBehaviour
{
    public static List<StorageScript> allStorages = new List<StorageScript>();
    public bool activated = false;

    private void OnEnable()
    {
        allStorages.Add(this);
    }
    private void OnDisable()
    {
        allStorages.Remove(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crate"))
        {
            activated = true;
        }
        if (allStorages.TrueForAll(x => x.activated))
        {
            CommandInvoker.ClearStack();
            GameModifiers.levelNumber++;
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Crate"))
        {
            activated = false;
        }
    }
}
