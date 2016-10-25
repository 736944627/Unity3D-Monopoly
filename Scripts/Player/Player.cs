using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Player : MonoBehaviour
{

    private int gold;
    public int Gold
    {
        set { gold = value; }
        get { return gold; }
    }

    /// <summary>
    /// 在地图中的位置
    /// </summary>
    private int count;
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    private Color color;
    public Color Color
    {
        get { return color; }
        set { color = value; }
    }


    //拥有的卡牌
    private List<CardInventory> cards;

    public List<CardInventory> Cards
    {
        get { return cards; }
        set { cards = value; }
    }
    public void AddCard(Card card)
    {
        //cards.Add(card);

    }



}
