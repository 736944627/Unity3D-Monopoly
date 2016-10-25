using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public float m_CameraMoveSpeed = 8f;
    public float m_CameraMoveDis = -20f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x >= m_CameraMoveDis&&GameManager._Instance.ThisRoundPlayer==null)
        {
            transform.Translate(new Vector3(0.5f, -0.5f, 0f) * m_CameraMoveSpeed * Time.deltaTime);
        }else
        {
            transform.GetComponent<CameraFollowPlayer>().enabled = true;
            this.enabled = false;
        }
        
	    
	}
}
