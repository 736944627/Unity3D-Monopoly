using UnityEngine;
using System.Collections;

public class RoadPoint : MonoBehaviour {

    public MapBlock mapBlock;
    public MapBlock MapBlock
    {
        get { return mapBlock; }
        set { mapBlock = value; }
    }


    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }



}
