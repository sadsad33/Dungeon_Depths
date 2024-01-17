using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "Map Data", menuName = "Scriptable Object/Map Data", order = int.MaxValue)]
public class MapData : ScriptableObject
{
    [SerializeField]
    private string    mapName;          //�� �̸�
    [SerializeField]
    private Vector3   position;         //�� ��ġ
    [SerializeField]
    private MapType   type;             //�� Ÿ��
    [SerializeField]
    private MapTheme  theme;            //���� �� �׸�
    [SerializeField]
    private MapDifficulty difficulty;   //���̵�
    [SerializeField]
    private int totalBoxNum;            //�ʿ� �����ϴ� �� ���� ���� ��
    [SerializeField]
    private int totalMonsterNum;        //�ʿ� ��ȯ�Ǵ� �� ���� ��

    public string MapName
    {
        get => mapName;
        set => mapName = value;
    }
    public Vector3 Position
    {
        get => position;
        set => position = value;
    }
    public MapType Type
    {
        get => type; 
        set => type = value;    
    }
    public MapTheme Theme
    {
        get => theme;
        set => theme = value;
    }
    public MapDifficulty Difficulty
    {
        get => difficulty;
        set => difficulty = value;
    }
    public int TotalBoxNum
    {
        get => totalBoxNum;
        set => totalBoxNum = value;
    }
    public int TotalMonsterNum
    {
        get => totalMonsterNum;
        set => totalMonsterNum = value;
    }
}
