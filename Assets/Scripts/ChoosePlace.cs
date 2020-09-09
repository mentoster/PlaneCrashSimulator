using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlace : MonoBehaviour
{
    public List<GameObject> Places;
    public SendMessage Plane;
    public GameObject Camera;

    void Update()
    {
        if (Places[0].activeInHierarchy == false && Places[1].activeInHierarchy == false && Places[2].activeInHierarchy == false && Places[3].activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Camera.SetActive(false);
                Places[0].SetActive(true);
                Plane.Activate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Camera.SetActive(false);
                Places[1].SetActive(true);
                Plane.Activate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Camera.SetActive(false);
                Places[2].SetActive(true);
                Plane.Activate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Camera.SetActive(false);
                Places[3].SetActive(true);
                Plane.Activate();
            }
        }
    }
}
