using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] [Range(2, 5)] int InteractionTime = 3;
    [SerializeField] Settings Settings;
    [SerializeField] GameObject[] Cameras;
    GameObject Canvas;
    [SerializeField] GameObject Timer;
    [SerializeField] Slider Slider;
    [SerializeField] Image Fill;
    [SerializeField] Color Exit_clr;
    [SerializeField] Color Restart_clr;
    float interact_timer = 0f;
    bool isRestarting, isExit = false;

    void Start()
    {
        Canvas = this.gameObject;
        if (Cameras[0].activeInHierarchy)
        {
            Canvas.transform.parent = Cameras[0].transform;
        }
        else
        {
            Canvas.transform.parent = Cameras[1].transform;
        }
        Canvas.transform.localPosition = new Vector3(0, 0, 0.48f);
        Canvas.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    void Update()
    {
        while (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Settings.enabled = true;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isExit = false;
            interact_timer = 0f;
            Slider.value = 0f;
            Fill.color = Restart_clr;
            Timer.SetActive(true);
            isRestarting = true;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            Timer.SetActive(false);
            isRestarting = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isRestarting = false;
            interact_timer = 0f;
            Slider.value = 0f;
            Fill.color = Exit_clr;
            Timer.SetActive(true);
            isExit = true;
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            Timer.SetActive(false);
            isExit = false;
        }

        if (isRestarting || isExit)
        {
            interact_timer += Time.deltaTime;
            Slider.value = interact_timer;
            if ((int)interact_timer == 3)
            {
                if (isRestarting)
                {
                    isRestarting = false;
                    Timer.SetActive(false);
                    Restart();
                }
                else if (isExit)
                {
                    isExit = false;
                    Timer.SetActive(false);
                    Exit();
                }
                interact_timer = 0f;
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Exit()
    {
        Application.Quit();
    }
}
