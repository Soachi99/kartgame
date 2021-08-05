using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSource : MonoBehaviour
{

    public Text textScore1;
    public Text textScore2;
    public Text textScore3;
    public Text textVueltas;

    public float Score1;
    public float Score2;
    public float Score3;

    public float score;
    // Start is called before the first frame update
    void Start()
    {
        CargarDatos();
        score = MoveControler.Score;

        if (score > Score1 && score > Score2 && score > Score3)
        {
            Score3 = Score2;
            Score2 = Score1;
            Score1 = score;               
        }
        else if (score < Score1 && score > Score2 && score > Score3)
        {
            Score3 = Score2;
            Score2 = score;            
        }
        else if (score < Score1 && score < Score2 && score > Score3)
        {
            Score3 = score;            
        }

        textScore1.text = Score1.ToString();
        textScore2.text = Score2.ToString();
        textScore3.text = Score3.ToString();
        textVueltas.text = MoveControler.numVueltas.ToString();

        GuardarDatos();
    }

    public void CargarDatos()
    {
        Score1 = PlayerPrefs.GetFloat("Score1", 0);
        Score2 = PlayerPrefs.GetFloat("Score2", 0);
        Score3 = PlayerPrefs.GetFloat("Score3", 0);
    }

    public void GuardarDatos()
    {
        PlayerPrefs.SetFloat("Score1", Score1);
        PlayerPrefs.SetFloat("Score2", Score2);
        PlayerPrefs.SetFloat("Score3", Score3);
    }
}
