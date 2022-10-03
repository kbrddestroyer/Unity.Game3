using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSurrounding : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float interactDistance;
    private Transform player;
    private Animator animator;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    public void interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().item = true;
        animator.SetBool("enabled", true);
        GetComponent<InteractableSurrounding>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, player.position) <= interactDistance)
        {
            interact();
        }
    }
}
