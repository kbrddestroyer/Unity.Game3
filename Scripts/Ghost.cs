using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float speed;
    public GameObject parent;

    private bool isActive = false;

    // Start is called before the first frame update

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isActive)
        {
            StartCoroutine(timer());
            isActive = true;
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(10f);

        Camera.main.GetComponent<CameraController>().player = parent.transform;
        parent.GetComponent<Player>().active = true;
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StopAllCoroutines();
            Camera.main.GetComponent<CameraController>().player = parent.transform;
            parent.GetComponent<Player>().active = true;
            Destroy(this.gameObject);
        }
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(direction * Time.deltaTime * speed);
        if (direction.x < 0) GetComponent<SpriteRenderer>().flipX = false;
        else if (direction.x > 0) GetComponent<SpriteRenderer>().flipX = true;
    }
}
