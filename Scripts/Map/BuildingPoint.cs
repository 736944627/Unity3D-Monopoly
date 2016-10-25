using UnityEngine;
using System.Collections;


public class BuildingPoint : MonoBehaviour
{

    private RoadPoint road;
    public RoadPoint Road
    {
        get { return road; }
        set { road = value; }
    }

    private MapBlock mapblock;
    public MapBlock Mapblock
    {
        get { return mapblock; }
        set { mapblock = value; }
    }
    private int level = 0;//房屋等级
    public int Level
    {
        set { level = value; }
        get { return level; }
    }

    private Player player;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    public int MaxLevel = 1;

    //升级房屋
    public void UpgradeBlock(Player player)
    {
        //Debug.Log(type);
        if (mapblock.Type == BlockType.Build)
        {
            this.player = player;
            gameObject.AddComponent<BuildUpgrade>().UpdateBuild(player);
        }
    }


}
