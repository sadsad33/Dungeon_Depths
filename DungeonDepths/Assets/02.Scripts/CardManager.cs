using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;

public class CardManager : MonoSingleton<CardManager>
{
    //public GameObject parent; // ������ �θ�
    [SerializeField]
    CardDatas cardDatas; // ��ũ���ͺ� ������Ʈ
    Transform cardParent;

    public GameObject cardPrefab;
    public GameObject player;
    [SerializeField]
    private List<CardData> normalCardList = new List<CardData>();
    [SerializeField]
    private List<CardData> rareCardList = new List<CardData>();
    [SerializeField]
    private List<GameObject> playerCardList = new List<GameObject>();
    public void Awake()
    {

    }
    private void Start()
    {
        cardParent = UIManager.Instance.WindowList[(int)Window.OPTION].transform.GetChild(5).GetChild(0).transform;
        InitCardData();
    }
    void InitCardData()
    {
        foreach (var _data in cardDatas.cardDataList) // ��ũ���ͺ� ������Ʈ �����͸� ����Ʈ�� �Ű� ���� ��
        {
            if (_data.Rarity == CardRarity.NOMAL)
                normalCardList.Add(_data);
            else if (GameManager.Instance.CurPlayerClass == _data.CardClass || _data.CardClass == Class.NONE)
                rareCardList.Add(_data);
        }
    }
    public CardData NormalCard() // �������� ī��̱�
    {
        var _index = Random.Range(0, normalCardList.Count);
        var _card = normalCardList[_index];
        return _card;
    }
    public CardData RareCard() // ����ī�� ���� �̱�
    {
        var _index = Random.Range(0, rareCardList.Count);
        var _card = rareCardList[_index];
        return _card;
    }
    public CardData RandomCard()
    {
        var _random = Random.Range(0, 10);
        if (_random < 2)
            return RareCard();
        else
            return NormalCard();
    }
    public List<CardData> SelectRandomCards(int _cardNum)
    {
        var _selectedCards = new List<CardData>();
        var _cardData = RandomCard();

        _selectedCards.Add(_cardData);
        while (_selectedCards.Count <= _cardNum)
        {
            _cardData = RandomCard();
            if (!_selectedCards.Contains(_cardData))
                _selectedCards.Add(_cardData);
        }
        return _selectedCards;
    }
    public void GetCard(CardData _card) // ���� ���� ī��
    {
        InstantiateCard(_card);
        if (_card.Rarity == CardRarity.NOMAL)
            normalCardList.Remove(_card);
        else
            rareCardList.Remove(_card);
        // �ɷ� �Լ� ȣ��

        AbilityEffect _abilityEffect = new AbilityEffect();
        _abilityEffect.StatBoostEffect(player.GetComponent<PlayerBase>(), _card);
    }
    public void InstantiateCard(CardData _cardData)
    {
        var _cardObj = Instantiate(cardPrefab, cardParent);
        _cardObj.name = _cardData.CardName;
        var _card = _cardObj.GetComponent<Card>();
        _card.cardData = _cardData;
        playerCardList.Add(_cardObj);
    }
    public void ClearPlayerCardList()
    {
        foreach (var _card in playerCardList)
            Destroy(_card);
        playerCardList.Clear();

    }
}


