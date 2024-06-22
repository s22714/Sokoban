using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorageScript : MonoBehaviour
{
    public static List<StorageScript> allStorages = new List<StorageScript>();
    public bool activated = false;
    public static bool canFinish = false;

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
            StartCoroutine(WaitForStop(collision.GetComponent<BoxMover>()));
        }
        
    }

    private IEnumerator WaitForStop(BoxMover boxMover)
    {
        while (boxMover.moving)
        {
            yield return new WaitForSeconds(0.1f);
        }
        activated = true;
        if (allStorages.TrueForAll(x => x.activated))
        {
            PlayerMover._animator.SetBool("Win", true);
            
            while(!canFinish)
            {
                yield return new WaitForSeconds(0.1f);
            }

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
