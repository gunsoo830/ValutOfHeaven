using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int defaultIncreasePoolSize = 5;
    public List<GameObject> poolObjectList = new List<GameObject>();
    
    private List<Queue<GameObject>> _objectPool = new List<Queue<GameObject>>();
    private List<List<GameObject>> _usedObjectList = new List<List<GameObject>>();
    private List<GameObject> _objectHolders = new List<GameObject>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this._initObjectPool();
    }

    // Init
    private void _initObjectPool()
    {
        for(int i=0; i<this.poolObjectList.Count; i++)
        {
            this._addSinglePool();
        }
    }
    protected virtual void _addSinglePool()
    {
        int index = this.poolObjectList.Count - 1;
        GameObject holder = new GameObject(this.poolObjectList[index].name);
        holder.transform.SetParent(this.transform);
        this._objectHolders.Add(holder);

        this._objectPool.Add(new Queue<GameObject>());
        this._usedObjectList.Add(new List<GameObject>());

        this._expandPool(this.poolObjectList.Count - 1);
    }
    protected virtual void _resetPoolObject(int targetIndex, GameObject obj)
    {
        obj.transform.SetParent(this._objectHolders[targetIndex].transform);    
        obj.SetActive(false);

    }

    // main
    protected virtual void _expandPool(int targetIndex)
    {
        for(int i=0; i<this.defaultIncreasePoolSize; i++)
        {
            this._objectPool[targetIndex].Enqueue(this._createObject(targetIndex));
        }
    }
    protected virtual GameObject _createObject(int targetIndex)
    {
        GameObject retVal = Instantiate(this.poolObjectList[targetIndex]);
        this._resetPoolObject(targetIndex, retVal);
        return retVal;
    }


    public void addNewPoolObject(GameObject obj)
    {
        this.poolObjectList.Add(obj);
        this._addSinglePool();
    }
    public GameObject getPoolObject(int targetIndex)
    {
        if (this._objectPool[targetIndex].Count <= 0)
            this._expandPool(targetIndex);

        GameObject retVal = this._objectPool[targetIndex].Dequeue();
        this._usedObjectList[targetIndex].Add(retVal);
        return retVal;
    }
    public void returnPoolObject(int targetIndex, GameObject obj)
    {
        this._resetPoolObject(targetIndex, obj);

        int usedindex = this._usedObjectList[targetIndex].FindIndex(ob => ob == obj);
        this._usedObjectList[targetIndex].RemoveAt(usedindex);

        this._objectPool[targetIndex].Enqueue(obj);
    }
}
