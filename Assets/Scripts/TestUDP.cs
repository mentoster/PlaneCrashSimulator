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
    [SerializeField]private int port = 7001;
    UdpClient Listener;
    IPEndPoint RemoteIpEndPoint;
    static readonly object lockObject = new object();
    Thread thread;
    bool process = false;
    string axis = "";
    float n = 0f;
    float m = 0f;

    private void Start()
    {
        thread = new Thread(new ThreadStart(ThreadMethod));     // создается поток
        thread.Start();                                         // запускается поток
    }

    private void Update()
    {
        if (process)
        {                                                       // Вызов функций, отвечающих за поворот
            process = false;
            if (axis == "pitch")
            {
                LeanForwardNBack(n);
            }
            else if (axis == "roll")
            {
                LeanRightNLeft(n);
            }
            else if (axis == "pr")
            {
                LeanForwardNBack(n);
                LeanRightNLeft(m);
            }
            n = 0f;
            m = 0f;
            axis = "";
        }
    }

    private void ThreadMethod()
    {
        Listener = new UdpClient(port);
        while (true)
        {
            RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receiveBytes = Listener.Receive(ref RemoteIpEndPoint);
            
            lock (lockObject)
            {
                string i = Encoding.ASCII.GetString(receiveBytes);
                string[] temp = i.Split(' ');
                //Debug.Log(i);
                if (temp[0] == "pitch" || temp[0] == "roll")
                {
                    axis = temp[0];
                    n = Convert.ToSingle(temp[1]);
                    process = true;
                }
                if (temp[0] == "pr")
                {
                    axis = temp[0];
                    //Debug.Log(temp[1] + " " + temp[2]);
                    n = Convert.ToSingle(temp[1]);
                    m = Convert.ToSingle(temp[2]);
                    process = true;
                }
            }
        }
    }

    public void LeanForwardNBack(float n)
    {
        transform.eulerAngles = new Vector3(n, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void LeanRightNLeft(float n)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, n);
    }
}
