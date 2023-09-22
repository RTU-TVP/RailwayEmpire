using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UcoveredObj : MonoBehaviour
{
    [SerializeField] private GameObject _unknownObj;
    [SerializeField] private  MemoGameManager _memoGameManager;
    [field: SerializeField] public int ObjId { get; private set; }

    private void OnMouseDown()
    {
        if (_unknownObj.activeSelf && _memoGameManager.canOpen)
        {
            _unknownObj.SetActive(false);
            _memoGameManager.objectOpened(this);
        }
        
    }

    private void ChangeObj(int id, GameObject obj)
    {
        ObjId = id;
        GameObject instObj = Instantiate(obj);
        instObj.transform.parent = this.transform;
    }

    public void Close()
    {
        _unknownObj.SetActive(true);
    }

}
