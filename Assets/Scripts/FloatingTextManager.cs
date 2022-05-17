using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        foreach (FloatingText t in floatingTexts)
        {
            t.UpdateFlaotingText();
        }
    }



    private List<FloatingText> floatingTexts = new List<FloatingText>();

    public void show(string msg,int fontsize,Color color,Vector3 position,Vector3 motion,float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontsize;
        floatingText.txt.color = color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);//transfering world space to screen space to use it in the UI
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.show();
    }



    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null) {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();
            floatingTexts.Add(txt);
        }

        return txt;
    }
}
