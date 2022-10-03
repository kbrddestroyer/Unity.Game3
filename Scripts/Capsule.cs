using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float interactDistance;
    public Ghost ghost = null;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ghost != null)
        {
            if (Vector2.Distance(transform.position, ghost.transform.position) < interactDistance)
            {
                animator.SetBool("opened", true);
                ghost.parent.GetComponent<Player>().alive = true;
                ghost.parent.GetComponent<Animator>().SetBool("alive", true);
                GetComponent<Capsule>().enabled = false;
            }
        }
    }
}
