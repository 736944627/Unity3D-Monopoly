using UnityEngine;
using System.Collections;

public class Touzi : MonoBehaviour
{
    public static Touzi _Instance;

    public int diceCount = 0;
    public int MaxRandom = 30;
    private int RandomCount = 0;

    void Awake()
    {
        _Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    //Method Roll the Dice  
    void RollTheDice()
    {
        //骰子随机显示数字
        InvokeRepeating("RollDiceOneTime", 0.01f, 0.05f);

    }

    void RollDiceOneTime()
    {
        
        RandomCount++;
        int count;
        if (RandomCount<=MaxRandom)
        {
            count = Random.Range(1, 7);

            if (RandomCount==MaxRandom)
            {
                diceCount = count;
            }

            RotateDice(count);

        }
        else
        {
            RandomCount = 0;
            CancelInvoke();
            HideDice();
            Debug.Log(diceCount);
            GameManager._Instance.OnRollClick(diceCount);
        }
 
    }

    void RotateDice(int count)
    {
        switch (count)
        {
            case 1:
                gameObject.transform.localRotation = Quaternion.identity;
                break;

            case 2:
                gameObject.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));
                break;

            case 3:
                gameObject.transform.localRotation = Quaternion.Euler(new Vector3(90, 90, 0));
                break;

            case 4:
                gameObject.transform.localRotation = Quaternion.Euler(new Vector3(90, 180, 0));
                break;

            case 5:
                gameObject.transform.localRotation = Quaternion.Euler(new Vector3(90, 270, 0));
                break;

            case 6:
                gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, -180, 0));
                break;
        }
    }

    public void ShowDice()
    {
        gameObject.SetActive(true);
        RollTheDice();
    }

    public void HideDice()
    {
        gameObject.SetActive(false);
    }

}
