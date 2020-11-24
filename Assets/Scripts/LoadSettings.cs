using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    [SerializeField] Transform[] Seats;
    void Start()
    {
        if (PlayerPrefs.HasKey("PosX") && PlayerPrefs.HasKey("PosY") && PlayerPrefs.HasKey("PosZ"))
        {
            float x = PlayerPrefs.GetFloat("PosX");
            float y = PlayerPrefs.GetFloat("PosY");
            float z = PlayerPrefs.GetFloat("PosZ");
            for (int i = 0; i < Seats.Length; i++)
            {
                Seats[i].localPosition = new Vector3(x, y, z);
            }
        }
    }
}
