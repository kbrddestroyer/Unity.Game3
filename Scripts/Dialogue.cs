using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float delay;
    [SerializeField] private Text attachedText;
    
    [SerializeField] private AudioClip beep;

    private AudioSource audioSource;
    private bool skiping = false;
    public bool printing = false;
    private IEnumerator print(string[] text)
    {
        printing = true;
        foreach (string str in text)
        {
            skiping = false;
            attachedText.text = "";
            foreach (char c in str)
            {
                attachedText.text += c;
                if (skiping) break;
                audioSource.clip = beep;
                audioSource.Play();
                yield return new WaitForSeconds(delay);
            }
            attachedText.text = str;
            skiping = false;
            while (!skiping) yield return new WaitForEndOfFrame();
        }
        printing = false;
    }

    public void say(string[] text)
    {
        if (!printing) StartCoroutine(print(text));
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && printing) skiping = true;
    }
}
