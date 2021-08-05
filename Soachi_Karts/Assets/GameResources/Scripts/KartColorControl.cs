using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartColorControl : MonoBehaviour
{
    private Renderer colorSkin;
    private int changeColorC = 0;

    void Start()
    {
        changeColorC = PlayerPrefs.GetInt("ColorKart", 0);
        colorSkin = GetComponent<Renderer>();
        Change();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !MoveControler.start)
        {
            changeColorC++;
            Change();
        }
    }

    private void Change()
    {
        switch (changeColorC)
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
                changeColorC = 0;
                break;
        }

        PlayerPrefs.SetInt("ColorKart", changeColorC);
    }
}
