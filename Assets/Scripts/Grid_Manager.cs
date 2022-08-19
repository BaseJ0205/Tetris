using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Manager : MonoBehaviour
{
    public Transform Spawn_Position;
    public static Transform[,] Grid = new Transform[10, 23];


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Init_Grid()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 23; j++)
            {
                Grid[i, j] = null;
            }
        }
        //foreach (Transform t in Game_Manager.Spawn_Position)
        //{
        //    if (t.childCount <= 1)
        //    {
        //        Destroy(t.gameObject);
        //    }
        //}
    }

    public void Insult_Grid(Transform m)//그리드안에 transform삽입
    {
        foreach (Transform child in m)//자식들 받아옴
        {
            if (child.gameObject.tag.Equals("Square"))//스퀘어 찾음
            {
                Vector2Int child_position = new Vector2Int(Mathf.RoundToInt(child.position.x), Mathf.RoundToInt(child.position.y));//자식들 좌표 반올림
                Grid[child_position.x, child_position.y] = child;
            }
            else if (child.gameObject.tag.Equals("Pivot"))
            {
                Destroy(child.gameObject);
            }
            if (m.transform.childCount == 0)
            {
                Destroy(m.gameObject);
            }
        }
        StartCoroutine(Del_Rows()); 
        



    }


    public bool Check_Grid(Transform m)
    {
        foreach (Transform child in m)
        {
            if (child.gameObject.tag.Equals("Square"))
            {
                Vector2Int child_position = new Vector2Int(Mathf.RoundToInt(child.position.x), Mathf.RoundToInt(child.position.y));
                if (child_position.x<0 || child_position.x > 9)
                {
                    return false;
                }
                if (child_position.y < 0 || child_position.y > 22)
                {
                    return false;
                }

                if (Grid[child_position.x, child_position.y] != null)
                {
                    return false;
                }
            }

        }
        return true;
    }

    public bool Check_Row_Full(int k)
    {
        int i = 0; // row 가 몇개나 차있는지 체크해주는 역할
        for(int j = 0;j<10;j++)
        {
            if(Grid[j, k] != null)//그리드의 j,k좌표가 비어있지않다면
            {
                i++;            //i를 추가함
                if (i == 10)    //row에 10개가 차있다면 
                {
                    return true; // 해당 row 1줄 가득참
                }
            }
            else // 한칸이라도 비어있다면
            {
                return false; // 바로 false를 반환하여 메모리절약
            }
             
        }
        return false;
    }

    IEnumerator Del_Rows()
    {
        for (int i = 0; i < 20; i ++ )
        {
            if (Check_Row_Full(i))
            {
                Del_Row(i);
                Decrease_Row(i);
                i--;
                GameObject.Find("GameManager").GetComponent<Game_Manager>().Add_Score();
                GameObject.Find("GameManager").GetComponent<Game_Manager>().Play_LineClear_Sound();
                yield return new WaitForSeconds(0.8f);
            }
        }
        if (!Check_GameOver()) {
            yield return new WaitForSeconds(0.4f);
            GameObject.Find("GameManager").GetComponent<Game_Manager>().Spawn();
        }
    }


    public void Del_Row(int j)
    {
        for (int a = 0; a < 10; a++)
        {
            Destroy(Grid[a, j].gameObject);
            Grid[a, j] = null;
        }
        foreach (Transform m in Spawn_Position)
        {
            if(m.childCount < 1)
            {
                Destroy(m.gameObject);
            }
            
        }
    }
    

    public void  Decrease_Row(int i)
    {
        for (int a = i; a < 22; a++)
        {
            for (int b = 0; b < 10; b++)
            {
                if (Grid[b, a + 1] != null)
                {
                    Grid[b, a] = Grid[b, a + 1];
                    Grid[b, a + 1] = null;
                    Grid[b, a].position += Vector3.down;

                }

            }
        }
        
    }

    bool Check_GameOver()
    {
        for (int a = 20; a < 22; a++)
        {
            for (int b = 0; b < 10; b++)
            {
                if (Grid[b, a] != null)
                {
                    //Application.Quit();
                    GameObject.Find("GameManager").GetComponent<Game_Manager>().GameEnd();
                    return true;

                }

            }
        }
        return false;
    }


    public void Clear_Grid()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 23; j++)
            {
                if (Grid[i, j] != null)
                {
                    Destroy(Grid[i, j].gameObject);
                    Grid[i, j] = null;
                }
            }
        }

        foreach (Transform t in Spawn_Position)
        {
            if (t.childCount >= 0)
            {
                Destroy(t.gameObject);
            }
        }
    }

    //public void Update_Grid(Transform m)
    //{
    //    foreach (Transform child in m)
    //    {
    //        if (child.gameObject.tag.Equals("Square"))
    //        {
    //            int a = (int)child.position.x;
    //            int b = (int)child.position.y;
    //            if (Grid[a, b] == null)
    //            {
    //                for (int i = 0; i < 10; i++)
    //                {
    //                    for (int j = 0; i < 20; j++)
    //                    {
    //                        Grid[a, b] = child.transform;
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                Game_Manager.Move_manager.transform.position += Vector3.up;
    //                for (int i = 0; i < 10; i++)
    //                {
    //                    for (int j = 0; i < 20; j++)
    //                    {
    //                        Grid[a, b] = child.transform;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
