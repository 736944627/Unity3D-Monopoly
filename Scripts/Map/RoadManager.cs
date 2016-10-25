using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour
{
    public static RoadManager _Instance;

    private int roadCount;

    public int RoadCount
    {
        get { return roadCount; }
       
    }

    private List<RoadPoint> list_Road = new List<RoadPoint>();
    public Dictionary<int, RoadPoint> dict_Road = new Dictionary<int, RoadPoint>();

    private List<RoadPoint> list_restRoad = new List<RoadPoint>();
    public List<RoadPoint> list_lastRoad = new List<RoadPoint>();

    private bool isInit = false;
    void Awake()
    {
        _Instance = this;
    }

    void Start()
    {
        RoadPoint[] roads = this.GetComponentsInChildren<RoadPoint>();
        
        for (int i = 0; i < roads.Length; i++)
        {
            //roads[i].Num = i;
            list_Road.Add(roads[i]);
            list_restRoad.Add(roads[i]);
        }
        roadCount = list_Road.Count;


    }

    void Update()
    {
        InitRoad();
    }

    void InitRoad()
    {
        if (MapManager._Instance.isXMLReadComplete&&isInit==false)
        {
            isInit = true;
            CreatePath();
            //PlayerPanel._Instance.SetText("ReadXml Success");
            MapManager._Instance.FallFirstBlock();
        }
    }

    /// <summary>
    /// 创建封闭路径
    /// </summary>
    void CreatePath()
    {
        for (int i = 0; i < 2; i++)
        {
            list_Road[i].Id = i;
            list_Road[i].name = "RoadPoint" + list_Road[i].Id;

            dict_Road.Add(list_Road[i].Id, list_Road[i]);
            list_restRoad.Remove(list_Road[i]);
            list_lastRoad.Add(list_Road[i]);
        }

        RoadPoint tempRoad = list_Road[1];
        int temp = 1;

        while (temp != list_Road.Count-1)
        {
            tempRoad = CompileToRelistRoad(tempRoad);
            if (tempRoad != null)
            {
                temp++;
                tempRoad.Id = temp;
                tempRoad.name = "RoadPoint" + tempRoad.Id;

                dict_Road.Add(tempRoad.Id, tempRoad);
                list_restRoad.Remove(tempRoad);
                list_lastRoad.Add(tempRoad);
            }

        }

    }

    bool ISRoadBehind(RoadPoint thisroad, RoadPoint otherroad)
    {
        bool isBehind = false;
        if (thisroad.transform.position.z == otherroad.transform.position.z)
        {
            float x = Mathf.Abs(thisroad.transform.position.x - otherroad.transform.position.x);
            if (x == 3.0f)
            {
                isBehind = true;
            }

        }
        else if (thisroad.transform.position.x == otherroad.transform.position.x)
        {
            float z = Mathf.Abs(thisroad.transform.position.z - otherroad.transform.position.z);
            if (z == 3.0f)
            {
                isBehind = true;
            }
        }



        return isBehind;
    }

    /// <summary>
    /// 获得与此路相邻的路
    /// </summary>
    /// <param name="road"></param>
    /// <returns></returns>
    RoadPoint CompileToRelistRoad(RoadPoint road)
    {
        for (int i = 0; i < list_restRoad.Count; i++)
        {
            if (ISRoadBehind(road, list_restRoad[i]))
            {

                return list_restRoad[i];
            }
        }


        return null;
    }


    public RoadPoint GetRoadById(int id)
    {
        RoadPoint road = null;
        dict_Road.TryGetValue(id, out road);
        return road;
    }
}
