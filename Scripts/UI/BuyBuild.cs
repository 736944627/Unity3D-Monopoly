using UnityEngine;
using System.Collections;

public class BuyBuild : MonoBehaviour {

    //public static BuyBuild _Instance;

    private UILabel label_gold;
    private UIButton btn_buy;
    private UIButton btn_nobuy;
    private UIButton btn_close;

    private TweenPosition tween;

	// Use this for initialization
	void Awake () {
        //_Instance = this;

        label_gold = transform.Find("LabelGold").GetComponent<UILabel>();
        btn_buy = transform.Find("BtnBuy").GetComponent<UIButton>();
        btn_nobuy = transform.Find("BtnNoBuy").GetComponent<UIButton>();
        btn_close = transform.Find("BtnClose").GetComponent<UIButton>();

        tween = GetComponent<TweenPosition>();

        //EventDelegate ed_buy = new EventDelegate(this, "OnBuyClick");
        //btn_buy.onClick.Add(ed_buy);

        EventDelegate ed_nobuy = new EventDelegate(this, "OnNoBuyClick");
        btn_nobuy.onClick.Add(ed_nobuy);

        EventDelegate ed_close = new EventDelegate(this, "OnCloseClick");
        btn_close.onClick.Add(ed_close);
        
	}

    void Start()
    {
        this.gameObject.SetActive(false);
    }
	
    public void Show()
    {
        this.gameObject.SetActive(true);
        tween.PlayForward();
    }

    void Dismiss()
    {
        StartCoroutine(SetWindowEnable());
        tween.PlayReverse();
    }

    IEnumerator SetWindowEnable()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }

    public void OnBuyClick()
    {
        //Debug.Log("OnBuyClick");
        GameManager._Instance.BuyBuild();
        Dismiss();
    }

    void OnNoBuyClick()
    {
        Dismiss();
    }

    void OnCloseClick()
    {
        Dismiss();
    }

}
