using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveControler : MonoBehaviour
{
    // Obtener movimiento mediante teclas
    public float Acelerar;
    public float Rotar;
    // Asignación de gravedad
    public float Gravedad = 9.8f;
    // Aignación velocidad del carrito
    private float speed = 20;
    private float rotationSpeed = 50;
    // Activación y duración del turbo
    private bool turbo;
    private float timeTurbo;
    // Activación y duración de la desorientación
    private bool desorientacion;
    private float timeOrientacion;
    // Start
    private bool start;
    private float timeStart;
    public Text textStart;
    // Score
    public Text ScoreText;
    public static float Score;    
    public bool vfx;
    private float timeVfx;
    // Numero de vueltas
    public static int numVueltas;


    // VFX
    public GameObject vfxTurbo;
    public GameObject vfxDes;
    public GameObject vfxTime;


    public CharacterController player;

    
    void Start()
    {
        player = GetComponent<CharacterController>();
        numVueltas++;
        Time.timeScale = 1;
        timeTurbo = 0;
        timeOrientacion = 0;
        turbo = false;
        desorientacion = false;
        start = false;
        timeStart = 4;
        textStart.enabled = true;
        Score = 15;
        vfx = false;


        // VFX
        vfxTurbo.SetActive(false);
        vfxDes.SetActive(false);
        vfxTime.SetActive(false);
    }    
        
    void Update()
    {
        
        if(!start)
        {
            timeStart-= Time.deltaTime;
            textStart.text = ((int)timeStart).ToString();
            if(timeStart <= 0)
            {
                start = true;                
            }
        }
        else
        {
            timeStart = 3;
            textStart.enabled = false;
        }

        
        if (Score <= 20 && start)
        {
           Score -= Time.deltaTime;
           ScoreText.text = Score.ToString();
        }    

        if(Score < 0)
        {
            Time.timeScale = 0;
            start = false;
            LoadScene("LoseScene");
        }
               
        if (turbo)
        {
            timeTurbo += Time.deltaTime;
            if(timeTurbo >= 3)
            {
                speed = 20;
                rotationSpeed = 50;
                timeTurbo = 0;
                turbo = false;
                vfxTurbo.SetActive(false);
            }
        }

        if (desorientacion)
        {
            timeOrientacion += Time.deltaTime;
            if (timeOrientacion >= 2)
            {
                Acelerar = Input.GetAxis("Vertical");
                Rotar = Input.GetAxis("Horizontal");
                timeOrientacion = 0;
                desorientacion = false;
                vfxDes.SetActive(false);
            }
        }
        else
        {
            Acelerar = Input.GetAxis("Vertical");
            Rotar = Input.GetAxis("Horizontal");
        }

        if (vfx)
        {
            timeVfx += Time.deltaTime;
            if (timeVfx>= 1.5f)
            {
                vfx = false;
                vfxTime.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (start)
        {
            Vector3 rotation = new Vector3(0, Rotar * rotationSpeed * Time.deltaTime, 0);
            Vector3 move = new Vector3(0, -Gravedad * Time.deltaTime, Acelerar * Time.deltaTime);

            move = this.transform.TransformDirection(move);
            player.Move(move * speed);
            this.transform.Rotate(rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turbo")
        {
            turbo = true;
            speed = 40;
            rotationSpeed = 100;
            Destroy(other.gameObject);
            vfxTurbo.SetActive(true);
        }

        if (other.tag == "Perdida")
        {
            desorientacion = true;
            Acelerar = Input.GetAxis("Horizontal");
            Rotar = Input.GetAxis("Vertical");
            Destroy(other.gameObject);
            vfxDes.SetActive(true);
        }

        if (other.tag == "Tiempo")
        {
            vfx = true;
            Score += 1;
            Destroy(other.gameObject);            
            vfxTime.SetActive(true);
        }

        if (other.tag == "Meta")
        {
            Time.timeScale = 0;
            start = false;
            LoadScene("WinScene");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }







}
