using UnityEngine;
using System.Collections;

public class PayBuild : MonoBehaviour {

    private TweenPosition tween;
    private UIButton btnOk;
    private UILabel labelGold;


	// Use this for initialization
	void Awake () {
        tween = GetComponent<TweenPosition>();
        btnOk = transform.Find("BtnOk").GetComponent<UIButton>();
        labelGold = transform.Find("LabelGold").GetComponent<UILabel>();

        

        EventDelegate ed = new EventDelegate(this, "OnOkClick");
        btnOk.onClick.Add(ed);

        gameObject.SetActive(false);

	}
	
    public void Show()
    {
        gameObject.SetActive(true);
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

    void OnOkClick()
    {
        Dismiss();
    }
}
