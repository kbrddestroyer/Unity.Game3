using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float speed;
    [SerializeField] public GameObject ghostPrefab;
    public bool active = true;
    private Dialogue dialogue;

    private Animator animator;
    private void ability()
    {
        animator.SetFloat("Speed", 0);
        GameObject ghost = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraController>().player = ghost.transform;
        ghost.GetComponent<Ghost>().parent = this.gameObject;
        active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerZoneText trigger = collision.GetComponent<TriggerZoneText>();
        if (trigger)
        {
            dialogue.say(trigger.text);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogue = GetComponent<Dialogue>();
    }

    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ability();
            }
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.Translate(direction * Time.deltaTime * speed);
            animator.SetFloat("Speed", Vector2.Distance(Vector2.zero, direction));
            if (direction.x < 0) GetComponent<SpriteRenderer>().flipX = true;
            else if (direction.x > 0) GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
