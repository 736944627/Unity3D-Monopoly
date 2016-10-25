using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    private int _Id;

    public int Id
    {
        get { return _Id; }
        set { _Id = value; }
    }
    private string _Name;

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    private string _Des;

    public string Des
    {
        get { return _Des; }
        set { _Des = value; }
    }
    private string _Icon;

    public string Icon
    {
        get { return _Icon; }
        set { _Icon = value; }
    }

}
