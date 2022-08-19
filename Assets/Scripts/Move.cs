using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    float last_time = 0.0f;//������ ������ �ð������ؼ� ���� ����
    private float delay= 0.8f;//�����̽ð�

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


        //Check_inside(this.transform);//Grid�������� ������ üũ

        //start_time = Time.time;//���۽ð��� Time.time�� �־ �ð�����

        if (Input.GetKeyDown("d"))//d ��ư�� ��������
        {

            transform.position += Vector3.right;// �������� ��ĭ�̵�
            if (!Check_inside(this.transform))//Check_inside �Լ����� ������ ���ð��
            {
                transform.position += Vector3.left;//���������� ��ĭ�̵�
            }

        }
        else if (Input.GetKeyDown("a"))//a ��ư�� ������ ��
        {
            transform.position += Vector3.left;// �������� ��ĭ�̵�
            if (!Check_inside(this.transform))//Check_inside �Լ����� ������ ���ð��
            {
                transform.position += Vector3.right;//���������� ��ĭ�̵�
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


        if (Input.GetKeyDown(KeyCode.Space))//d ��ư�� ��������
        {

            while (true)
            {
                transform.position += Vector3.down;//������ ��ĭ ������
                if (!Check_inside(this.transform))
                {

                    break;
                }

            }

            transform.position += Vector3.up;
            //GameObject.Find("GameManager").GetComponent<Game_Manager>().Play_Drop_Sound();
            Game_Manager.Grid.Insult_Grid(transform);//shape�� Grid�ȿ� �־���
            enabled = false;
            


        }

        if (Time.time - last_time >= ((Input.GetKey("s")) ? delay / 10 : delay))//Fall ����
        {
            transform.position += Vector3.down;//������ ��ĭ ������
            GameObject.Find("GameManager").GetComponent<Game_Manager>().Play_Drop_Sound();
            if (!Check_inside(this.transform))//Grid�������� ������ üũ
            {
                transform.position += Vector3.up;
                Game_Manager.Grid.Insult_Grid(transform);//shape�� Grid�ȿ� �־���
                enabled = false;
            }
            last_time = Time.time;//�Լ������� ������ �ð�����
        }
    }





    
    //IEnumerator Fall()//�������� �ڷ�ƾ
    //{
    //    if (transform.position.y > 0)//���� ������Ʈ�� y��ǥ�� 0���� ũ��
    //    {
    //        transform.position += Vector3.down;//�Ʒ��� ��ĭ ������
    //    }
    //    Check_inside(this.transform);//Grid�������� ������ üũ
    //    yield return new WaitForSeconds(1.0f); // 1�� ��ٸ�
    //    StartCoroutine("Fall");// Fall �ڷ�ƾ �����(update�� ������ ���Ѵ�� ������ start�� �����Ͽ� �ݺ�����ǵ���)
    //    //Debug.Log(transform.position.y);
    //}

    bool Check_inside(Transform m)//�������� ���޹޾Ƽ� m�� ��ǥ���� Grid�� ���ԵǾ� �ִ��� üũ���ִ� �Լ�
    {
        foreach(Transform child in m)//foreach���� ����Ͽ� m������ �ڽĵ��� ����
        {
            if (child.tag.Equals("Square"))//�ڽĵ� �߿��� Square���� ���
            {
                //Debug.Log(child.position);
                Vector2Int child_position = new Vector2Int(Mathf.RoundToInt(child.position.x),Mathf.RoundToInt(child.position.y));
                //�ڽĵ��� xy��ǥ���� �ݿø��Ͽ� �޾ƿ� (�ݿø��ϴ� ������ �̵��ϸ鼭 �Ҽ����� ����鼭 ������������ ����)


                if (child_position.x < 0 || child_position.x > 9)//x��ǥ ���ؼ� ���ΰ� �ƴ϶�� 
                {
                    return false;//false ����
                }
                else if (child_position.y < 0 || child_position.y >22)//y���� ����ٸ�
                {
                    return false;//false����
                }
                else if (!Game_Manager.Grid.Check_Grid(transform))
                {
                    return false;//false����
                }
            }


        }
        return true;//���� ���׿��� ���ϵ��������� false�� �ƴϹǷ� true����
    }
}
