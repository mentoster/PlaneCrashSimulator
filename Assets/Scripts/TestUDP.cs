using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System;

public class TestUDP : MonoBehaviour
{
    public void LeanForwardNBack(float n)
    {
        transform.eulerAngles = new Vector3(n, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void LeanRightNLeft(float n)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, n);
    }
}
