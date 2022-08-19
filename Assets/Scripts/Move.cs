using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    float last_time = 0.0f;//딜레이 마지막 시간저장해서 차이 보기
    private float delay= 0.8f;//딜레이시간

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Check_inside(transform))
        {
            Application.Quit();
        }


        //Check_inside(this.transform);//Grid내부인지 밖인지 체크

        //start_time = Time.time;//시작시간에 Time.time을 넣어서 시간설정

        if (Input.GetKeyDown("d"))//d 버튼을 눌렀을때
        {

            transform.position += Vector3.right;// 왼쪽으로 한칸이동
            if (!Check_inside(this.transform))//Check_inside 함수에서 거짓이 나올경우
            {
                transform.position += Vector3.left;//오른쪽으로 한칸이동
            }

        }
        else if (Input.GetKeyDown("a"))//a 버튼을 눌렀을 때
        {
            transform.position += Vector3.left;// 왼쪽으로 한칸이동
            if (!Check_inside(this.transform))//Check_inside 함수에서 거짓이 나올경우
            {
                transform.position += Vector3.right;//오른쪽으로 한칸이동
            }

        }
        else if (Input.GetKeyDown("w"))
        {
            transform.RotateAround(transform.Find("pivot").position, new Vector3(0, 0, -1), 90);
            if (!Check_inside(transform))
            {
                transform.RotateAround(transform.Find("pivot").position, new Vector3(0, 0, 1), 90);
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))//d 버튼을 눌렀을때
        {

            while (true)
            {
                transform.position += Vector3.down;//밑으로 한칸 내려감
                if (!Check_inside(this.transform))
                {

                    break;
                }

            }

            transform.position += Vector3.up;
            //GameObject.Find("GameManager").GetComponent<Game_Manager>().Play_Drop_Sound();
            Game_Manager.Grid.Insult_Grid(transform);//shape를 Grid안에 넣어줌
            enabled = false;
            


        }

        if (Time.time - last_time >= ((Input.GetKey("s")) ? delay / 10 : delay))//Fall 구현
        {
            transform.position += Vector3.down;//밑으로 한칸 내려감
            GameObject.Find("GameManager").GetComponent<Game_Manager>().Play_Drop_Sound();
            if (!Check_inside(this.transform))//Grid내부인지 밖인지 체크
            {
                transform.position += Vector3.up;
                Game_Manager.Grid.Insult_Grid(transform);//shape를 Grid안에 넣어줌
                enabled = false;
            }
            last_time = Time.time;//함수끝나는 마지막 시간저장
        }
    }





    
    //IEnumerator Fall()//떨어지는 코루틴
    //{
    //    if (transform.position.y > 0)//현재 오브젝트의 y좌표가 0보다 크면
    //    {
    //        transform.position += Vector3.down;//아래로 한칸 떨어짐
    //    }
    //    Check_inside(this.transform);//Grid내부인지 밖인지 체크
    //    yield return new WaitForSeconds(1.0f); // 1초 기다림
    //    StartCoroutine("Fall");// Fall 코루틴 재실행(update에 넣으면 무한대로 떨어짐 start에 선언하여 반복실행되도록)
    //    //Debug.Log(transform.position.y);
    //}

    bool Check_inside(Transform m)//프리팹을 전달받아서 m의 좌표값이 Grid에 포함되어 있는지 체크해주는 함수
    {
        foreach(Transform child in m)//foreach문을 사용하여 mㅇ에서 자식들을 추출
        {
            if (child.tag.Equals("Square"))//자식들 중에서 Square만을 사용
            {
                //Debug.Log(child.position);
                Vector2Int child_position = new Vector2Int(Mathf.RoundToInt(child.position.x),Mathf.RoundToInt(child.position.y));
                //자식들의 xy좌표들을 반올림하여 받아옴 (반올림하는 이유는 이동하면서 소수점이 생기면서 오류날수있음 방지)


                if (child_position.x < 0 || child_position.x > 9)//x좌표 비교해서 내부가 아니라면 
                {
                    return false;//false 리턴
                }
                else if (child_position.y < 0 || child_position.y >22)//y값이 벗어났다면
                {
                    return false;//false리턴
                }
                else if (!Game_Manager.Grid.Check_Grid(transform))
                {
                    return false;//false리턴
                }
            }


        }
        return true;//위의 사항에서 리턴되지않으면 false가 아니므로 true리턴
    }
}
