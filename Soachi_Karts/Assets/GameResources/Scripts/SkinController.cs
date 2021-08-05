using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinController : MonoBehaviour
{
    private Renderer colorSkin;
    private int changeColor=0;
    public Text UIText1;
    public Text UIText2;
    public Text UIText3;

    void Start()
    {
        changeColor = PlayerPrefs.GetInt("Color", 0);
        colorSkin = GetComponent<Renderer>();
        Change();

        UIText1.enabled = true;
        UIText2.enabled = true;
        UIText3.enabled = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !MoveControler.start)
        {
            changeColor++;
            Change();            
        }

        if(MoveControler.start)
        {
            UIText1.enabled = false;
            UIText2.enabled = false;
            UIText3.enabled = false;
        }
    }

    private void Change()
    {
        switch(changeColor)
        {
            case 1:
                colorSkin.material.SetColor("_Color", Color.blue);
                break;
            case 2:
                colorSkin.material.SetColor("_Color", Color.green);
                break;
            case 3:
                colorSkin.material.SetColor("_Color", Color.red);
                break;
            case 4:
                colorSkin.material.SetColor("_Color", Color.white);
                break;
            case 5:
                changeColor = 0;
                break;
        }

        PlayerPrefs.SetInt("Color", changeColor);
    }
}
