using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCard : MonoBehaviour
{

    public static PlayerCard _Instance;
    public GameObject CardInventoryPrefeb;

    private UIGrid grid;
    private List<CardInventory> cardList = new List<CardInventory>();


    void Awake()
    {
        _Instance = this;
        grid = transform.Find("Scroll View/Grid").GetComponent<UIGrid>();

    }

    void Start()
    {
        
        CreateCardInventory();
    }

    void CreateCardInventory()
    {
        //GetCardInventory(GameManager._Instance.ThisRoundPlayer.GetComponent<Player>());

        CardInventory card1 = new CardInventory();
        card1.Card = CardManager._Instance.GetCardById(1);
        card1.Num = 1;

        CardInventory card2 = new CardInventory();
        card2.Card = CardManager._Instance.GetCardById(2);
        card2.Num = 3;

        cardList.Add(card1);
        cardList.Add(card2);

        for (int i = 0; i < cardList.Count; i++)
        {
            GameObject cardItemUI = GameObject.Instantiate(CardInventoryPrefeb);
            cardItemUI.GetComponent<CardInventoryItemUI>().SetCardInventory(cardList[i]);

            //NGUITools.AddChild(this.gameObject, CardInventoryPrefeb);
            grid.AddChild(cardItemUI.transform);

        }
    }

    public void ShowNextPlayerCard(Player player)
    {
        cardList.Clear();


        CardInventory card1 = new CardInventory();
        card1.Card = CardManager._Instance.GetCardById(3);
        card1.Num = 2;

        CardInventory card2 = new CardInventory();
        card2.Card = CardManager._Instance.GetCardById(4);
        card2.Num = 4;

        CardInventory card3 = new CardInventory();
        card3.Card = CardManager._Instance.GetCardById(1);
        card3.Num = 1;

        cardList.Add(card1);
        cardList.Add(card2);
        cardList.Add(card3);


        List<Transform> listChild = grid.GetChildList();
        for (int i = 0; i < listChild.Count;i++ )
        {
            grid.RemoveChild(listChild[i]);
            Destroy(listChild[i].gameObject);
        }

        for (int i = 0; i < cardList.Count; i++)
        {
            GameObject cardItemUI = GameObject.Instantiate(CardInventoryPrefeb);
            cardItemUI.GetComponent<CardInventoryItemUI>().SetCardInventory(cardList[i]);

            //NGUITools.AddChild(this.gameObject, CardInventoryPrefeb);
            grid.AddChild(cardItemUI.transform);

        }
    }

    void GetCardInventory(Player player)
    {
        cardList = player.Cards;
    }
}
