using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{

    public static CardManager _Instance;
    public TextAsset CardInfoText;

    //方案1--Hashtable
    protected Hashtable hashtable;
    private int DataRow;

    //方案2--Dict
    private Dictionary<int, Card> dicr_card = new Dictionary<int, Card>();

    void Awake()
    {
        _Instance = this;

       // ReadCardInfo();

        ReadCardInfoText();

        //Debug.Log(GetCardById(2).Name);
    }

    void ReadCardInfoText()
    {
        string[] cardsInfos = CardInfoText.ToString().Split('\r');

        for (int i = 1; i < cardsInfos.Length;i++ )
        {
            string[] cardInfos = cardsInfos[i].Split(',');
            Card card=new Card();
            card.Id = int.Parse(cardInfos[0]);
            card.Name = cardInfos[1];
            card.Des = cardInfos[2];
            card.Icon = cardInfos[3];
            dicr_card.Add(card.Id, card);
        }


        //Debug.Log(dicr_card.Count);
    }

    public Card GetCardById(int id)
    {
        if (dicr_card.ContainsKey(id))
        {
            Card card;
            dicr_card.TryGetValue(id,out card);
            return card;
        }else
             return null;
    }

#region HashTable
    void ReadCardInfo()
    {
        hashtable = new Hashtable();

        string[] lineArray = CardInfoText.text.Split('\r');
        string[][] cardInfoArray = new string[lineArray.Length][];

        //把csv中的数据存在二维数组中
        for (int i = 0; i < lineArray.Length; i++)
        {
            cardInfoArray[i] = lineArray[i].Split(',');
            //Debug.Log(lineArray[i]);
            //Debug.Log(cardInfoArray[i]);
        }

        int nRow = cardInfoArray.Length;
        int nCow = cardInfoArray[0].Length;
        //Debug.Log("nRow:" + nRow);

        for (int i = 1; i < nRow; ++i)
        {

            //Debug.Log(cardInfoArray[i][0]);

            if (cardInfoArray[i][0] == "\n" || cardInfoArray[i][0] == "" || cardInfoArray[i][0] == "\r")//排空
            {
                nRow--;
                continue;
            }

            //ID信息
            string id = cardInfoArray[i][0].Trim();

            for (int j = 1; j < nCow; j++)
            {
                hashtable.Add(cardInfoArray[0][j] + "_" + id, cardInfoArray[i][j]);
            }
        }

        //Debug.Log("nRow:" + nRow);
        //Debug.Log("nCow:" + nCow);
        DataRow = nRow - 1;
    }

    public int GetAllCardType()
    {
        return DataRow;
    }

    public string GetProperty(string name, int id)
    {
        return GetProperty(name, id.ToString());
    }

    public string GetProperty(string name, string id)
    {
        string key = name + "_" + id;

        if (hashtable.ContainsKey(key))
        {
            return hashtable[key].ToString();
        }
        else
            return "NO KEY";
    }
#endregion
    
}
