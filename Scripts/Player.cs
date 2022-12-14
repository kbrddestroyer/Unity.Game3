using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float speed;
    [SerializeField] public GameObject ghostPrefab;
    [SerializeField] private AudioClip step;
    [SerializeField] private Transform ghostPosition;
    private AudioSource audioSource;
    public bool active = true;
    public bool alive = false;
    private Dialogue dialogue;

    private Animator animator;

    public bool item = false;
    private IEnumerator Blink()
    {
        while (true)
        {
            animator.SetBool("Blink", false);
            yield return new WaitForSeconds(10f);
            animator.SetBool("Blink", true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void ability()
    {
        GameObject ghost = Instantiate(ghostPrefab, ghostPosition.position, Quaternion.identity);
        Camera.main.GetComponent<CameraController>().player = ghost.transform;
        ghost.GetComponent<Ghost>().parent = this.gameObject;
        active = false;
        Capsule capsule = GameObject.Find("capsule").GetComponent<Capsule>();
        if (capsule)
        {
            capsule.ghost = ghost.GetComponent<Ghost>();
        }
        animator.SetFloat("Speed", 0);
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
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Blink());
    }

    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ability();
            }
            if (alive)
            {
                Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                transform.Translate(direction * Time.deltaTime * speed);

                if (direction != Vector2.zero && !audioSource.isPlaying)
                {
                    audioSource.clip = step;
                    audioSource.Play();
                }

                animator.SetFloat("Speed", (Vector2.Distance(Vector2.zero, direction) > 0 ? 1 : 0));
                if (direction.x < 0) GetComponent<SpriteRenderer>().flipX = true;
                else if (direction.x > 0) GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
