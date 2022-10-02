using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float delay;
    [SerializeField] private Text attachedText;
    
    private IEnumerator print(string str)
    {
        foreach (char c in str)
        {
            attachedText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    public void say(string text)
    {
        StartCoroutine(print(text));
    }
}
