using UnityEngine;
using ChairControl.ChairWork;
using System.Net;
using System.Collections;

public class SendMessage : MonoBehaviour
{
    private FutuRiftController controller;
    private FutuRiftController controller2;
    private float pitch = 0;
    private float roll = 0;
    public TestUDP TU;
    [Space(10)]
    [Header("Connection settings")]
    public int port = 7000;
    private int port2 = 7001;
    public string ip = "192.168.1.255";
    [Space(10)]
    [Header("Start settings")]
    [Range(0, 60)] public int TimeToStart = 10;
    [Range(0.01f, 1f)] public float ShakeTick = 0.1f;
    [Range(0.1f, 1f)] public float PitchShake = 0.5f;
    [Space(10)]
    [Header("Lean per tick settings")]
    [Range(0.01f, 1f)] public float PitchTick = 0.065f;
    [Range(0.01f, 0.06f)] public float PitchPerTick = 0.03f;
    [Range(0.01f, 1f)] public float RollLeftTick = 0.025f;
    [Range(0.01f, 1f)] public float RollRightTick = 0.015f;
    [Range(0.01f, 0.06f)] public float RollPerTick = 0.035f;
    [Space(10)]
    [Header("Shake per tick settings")]
    [Range(0.1f, 1f)] public float RollPerTickShake = 0.25f;
    [Range(0.1f, 1f)] public float PitchPerTickShake = 0.3f;
    [Range(0.01f, 0.1f)] public float ShakeInterval = 0.025f;

    void Start()
    {
        controller = new FutuRiftController(port, IPAddress.Parse(ip))
        {
            Pitch = pitch,
            Roll = roll
        };
        controller.Start();
        StartCoroutine("Strt");
    }

    private void OnApplicationQuit()
    {
        controller.Stop();
    }

    IEnumerator Strt()
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(TimeToStart);
        StartCoroutine("Pitch");
        StartCoroutine("Roll");
        StartCoroutine("Shake");
    }

    IEnumerator Pitch()
    {
        controller.Pitch = controller.Pitch - PitchShake;
        TU.LeanForwardNBack(controller.Pitch - PitchShake);
        yield return new WaitForSeconds(ShakeTick);
        controller.Pitch = controller.Pitch + PitchShake;
        TU.LeanForwardNBack(controller.Pitch - PitchShake);
        while (pitch < 21)
        {
            controller.Pitch = controller.Pitch + PitchPerTick;
            TU.LeanForwardNBack(controller.Pitch - PitchShake);
            pitch = controller.Pitch;
            yield return new WaitForSeconds(PitchTick);
        }
    }

    IEnumerator Roll()
    {
        while(roll < 18)
        {
            controller.Roll = controller.Roll + RollPerTick;
            TU.LeanRightNLeft(controller.Roll + RollPerTick);
            roll = controller.Roll;
            yield return new WaitForSeconds(RollLeftTick);
        }
        while (roll > -18)
        {
            controller.Roll = controller.Roll - RollPerTick;
            TU.LeanRightNLeft(controller.Roll + RollPerTick);
            roll = controller.Roll;
            yield return new WaitForSeconds(RollRightTick);
        }
    }

    IEnumerator Shake()
    {
        while (true)
        {
            controller.Roll = controller.Roll + RollPerTickShake;
            TU.LeanRightNLeft(controller.Roll + RollPerTick);
            yield return new WaitForSeconds(ShakeInterval);
            controller.Roll = controller.Roll - RollPerTickShake;
            controller.Pitch = controller.Pitch - PitchPerTickShake;
            TU.LeanRightNLeft(controller.Roll + RollPerTick);
            TU.LeanForwardNBack(controller.Pitch - PitchShake);
            yield return new WaitForSeconds(ShakeInterval);
            controller.Pitch = controller.Pitch + PitchPerTickShake;
            TU.LeanForwardNBack(controller.Pitch - PitchShake);
        }
    }
}
