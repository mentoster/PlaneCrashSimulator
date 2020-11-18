using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController[] AC_Left;
    [SerializeField] RuntimeAnimatorController[] AC_Right;
    [SerializeField] Transform[] SpawnPoint;
    [SerializeField] GameObject[] Parts;

    public void Spawn()
    {
        StartCoroutine("Spawner");
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        int index = Random.Range(0, 2);
        GameObject go = Instantiate(Parts[Random.Range(0, Parts.Length)], SpawnPoint[index].position, SpawnPoint[index].rotation);
        go.transform.parent = SpawnPoint[index].transform;
        switch (index)
        {
            case 0:
                go.GetComponent<Animator>().runtimeAnimatorController = AC_Left[Random.Range(0, AC_Left.Length)];
                break;
            case 1:
                go.GetComponent<Animator>().runtimeAnimatorController = AC_Right[Random.Range(0, AC_Right.Length)];
                break;
        }
        StartCoroutine("Spawner");
    }
}
