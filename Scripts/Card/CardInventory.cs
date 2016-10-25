using UnityEngine;
using System.Collections;

public class CardInventory : MonoBehaviour {

    private Card card;

    public Card Card
    {
        get { return card; }
        set { card = value; }
    }

    private int num;

    public int Num
    {
        get { return num; }
        set { num = value; }
    }


}
