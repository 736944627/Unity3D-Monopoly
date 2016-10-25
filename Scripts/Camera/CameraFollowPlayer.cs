using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{

    public Vector3 offset;
    private Transform player;
    public float smoothing = 1;

    private bool isRotating = false;//正在旋转
    private float m_RotateSpeed = 2f;
    public float MaxRotateX = 70f;
    public float MinRotateX = 10f;

    private float distance = 0;
    public float m_ScrollSpeed = 5f;
    public float MaxDistance = 10f;
    public float MinDistance = 3f;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(70, -135, 0));
        transform.LookAt(player);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager._Instance.ThisRoundPlayer != null)
        {
            SetPlayer(GameManager._Instance.ThisRoundPlayer.transform);
            transform.LookAt(player);
        }

        if (player != null)
        {
            Vector3 targetPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
            

            RotateView();
            ScrollView();
        }

    }

    void RotateView()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(1))
            isRotating = false;

        Vector3 orginalPosition = transform.position;
        Quaternion orginalRotation = transform.rotation;

        if (isRotating)
        {
            transform.RotateAround(player.position, player.up, Input.GetAxis("Mouse X") * m_RotateSpeed);
            transform.RotateAround(player.position, -transform.right, Input.GetAxis("Mouse Y") * m_RotateSpeed);
            offset = transform.position - player.position;
        }

        //对旋转范围进行限定
        float x = transform.eulerAngles.x;
        if (x < MinRotateX || x > MaxRotateX)
        {
            transform.position = orginalPosition;
            transform.rotation = orginalRotation;
        }
    
    }

    void ScrollView()
    {
        //Debug.Log (Input.GetAxis("Mouse ScrollWheel"));
        //向前正--向后负
        //向前-->拉近视野
        //向后-->拉远视野
        distance = offset.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * m_ScrollSpeed;

        //		distance = Mathf.Max (distance,MinDistance);
        //		distance = Mathf.Min (distance,MaxDistance);
        distance = Mathf.Clamp(distance, MinDistance, MaxDistance);

        //normalized-->单位向量
        offset = offset.normalized * distance;
    }


    void SetPlayer(Transform player)
    {
        this.player = player;
    }
}
