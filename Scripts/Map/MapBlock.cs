using UnityEngine;
using System.Collections;
using DG.Tweening;

public enum BlockType
{
    Road,
    Build,
    Normal,
    Null
}

public enum RoadType
{
    Normal,
    CanBuild

}

public class MapBlock : MonoBehaviour
{

    private BlockType type;
    private int id;
    private int x;
    private int y;
    public int count;//在地图中的编号
    private int rotate;
    private bool isSet = false;

    //特殊Road相关
    private RoadType roadType;
    public RoadType RoadType
    {
        get { return roadType; }
        set { roadType = value; }
    }
    //如果改道路可以建造道路的话
    private BuildingPoint build;
    public BuildingPoint Build
    {
        get { return build; }
        set { build = value; }
    }

    #region setget
    public BlockType Type
    {
        set { type = value; }
        get { return type; }
    }

    public int Id
    {
        set { id = value; }
        get { return id; }
    }

    public int X
    {
        set { x = value; }
        get { return x; }
    }

    public int Y
    {
        set { y = value; }
        get { return y; }
    }

    public int Count
    {
        set { count = value; }
        get { return count; }
    }

    public bool IsSet
    {
        set { isSet = value; }
        get { return isSet; }
    }

  
    public int Rotate
    {
        set { rotate = value; }
        get { return rotate; }
    }
    #endregion


    void Start()
    {

    }

    public void InitBlock(BlockType blocktype, int id, int x, int y, int count, int rotate)
    {

        SetBlock(blocktype, id, x, y, count, rotate);
    }

    void SetBlock(BlockType blocktype, int id, int x, int y, int count,int rotate)
    {
        //Debug.Log(blocktype);
        this.Type = blocktype;
        this.Id = id;
        this.X = x;
        this.Y = y;
        this.Count = count;
        this.Rotate = rotate;

        if (type==BlockType.Road)
        {
            roadType = RoadType.Normal;
        }
    }

    public void FallBlock()
    {
        isSet = true;

        Tweener tweener = transform.DOMoveY(0, 0.1f);
        tweener.SetEase(Ease.InCubic);

        tweener.OnComplete(FallComplete);

    }

    void FallComplete()
    {

        int Row = MapManager._Instance.Row+2;
        int rows = Count / Row;
        // Debug.Log(rows);
        MapManager._Instance.FallBlockOneByOne(Count + 1);
        MapManager._Instance.FallBlockOneByOne((rows + 1) * Row);
    }
}
