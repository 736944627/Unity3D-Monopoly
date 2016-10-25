using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    private Animator animator;

    private string boolIsMove = "isMove";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdleAnim()
    {
        animator.SetBool(boolIsMove,false);
        
    }

    public void PlayWalkAnim()
    {
        animator.SetBool(boolIsMove, true);
    }
}
