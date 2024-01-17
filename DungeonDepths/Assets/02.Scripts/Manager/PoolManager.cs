using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData // ���߿� ���ҽ��Ŵ����� �������� ��������
{
    public string name;
    public GameObject prefab; // ���߿� ���ҽ��Ŵ����� �������� ��������
    public int count;   // �̸������� ������Ʈ ��
    public Transform parent;    // ��Ƶ� ������Ʈ �θ���
}
public class PoolManager : MonoSingleton<PoolManager>
{

    void Awake()
    {
        Init();
    }

    public List<ObjectData> objList = new List<ObjectData>();
    public bool willGrow = true; // �̸� ������ ������Ʈ�� ���ڸ���� ����� ����

    public Dictionary<string, List<GameObject>> poolList = new Dictionary<string, List<GameObject>>();
    void Init() // ���ҽ��Ŵ����� �������� ������ ������ Ǯ ��ųʸ��� ��� ����
    {
        GameObject _go, _obj;
        List<GameObject> _list;
        int _count;
        Transform _parent;
        for (int j = 0, jmax = objList.Count; j < jmax; j++) 
        {
            _count = objList[j].count;  // � �̸� ��������
            _obj = objList[j].prefab;   // ���߿� ���ҽ��������� ��������.
            _parent = objList[j].parent;    // ������Ʈ Ǯ���� �θ� �������Ʈ ����
            _list = new List<GameObject>();
            poolList.Add(_obj.name, _list);

            for (int i = 0; i < _count; i++)
            {
                _go = Instantiate(_obj) as GameObject;
                _go.transform.SetParent(_parent);
                _go.SetActive(false);
                _list.Add(_go);
            }
        }
    }

    public GameObject Instantiate(string _name, Vector3 _pos, Quaternion _rot)
    {
        GameObject createObject = Instantiate(_name);
        createObject.transform.position = _pos;
        createObject.transform.rotation = _rot;

        return createObject;

    }

    GameObject Instantiate(string _name)
    {
        if (!poolList.ContainsKey(_name))
        {
            Debug.LogError("Not Found Pooling GameObject name" + _name);
            return null;
        }

        GameObject _rtn = null;
        bool _bFind = false;
        List<GameObject> _list = poolList[_name];
        for (int i = 0; i < _list.Count; i++)
        {
            //gameobject ���Ҽ�ȭ -> ���ݻ�������� �ʴٴ� �ǹ�...
            if (!_list[i].activeInHierarchy)
            {
                _rtn = _list[i];
                _bFind = true;
                _rtn.SetActive(true);
                break;
            }
        }

        //not found > create
        if (!_bFind && willGrow) // ���� �ִ� ī��Ʈ���� �� �ʿ��Ѱ�� ���λ����ϱ� ���� ���ǹ�
        {
            ObjectData _obj = GetObject(_name);
            GameObject _go = Instantiate(_obj.prefab) as GameObject;
            _go.transform.SetParent(_obj.parent);
            _go.SetActive(true);
            _list.Add(_go);

            _rtn = _go;
        }

        return _rtn;
    }

    ObjectData GetObject(string _name) // ���� ������ ������Ʈ�� �̸����������� ����Ǿ� �ִ��� �˻�
    {
        ObjectData _obj = null;
        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i].name == _name)
            {
                _obj = objList[i];
                break;
            }
        }
        return _obj;
    }
}