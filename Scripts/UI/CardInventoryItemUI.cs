using UnityEngine;
using System.Collections;

public class CardInventoryItemUI : MonoBehaviour {

    private CardInventory cardInventory;

    private UISprite sprite;
    private UILabel label;

	// Use this for initialization
	void Awake () {
        sprite = transform.Find("Sprite").GetComponent<UISprite>();
        label = transform.Find("Label").GetComponent<UILabel>();
        this.GetComponent<UISprite>().width = 100;
	}
	
    public void SetCardInventory(CardInventory cardInventory)
    {
        this.cardInventory = cardInventory;
        Init();

    }

	void Init()
    {
        Debug.Log(cardInventory.Card.Icon);
        sprite.spriteName = cardInventory.Card.Icon;
        //sprite.spriteName = "robber.png";
        label.text = cardInventory.Num.ToString();
    }
}
