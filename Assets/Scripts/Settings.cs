using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject Legend;
    [SerializeField] Transform Seat;
    [SerializeField] GameObject Player;

    void Start()
    {
        Legend.SetActive(true);
        Player.transform.position = Seat.position;
    }

    void Update()
    {
        while (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Seat.localPosition += new Vector3(0, -0.0005f, 0);
                Player.transform.position = Seat.position;
                Save();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Seat.localPosition += new Vector3(0, 0.0005f, 0);
                Player.transform.position = Seat.position;
                Save();
            }
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Seat.localPosition += new Vector3(0, 0, 0.0005f);
            Player.transform.position = Seat.position;
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Seat.localPosition += new Vector3(0, 0, -0.0005f);
            Player.transform.position = Seat.position;
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Seat.localPosition += new Vector3(-0.0005f, 0, 0);
            Player.transform.position = Seat.position;
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Seat.localPosition += new Vector3(0.0005f, 0, 0);
            Player.transform.position = Seat.position;
            Save();
        }
    }

    void Save()
    {
        PlayerPrefs.SetFloat("PosX", Seat.localPosition.x);
        PlayerPrefs.SetFloat("PosY", Seat.localPosition.y);
        PlayerPrefs.SetFloat("PosZ", Seat.localPosition.z);
    }
}
