using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour {

    private Button b;
    private Image image;


    private string hoverColorString = "#41DB75D5";
    private Color hoverColor;

    Color initialImageColor;

    // Use this for initialization
    void Start () {

        b = this.GetComponent<Button>();

        image = this.GetComponent<Image>();

        ColorUtility.TryParseHtmlString(hoverColorString, out hoverColor);

        initialImageColor = image.color;

        hoverColor.a = 1f;
    }


    public void enterHover()
    {
        image.color = hoverColor;

    }

    public void exitHover()
    {
        image.color = initialImageColor;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
