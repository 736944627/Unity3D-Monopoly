using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager _Instance;

    public int _round = 1;
    public GameObject[] players;
    private int playerIndex = 0;
    private GameObject thisRoundPlayer;
    public GameObject ThisRoundPlayer
    {
        get { return thisRoundPlayer; }
        set { thisRoundPlayer = value; }
    }

    public BuyBuild buyBuildUI;
    public PayBuild pauBuildUI;


    void Awake()
    {
        _Instance = this;

        
    }

	void Start () {
        
	}



    public void OnRollClick(int number)
    {
        LotNumber(number);
    }

    
    public void OnEndRoundClick()
    {
        //切换至下一个player
        playerIndex++;
        thisRoundPlayer.GetComponent<PlayerMove>().CLoseHeadCamera();
        if (playerIndex>=players.Length)
        {
            //关闭相机
            
            playerIndex = 0;
            _round++;
            thisRoundPlayer = players[0];
        }
        else
        {
            thisRoundPlayer = players[playerIndex];
        }

        thisRoundPlayer.GetComponent<PlayerMove>().ShowHeadCamera();
        PlayerCard._Instance.ShowNextPlayerCard(ThisRoundPlayer.GetComponent<Player>());
    }

    /// <summary>
    /// 摇骰子
    /// </summary>
    void LotNumber(int number)
    {
         
        //通知player前进
        MovePlayer(thisRoundPlayer.GetComponent<PlayerMove>(), number);

    }

    void MovePlayer(PlayerMove player, int number)
    {
        player.SendMessage("Move", number);
    }

    public void AddPlayer()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void ShowBuyBuild(RoadPoint roadpoint)
    {
        buyBuildUI.Show();
    }

    void ShowPayBuild()
    {
        pauBuildUI.Show();
    }

    public void BuyBuild()
    {
        //Debug.Log("BuyBuild");
        //当前路的对应房子
        int count=thisRoundPlayer.GetComponent<Player>().Count;
        //得到当前的路
        MapBlock block=MapManager._Instance.GetRoadByCount(count);
        //Debug.Log("BuyBuild2222");
        if (thisRoundPlayer!=null&&block.Build!=null)
        {
            block.Build.UpgradeBlock(thisRoundPlayer.GetComponent<Player>());
        }
        
    }

    public void PayBuild()
    {
        ShowPayBuild();
    }


}
