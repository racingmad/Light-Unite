using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textPopUp : MonoBehaviour
{
    public string text;
    public float timer;

    private Text prompt;

    // Start is called before the first frame update
    void Start()
    {
        prompt = GetComponent<Text>();
        prompt.CrossFadeAlpha(0, 0.3f, false);
        StartCoroutine(TextAfterTime(text, timer));
    }

    IEnumerator TextAfterTime(string text, float time)
    {
        yield return new WaitForSeconds(time);
        prompt.text = text;
        prompt.CrossFadeAlpha(1, 0.3f, false);
    }
}
