using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlace : MonoBehaviour
{
    public GameObject Player;
    public GameObject Map;
    public List<Transform> Places;
    public SendMessage Plane;
    bool onPlace = false;

    void Update()
    {
        if (!onPlace)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Player.transform.position = Places[0].transform.position;
                Plane.Activate();
                Map.SetActive(false);
                onPlace = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Player.transform.position = Places[1].transform.position;
                Plane.Activate();
                Map.SetActive(false);
                onPlace = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Player.transform.position = Places[2].transform.position;
                Plane.Activate();
                Map.SetActive(false);
                onPlace = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Player.transform.position = Places[3].transform.position;
                Plane.Activate();
                Map.SetActive(false);
                onPlace = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
