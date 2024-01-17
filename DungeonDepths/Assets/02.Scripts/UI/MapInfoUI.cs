using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInfoUI : MonoBehaviour
{
    public List<Sprite> mapPreviewList = new List<Sprite>();
    Text mapName;
    [SerializeField]
    Text mapDifficulty;
    Image mapImage;
    Image clearImage;
    Map selectedMap;

    public List<Button> buttonList = new List<Button>(); // ��ư ����Ʈ

    void Awake()
    {
        mapName = transform.GetChild(0).GetComponent<Text>();
        mapDifficulty = transform.GetChild(1).GetComponent<Text>();
        mapImage = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        clearImage = transform.GetChild(2).GetChild(1).GetComponent<Image>();
        clearImage.gameObject.SetActive(false);
        for (int i = 0; i < buttonList.Count; i++)
        {
            // ��ư�� �� ������ ����
            int mapIndex = i;
            buttonList[i].onClick.AddListener(() =>
            {
                OnClick(mapIndex);
            });
        }
    }

    private void OnClick(int _mapIndex)
    {
        // �� ���� ����Ʈ���� ���õ� �� ������ ������
        selectedMap = StageManager.Instance.GetMapInfoList()[_mapIndex];

        // ���õ� �� ������ ����Ͽ� �� ������ ǥ��
        string _mapName = selectedMap.mapData.MapName;
        string _difficulty = selectedMap.mapData.Difficulty.ToString();
        mapName.text = "Name : " + _mapName;
        mapDifficulty.text = "Difficulty : " + _difficulty;
        mapImage.sprite = mapPreviewList[_mapIndex];
        if (selectedMap.IsClear) 
            clearImage.gameObject.SetActive(true);
        else
            clearImage.gameObject.SetActive(false);
    }

    public void OnClickSelectBtn()
    {
        if (selectedMap.IsClear) return;
        selectedMap.gameObject.SetActive(true);
        StageManager.Instance.StartStageMap(selectedMap);
    }



}
