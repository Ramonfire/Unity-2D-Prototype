using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastshown;

    // showing and hiding the floating text
    public void show()
    {
        active = true;
        lastshown = Time.time;
        go.SetActive(active);
    }



    public void hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFlaotingText()
    {
        if (!active) { return; }
        

        //verifying last time the text was shown
        if(Time.time - lastshown > duration) hide();

        go.transform.position += motion * Time.deltaTime;
    }

}
