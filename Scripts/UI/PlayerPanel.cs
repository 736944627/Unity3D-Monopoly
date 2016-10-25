using UnityEngine;
using System.Collections;

public class PlayerPanel : MonoBehaviour {

    public static PlayerPanel _Instance;

    private UIButton btn_Roll;
    private UIButton btn_EndBtn;
    private UILabel btnRoll_label;
    private UITexture texture_head;
    private UISprite sprite_frame;

    void Awake()
    {
        _Instance = this;

        btn_Roll = transform.Find("BtnRoll").GetComponent<UIButton>();
        btn_Roll.gameObject.SetActive(false);

        btn_EndBtn = transform.Find("BtnEndRound").GetComponent<UIButton>();
        btn_EndBtn.gameObject.SetActive(false);

        texture_head = transform.Find("HeadTexture").GetComponent<UITexture>();
        texture_head.gameObject.SetActive(false);

        sprite_frame=texture_head.transform.Find("Head").GetComponent<UISprite>();

        btnRoll_label = btn_Roll.transform.Find("Label").GetComponent<UILabel>();
        //TextAsset text = Resources.Load("Text/map1", typeof(TextAsset)) as TextAsset;
        //string s=text.text;
        //string[] s2 = s.Split('\n');
        //Debug.Log(s);
        //btnRoll_label.text = s2[0];

        
    }

    void Start()
    {
        
    }

	public void OnRollClick()
    {
        btn_Roll.gameObject.SetActive(false);
        
        Touzi._Instance.ShowDice();


        btn_EndBtn.gameObject.SetActive(true);

        
    }

    public void OnEndRoundClick()
    {
        btn_EndBtn.gameObject.SetActive(false);
        btn_Roll.gameObject.SetActive(true);

        GameManager._Instance.OnEndRoundClick();
        sprite_frame.color = GameManager._Instance.ThisRoundPlayer.GetComponent<Player>().Color;
    }

    public void SetText(string s)
    {
        btnRoll_label.text = s;
    }

    public void ShowUI()
    {
        texture_head.gameObject.SetActive(true);
        btn_Roll.gameObject.SetActive(true);
   
    }

    public void SetFrameColor(Color color)
    {
        sprite_frame.color = color;
    }
}
