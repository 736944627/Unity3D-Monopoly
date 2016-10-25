using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BuildUpgrade : MonoBehaviour {

    public int MAXLEVEL = 4;

    public BuildingPoint buildPoint;

    void Awake()
    {
        buildPoint = GetComponent<BuildingPoint>();
    }

    public void UpdateBuild(Player player)
    {
        //已经最高级,不能再升级
        if (buildPoint.Level > MAXLEVEL)
        {
            return;
        }

        if (buildPoint.Level==0)
        {
            buildPoint.Level += 1;
            UpgradeToLevel1(player);
        }
        
        
    }

    void UpgradeToLevel1(Player player)
    {
        //Debug.Log(buildPoint.Mapblock.Rotate);

        //升级
        //实例化三根柱子
        GameObject prefeb = Resources.Load("Prefebs/Building/" + "modularBuildings_100") as GameObject;

        GameObject polo1 = GameObject.Instantiate(prefeb, Vector3.zero, Quaternion.Euler(0, buildPoint.Mapblock.Rotate, 0)) as GameObject;
        polo1.transform.parent = transform;
        polo1.transform.localPosition = new Vector3(-0.036f, -0.5f, -0.03f);

        GameObject polo2 = GameObject.Instantiate(prefeb, Vector3.zero, Quaternion.Euler(new Vector3(0, buildPoint.Mapblock.Rotate + 90, 0))) as GameObject;
        polo2.transform.parent = transform;
        polo2.transform.localPosition = new Vector3(-0.5f, -0.5f, -0.95f);

        polo1.transform.DOMoveY(0.2f, 0.5f);
        polo2.transform.DOMoveY(0.2f, 0.7f);


        //从地下冒出建筑
        GameObject prefeb_build = Resources.Load("Prefebs/Building/" + "modularBuildings_026") as GameObject;
        GameObject build = GameObject.Instantiate(prefeb_build, Vector3.zero, Quaternion.Euler(0, buildPoint.Mapblock.Rotate + 180, 0)) as GameObject;

        build.transform.parent = transform;
        build.transform.localPosition = new Vector3(-1f, -2.5f, -1f);

        build.transform.DOMoveY(0f, 1.2f);

        //从天上掉下屋顶
        GameObject prefeb_loft = Resources.Load("Prefebs/Building/" + "modularBuildings_044") as GameObject;
        GameObject loft = GameObject.Instantiate(prefeb_loft, Vector3.zero, Quaternion.Euler(0, buildPoint.Mapblock.Rotate + 180, 0)) as GameObject;
        loft.transform.parent = transform;
        loft.transform.localPosition = new Vector3(-1f, 10f, -1f);

        //Mesh1_Group1_Model
        GameObject loftmesh = loft.transform.Find("Mesh1_Group1_Model").gameObject;
        loftmesh.GetComponent<Renderer>().materials[1].color = player.Color;

        Tweener tweener_loft=loft.transform.DOMoveY(2.5f, 3f);
        tweener_loft.SetEase(Ease.InCubic);


        StartCoroutine(DestoryGameObject(polo1, 3f));
        StartCoroutine(DestoryGameObject(polo2, 3f));
    }


    IEnumerator DestoryGameObject(GameObject go,float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(go, time);
    }
}
