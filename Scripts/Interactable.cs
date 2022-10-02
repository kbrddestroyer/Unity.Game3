using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().item = true;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }
    }
}
