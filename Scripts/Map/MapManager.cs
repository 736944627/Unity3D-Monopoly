using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using DG.Tweening;

public class MapManager : MonoBehaviour
{
    public static MapManager _Instance;

    public string mapXmlPath = "Text/map1.xml";
    public GameObject roadPointPrefeb;//路径点预制品
    public GameObject playerPrefeb;

    private List<MapBlock> blocksList = new List<MapBlock>();
    private List<BuildingPoint> buildList = new List<BuildingPoint>();
    public List<MapBlock> roadBuildList = new List<MapBlock>();
    //private List<RoadPoint> roadpointList = new List<RoadPoint>();//存放路径点

    public bool isXMLReadComplete = false;
    private bool isMapComplete = false;
    private bool isPlayerSetupOver = false;

    //Row列 Column行
    public int Row = 14;
    public int Colums = 15;
    public int PlayerNum = 2;


    private GameObject newroadPoint;
    public Color[] playerColors;
    public int[] playerPos;


    void Awake()
    {
        _Instance = this;

#if UNITY_ANDROID
        //StartCoroutine(LoadXmlAndroid());
        LoadXmlAndroid();
#endif

#if UNITY_STANDALONE_WIN
        LoadXml();
#endif

        //GameObject go = null;
        //GameObject prefeb = null;
        //prefeb = Resources.Load("Prefebs/Road/" + "roadTile_" + string.Format("{0:000}", 1)) as GameObject;
        //go = GameObject.Instantiate(prefeb, new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    void Start()
    {
        GetBuildingInMap();
    }

    public void FallFirstBlock()
    {
        blocksList[0].FallBlock();
    }

    void PaseXml(XmlDocument xml)
    {
        //得到objects节点下的所有子节点
        XmlNodeList xmlNodeList = xml.SelectSingleNode("map/Enenty").ChildNodes;
        //遍历所有子节点
        foreach (XmlElement xl1 in xmlNodeList)
        {
            foreach (XmlElement xl2 in xl1.ChildNodes)
            {
                string type = (xl2.GetAttribute("type") + xl2.InnerText);
                BlockType blocktype = BlockType.Null;
                switch (type)
                {
                    case "Road":
                        blocktype = BlockType.Road;

                        //PlayerPanel._Instance.SetText(type);
                        break;
                    case "Build":
                        blocktype = BlockType.Build;
                        break;
                    case "Normal":

                        blocktype = BlockType.Normal;
                        break;
                    default:
                        blocktype = BlockType.Null;
                        break;
                }


                int id = int.Parse(xl2.GetAttribute("id") + xl2.InnerText);
                int x = int.Parse(xl2.GetAttribute("x") + xl2.InnerText);
                int y = int.Parse(xl2.GetAttribute("y") + xl2.InnerText);
                int rotate = int.Parse(xl2.GetAttribute("rotate") + xl2.InnerText);

                //x += 30;
                //y += 30;

                int height = 30;

                GameObject go = null;
                GameObject prefeb = null;


                if (blocktype == BlockType.Road)
                {
                    prefeb = Resources.Load("Prefebs/Road/" + "roadTile_" + string.Format("{0:000}", id)) as GameObject;

                    newroadPoint = GameObject.Instantiate(roadPointPrefeb, new Vector3(x - 1.5f, 0.25f, y - 1.5f), Quaternion.identity) as GameObject;
                    newroadPoint.transform.parent = GameObject.Find("RoadPoints").transform;

                    //roadpointList.Add(newroadPoint.GetComponent<RoadPoint>());
                }
                else if (blocktype == BlockType.Build)
                {
                    prefeb = Resources.Load("Prefebs/Building/" + "modularBuildings_" + string.Format("{0:000}", id)) as GameObject;

                }
                else if (blocktype == BlockType.Normal)
                {
                    prefeb = Resources.Load("Prefebs/Road/" + "roadTile_" + string.Format("{0:000}", id)) as GameObject;

                }


                if (rotate == 0)
                {
                    go = GameObject.Instantiate(prefeb, new Vector3(x, height, y), Quaternion.identity) as GameObject;
                }
                else if (rotate == 90)
                {
                    go = GameObject.Instantiate(prefeb, new Vector3(x, height, y - 3), Quaternion.Euler(0, rotate, 0)) as GameObject;
                }
                else if (rotate == 180)
                {
                    go = GameObject.Instantiate(prefeb, new Vector3(x - 3, height, y - 3), Quaternion.Euler(0, rotate, 0)) as GameObject;
                }
                else if (rotate == 270)
                {
                    go = GameObject.Instantiate(prefeb, new Vector3(x - 3, height, y), Quaternion.Euler(0, rotate, 0)) as GameObject;
                }

                if (blocktype == BlockType.Road)
                {
                    go.gameObject.name = "Road-" + x / 3 + "-" + y / 3;
                }
                else if (blocktype == BlockType.Build)
                {
                    go.gameObject.name = "Building-" + x / 3 + "-" + y / 3;
                }
                else if (blocktype == BlockType.Normal)
                {
                    go.gameObject.name = "Normal-" + x / 3 + "-" + y / 3;
                }



                go.transform.parent = GameObject.Find("Terrain").transform;


                MapBlock block = go.GetComponent<MapBlock>();
                if (newroadPoint != null)
                {
                    newroadPoint.GetComponent<RoadPoint>().MapBlock = block;
                }
                newroadPoint = null;
                //Debug.Log(rotate);
                block.InitBlock(blocktype, id, x, y, blocksList.Count, rotate);

                blocksList.Add(block);
            }
        }

        isXMLReadComplete = true;
        
    }

#if UNITY_STANDALONE_WIN
    //读取地图配置
    void LoadXml()
    {
        XmlDocument xml = new XmlDocument();
        XmlReaderSettings set = new XmlReaderSettings();
        set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取

        //Resources.Load(mapXmlPath)


        //yield return new WaitForSeconds(0.01f);
        xml.Load(XmlReader.Create("Assets/Resources/" + mapXmlPath, set));
        PaseXml(xml);


    }
#endif

#if UNITY_ANDROID
    void LoadXmlAndroid()
    {
        XmlDocument xml = new XmlDocument();
        XmlReaderSettings set = new XmlReaderSettings();
        set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取
        string path=Application.dataPath + "/Resources/" + mapXmlPath;
        xml.Load(XmlReader.Create(path, set));
        PaseXml(xml);

    }

#endif

    void GetBuildingInMap()
    {
        for (int i = 0; i < blocksList.Count; i++)
        {
            if (blocksList[i].Type == BlockType.Build)
            {
                blocksList[i].gameObject.AddComponent<BuildingPoint>();
                blocksList[i].gameObject.GetComponent<BuildingPoint>().Mapblock = blocksList[i];
                buildList.Add(blocksList[i].GetComponent<BuildingPoint>());
            }
        }

        SetBuildRoad();
    }

    /// <summary>
    /// 设置建筑物对应的路
    /// </summary>
    void SetBuildRoad()
    {
        for (int i = 0; i < buildList.Count; i++)
        {
            //Debug.Log(buildList[i].Mapblock.Rotate);
            int count = 0;
            if (buildList[i].Mapblock.Rotate == 270)
            {
                //获得左侧的Road
                count = buildList[i].Mapblock.Count - Row - 2;
                // Debug.Log(blocksList[count].gameObject.transform.localPosition);

            }
            else if (buildList[i].Mapblock.Rotate == 90)
            {
                count = buildList[i].Mapblock.Count + Row + 2;
                //roadBuildList.Add(blocksList[count]);
            }
            else if (buildList[i].Mapblock.Rotate == 0)
            {
                count = buildList[i].Mapblock.Count - 1;
                //roadBuildList.Add(blocksList[count]);
            }
            else if (buildList[i].Mapblock.Rotate == 180)
            {
                count = buildList[i].Mapblock.Count + 1;
                //roadBuildList.Add(blocksList[count]);
            }

            blocksList[count].RoadType = RoadType.CanBuild;
            blocksList[count].Build = buildList[i];
            //Debug.Log(blocksList[count].Build.Mapblock.Count);
            roadBuildList.Add(blocksList[count]);
        }
    }

    public void FallBlockOneByOne(int i)
    {

        if (i < blocksList.Count && blocksList[i].IsSet == false)
        {
            StartCoroutine(FallBlock(i));
            //Debug.Log(i);
            if (i == blocksList.Count - 1)
            {
                //Debug.Log("isMapComplete");
                isMapComplete = true;
            }
        }

    }

    IEnumerator FallBlock(int i)
    {
        yield return new WaitForSeconds(0.1f);
        blocksList[i].FallBlock();
    }


    void Update()
    {
        if (isMapComplete)
        {
            SetUpPlayer();
        }
    }


    void SetUpPlayer()
    {
        if (!isPlayerSetupOver)
        {
            for (int i = 0; i < PlayerNum; i++)
            {
                GameObject player = CreatePlayerWithPos(playerPos[i]);
                player.name = "Player" + (i + 1);
                player.GetComponent<Player>().Color = playerColors[i];
                //改变对应颜色
                //player.GetComponent<Renderer>().material.color = player.GetComponent<Player>().Color;
            }

            isPlayerSetupOver = true;
            GameManager._Instance.AddPlayer();
            GameManager._Instance.ThisRoundPlayer = GameManager._Instance.players[0];
            //现在玩家开启相机


            PlayerPanel._Instance.ShowUI();
            PlayerPanel._Instance.SetFrameColor(GameManager._Instance.ThisRoundPlayer.GetComponent<Player>().Color);

        }
    }

    public MapBlock GetRoadByCount(int count)
    {
        return blocksList[count];
    }

    GameObject CreatePlayerWithPos(int count)
    {
        RoadPoint road0 = null;
        RoadManager._Instance.dict_Road.TryGetValue(count, out road0);
        Vector3 pos = Vector3.zero;
        if (road0 != null)
        {
            pos = new Vector3(road0.gameObject.transform.position.x,//
            road0.gameObject.transform.position.y + 10,//
            road0.gameObject.transform.position.z);
        }

        //生成玩家
        GameObject player = GameObject.Instantiate(playerPrefeb, pos, Quaternion.identity) as GameObject;
        player.transform.DOMoveY(0.5f, 2f).SetEase(Ease.InCubic);
        player.GetComponent<PlayerMove>().FootRoad = road0;
        player.GetComponent<Player>().Count = road0.MapBlock.Count;


        return player;
    }

    

}
