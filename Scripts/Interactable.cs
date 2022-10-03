using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float interactDistance;
    private Player player;
    private IEnumerator Pickup()
    {
        player.GetComponent<Animator>().SetBool("pickup", true);
        yield return new WaitForSeconds(0.4f);
        player.GetComponent<Animator>().SetBool("pickup", false);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().item = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, player.transform.position) <= interactDistance)
        {
            interact();
            StartCoroutine(Pickup());
        }
    }
}
