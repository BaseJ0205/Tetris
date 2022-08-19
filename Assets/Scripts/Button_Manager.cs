using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Manager : MonoBehaviour
{
    public GameObject Start_Btn;
    public GameObject End_Btn;
    public GameObject ReStart_Btn;
    public GameObject Pause_Btn;
    public GameObject Home_Btn;


    bool is_Pause = false;

    // Start is called before the first frame update
    void Start()
    {
        Pause_Btn.SetActive(false);
        ReStart_Btn.SetActive(false);
        is_Pause = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Exit_Button()
    {
        Application.Quit();
        //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!");
    }

    public void Start_Button()
    {
        Pause_Btn.SetActive(true);
        GameObject.Find("GameManager").GetComponent<Game_Manager>().GameStart();
        Start_Btn.SetActive(false);
        End_Btn.SetActive(false);
    }
    public void ReStart_Button()
    {
        Game_Manager.Grid.Clear_Grid();
        GameObject.Find("GameManager").GetComponent<Game_Manager>().GameStart();
        Start_Btn.SetActive(false);
        End_Btn.SetActive(false);
        Time.timeScale = 1;
        GameObject.Find("GameManager").GetComponent<Game_Manager>().Reset_Score();

        ReStart_Btn.SetActive(false);
    }
    public void Pause_Button()
    {
        if (!is_Pause)
        {
            Time.timeScale = 0;
            ReStart_Btn.SetActive(true);
            End_Btn.SetActive(true);
            is_Pause = true;
        }
        else 
        {
            Time.timeScale = 1;
            ReStart_Btn.SetActive(false);
            End_Btn.SetActive(false);
            is_Pause = false;
        }
    }
    public void Home_Button()
    {
        Pause_Btn.SetActive(false);
        ReStart_Btn.SetActive(false);
        ReStart_Btn.SetActive(true);
        is_Pause = false;

        if (!is_Pause)
        {
            Time.timeScale = 0;
            ReStart_Btn.SetActive(true);
            End_Btn.SetActive(true);
            is_Pause = true;
        }
        else
        {
            Time.timeScale = 1;
            ReStart_Btn.SetActive(false);
            End_Btn.SetActive(false);
            is_Pause = false;
        }
    }
}
