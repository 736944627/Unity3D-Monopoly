using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

    //脚下的
    private RoadPoint footRoad;
    public RoadPoint FootRoad
    {
        get { return footRoad; }
        set { footRoad = value; }
    }

    private bool isMoveOver = true;
    private bool isMoveing = false;

    public float m_MoveSpeed = 3f;
    private Vector3 nowPos;
    private Vector3 targetPos;
    private int step = 0;

    private PlayerAnim playerAnim;
    public Camera headCamera;

    void Start()
    {
        playerAnim = GetComponent<PlayerAnim>();
        //characterController = GetComponent<CharacterController>();

        int footId = footRoad.Id;
        targetPos = RoadManager._Instance.GetRoadById(footId+1).transform.position + new Vector3(0, 0.5f, 0);
        FaceToPos(targetPos);

        headCamera=transform.Find("HeadCamera").GetComponent<Camera>();
        //headCamera.gameObject.SetActive(false);

        if(this.gameObject==GameManager._Instance.ThisRoundPlayer)
        {
            ShowHeadCamera();
        }
        else
        {
            CLoseHeadCamera();
        }
    }

    public void CLoseHeadCamera()
    {
        headCamera.gameObject.SetActive(false);
    }

    public void ShowHeadCamera()
    {
        headCamera.gameObject.SetActive(true);
    }
 
    //移动到下一块 默认走一步
    public void Move(int step = 1)
    {
        //Debug.Log("step" + step);
        this.step = step;
        //开始移动
        isMoveOver = false;

        MoveOnStep();

    }

    //移动到下一个格子
    void MoveOnStep()
    {
        this.step -= 1;

        //得到脚下的ID
        int footId = footRoad.Id;
        //得到总路径
        int MAXROAD = RoadManager._Instance.RoadCount - 1;

        //移动到footid+1
        if (footId == MAXROAD)
        {
            //移动到 id=0的格子
            MoveToRoad(0);
        }
        else
        {
            //移动到 下一格
            MoveToRoad(footId + 1);
        }

    }

    void MoveToRoad(int id)
    {

        footRoad = RoadManager._Instance.GetRoadById(id);
        //目标位置
        targetPos = footRoad.transform.position + new Vector3(0, 0.5f, 0);
        FaceToPos(targetPos);
        isMoveing = true;
        gameObject.GetComponent<Player>().Count = footRoad.MapBlock.Count;


    }

    void MoveToPos(Vector3 targetpos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetpos, m_MoveSpeed * Time.deltaTime);
        if (Vector3.Distance(targetPos, transform.position) < 0.1f)
        {
            isMoveing = false;
            transform.position = targetpos;
            if (step == 0)
            {
                //没有步数了
                isMoveing = false;
                isMoveOver = true;
                ShowBuyBuild();
            }
            else
            {
                MoveOnStep();
            }

        }
    }

    void ShowBuyBuild()
    {
        if (footRoad.MapBlock.RoadType == RoadType.CanBuild && footRoad.MapBlock.Build.Level == 0)
        {
            //显示购买
            GameManager._Instance.ShowBuyBuild(footRoad);
        }
        else if (footRoad.MapBlock.RoadType == RoadType.CanBuild &&//
            footRoad.MapBlock.Build.Player == this.gameObject.GetComponent<Player>() &&//
            footRoad.MapBlock.Build.Level < footRoad.MapBlock.Build.MaxLevel)
        {
            //显示升级
            Debug.Log("升级房子");
        }
        else if (footRoad.MapBlock.RoadType == RoadType.CanBuild && footRoad.MapBlock.Build.Player != this.gameObject.GetComponent<Player>())
        {
            Debug.Log("别人的房子");
            GameManager._Instance.PayBuild();
        }

        //没人买,等级为0级时
        //走上别人买过的时候
        //走上自己的房子时候
    }

    void Update()
    {

        if (isMoveing)
        {
            //Debug.Log("targetpos" + targetPos);
            MoveToPos(targetPos);
            playerAnim.PlayWalkAnim();
        }
        else
        {
            playerAnim.PlayIdleAnim();
        }


    }

    void FaceToPos(Vector3 targetPos)
    {
        //判断格子在player的什么位置
        //仅仅判断x,z
        float x = targetPos.x - transform.position.x;
        float z = targetPos.z - transform.position.z;

        if (x > 0)
        {
            //朝下
            //Debug.Log("Down");
            //判断是否需要转动

            transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
            //if (88 < transform.rotation.eulerAngles.y || transform.rotation.eulerAngles.y < 92)
            //    transform.Rotate(new Vector3(0, 90, 0));
        }

        if (x < 0)
        {
            //朝上
            //Debug.Log("Up");
            //if (-92 < transform.rotation.eulerAngles.y || transform.rotation.eulerAngles.y < -88)
            //    transform.Rotate(new Vector3(0, -90, 0));

            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }

        if (z > 0)
        {
            //朝右
            //Debug.Log("Right");
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (z < 0)
        {
            //朝左
            //Debug.Log("Left");
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

}
