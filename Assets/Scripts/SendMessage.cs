using ChairControl.ChairWork;
using ChairControl.ChairWork.Options;
using System.Collections;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    FutuRiftController controller;
    float _pitch = 0;
    float _roll = 0;
    [SerializeField] PlaneController TU;
    [Space(10)]
    [Header("Connection settings")]
    [SerializeField] bool UseUDP = false;
    [SerializeField] UdpOptions UdpOpt;
    [SerializeField] int Com = 6;
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
    [Space(10)]
    [Header("Animated parts")]
    [SerializeField] GameObject LeftTurbine;
    [SerializeField] GameObject RightTurbine;
    [SerializeField] Animator RightBaggage;
    [SerializeField] Animator LeftBaggage;
    [SerializeField] GameObject Tale;
    [SerializeField] GameObject LeftChairs;
    [SerializeField] GameObject LeftChair;
    [SerializeField] GameObject RightChair;
    [SerializeField] SoundController Sounds;
    [SerializeField] PartSpawner Spawner;
    [Space(10)]
    [Header("Fire and Smoke")]
    [SerializeField] GameObject FireRight;
    [SerializeField] GameObject FireLeft;
    [SerializeField] GameObject SmokeRight;
    [SerializeField] GameObject SmokeLeft;
    [Space(10)]
    [Header("Baggage")]
    [SerializeField] Animator OnChair;
    [SerializeField] Animator OnFloor;
    [SerializeField] Animator OnFrontChair;
    [SerializeField] Animator OnFrontFloor;

    void Start()
    {
        if (UseUDP)
        {
            controller = new FutuRiftController(UdpOpt)
            {
                Pitch = _pitch,
                Roll = _roll
            };
        }
        else
        {
            controller = new FutuRiftController(new ComPortOptions { ComPort = Com })
            {
                Pitch = _pitch,
                Roll = _roll
            };
        }
    }

    public void Activate()
    {
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
        Sounds.PlaySeatBelt();
        StartCoroutine("Tourbine_L");
        StartCoroutine("Pitch");
        StartCoroutine("Roll");
        StartCoroutine("Shake");
        Spawner.Spawn();
        yield return new WaitForSeconds(4f);
        OnChair.enabled = true;
        yield return new WaitForSeconds(2.5f);
        OnFloor.enabled = true;
        yield return new WaitForSeconds(2.3f);
        OnFrontChair.enabled = true;
    }

    IEnumerator Pitch()
    {
        controller.Pitch = controller.Pitch - PitchShake;
        TU.LeanForwardNBack(controller.Pitch - PitchShake);
        yield return new WaitForSeconds(ShakeTick);
        controller.Pitch = controller.Pitch + PitchShake;
        TU.LeanForwardNBack(controller.Pitch - PitchShake);
        while (_pitch < 21)
        {
            controller.Pitch = controller.Pitch + PitchPerTick;
            TU.LeanForwardNBack(controller.Pitch - PitchShake);
            _pitch = controller.Pitch;
            yield return new WaitForSeconds(PitchTick);
        }
    }

    IEnumerator Roll()
    {
        bool check = true;
        while (_roll < 18)
        {
            controller.Roll = controller.Roll + RollPerTick;
            TU.LeanRightNLeft(controller.Roll + RollPerTick);
            _roll = controller.Roll;
            yield return new WaitForSeconds(RollLeftTick);
        }
        StartCoroutine("Tourbine_R");
        while (_roll > -18)
        {
            if (_roll < 0 && check)
            {
                Tale.GetComponent<Animator>().enabled = true;
                Tale.GetComponent<DestroyAfterSomeTime>().enabled = true;
                yield return new WaitForSeconds(1);
                LeftChairs.GetComponent<Animator>().enabled = true;
                LeftChair.GetComponent<DestroyAfterSomeTime>().enabled = true;
                RightChair.GetComponent<Animator>().enabled = true;
                RightChair.GetComponent<DestroyAfterSomeTime>().enabled = true;
                controller.Roll = controller.Roll - RollPerTick * 2;
                TU.LeanRightNLeft(controller.Roll + RollPerTick * 2);
                LeftBaggage.enabled = true;
                check = false;
            }
            controller.Roll = controller.Roll - RollPerTick;
            TU.LeanRightNLeft(controller.Roll + RollPerTick);
            _roll = controller.Roll;
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

    IEnumerator Tourbine_L()
    {
        SmokeLeft.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        FireLeft.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LeftTurbine.GetComponent<Animator>().enabled = true;
        LeftTurbine.GetComponent<DestroyAfterSomeTime>().enabled = true;
        RightBaggage.enabled = true;
    }

    IEnumerator Tourbine_R()
    {
        OnFrontFloor.enabled = true;
        SmokeRight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        FireRight.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        RightTurbine.GetComponent<Animator>().enabled = true;
        RightTurbine.GetComponent<DestroyAfterSomeTime>().enabled = true;
    }
}
