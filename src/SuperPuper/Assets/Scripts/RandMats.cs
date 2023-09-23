
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class RandMats : MonoBehaviour

{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Material _material;
    private Object[] _mats = new Object[10];
    private Random rnd = new Random();
    private List<Object> asd = new List<Object>();
    private string[] materialNames = new [] { "helmet_02", "Pants_02", "Pants_03" };
    private void Start()
    {
        _mats = AssetDatabase.LoadAllAssetsAtPath("Assets/Materials/helmet_02.mat");
        foreach (var mat in _mats)
        {
            print(mat);
        }
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        while (true)
        {
            
            var spawned = Instantiate(prefab);
            /*foreach (var x in spawned.GetComponent<Renderer>().materials)
            {
                print(x);
            }*/
            print(spawned.GetComponent<MeshRenderer>().materials[0]);
            spawned.GetComponent<MeshRenderer>().materials[0] = _material;
            print(spawned.GetComponent<MeshRenderer>().materials[0]);
            yield return new WaitForSeconds(1);
        }
    }
}
/*public class RandMats : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Material _material;

    private void Start()
    {
        StartCoroutine(SpawnAndReplaceMaterial());
    }

    private IEnumerator SpawnAndReplaceMaterial()
    {
        while (true)
        {
            GameObject spawned = Instantiate(prefab);
            Renderer renderer = spawned.GetComponent<Renderer>();

            for (var index = 0; index < renderer.materials.Length; index++)
            {
                renderer.materials[index] = _material;
            }

            yield return new WaitForSeconds(1);
        }
    }
}*/