#region

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

#endregion

public class RandMats : MonoBehaviour

{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Material _material;
    private Object[] _mats = new Object[10];
    private List<Object> asd = new List<Object>();
    private string[] materialNames = { "helmet_02", "Pants_02", "Pants_03" };
    private Random rnd = new Random();
    private void Start()
    {
        _mats = AssetDatabase.LoadAllAssetsAtPath("Assets/Materials/helmet_02.mat");
        foreach (Object mat in _mats)
        {
            print(mat);
        }
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        while (true)
        {

            GameObject spawned = Instantiate(prefab);
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
