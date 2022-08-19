using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



[RequireComponent(typeof(Grid_Manager))]
[RequireComponent(typeof(Move))]



public class Game_Manager : MonoBehaviour
{
    public TextMeshProUGUI Score_Text; 
    public TextMeshProUGUI BestScore_Text;
    public TextMeshProUGUI Name_Text;

    public AudioClip LineClear_Sound;
    public AudioClip Drop_Sound;
    public AudioClip GameOver_Sound;

    public AudioSource effect_Sound;
   

    public GameObject[] Prefabs;
    static private Grid_Manager Grid_Manage;
    static private Move Move_manage;

    public static int Score = 0;
    public static int BestScore = 0;
    bool is_Start;

    static public Grid_Manager Grid
    {
        get { return Grid_Manage; }
    }
    static public Move Move_manager
    {
        get { return Move_manage; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Grid_Manage = GetComponent<Grid_Manager>();
        Move_manage = GetComponent<Move>();
        Score_Text.gameObject.SetActive(false);
        BestScore_Text.gameObject.SetActive(false);
        //GameStart();
    }

    // Update is called once per frame
    void Update()
    {

        Score_Text.text = "Score : " + Score;
        BestScore_Text.text = "BestScore : " + BestScore;
    }
    public void Spawn()
    {
        int a = Random.Range(0,Prefabs.Length);
        Instantiate(Prefabs[a],Grid.Spawn_Position);
    }
    public void Play_Drop_Sound()
    {
        effect_Sound.clip = Drop_Sound;
        effect_Sound.Play();
    }

    public void Play_LineClear_Sound()
    {
        effect_Sound.clip = LineClear_Sound;
        effect_Sound.Play();
    }
    public void Play_GameOver_Sound()
    {
        effect_Sound.clip = GameOver_Sound;
        effect_Sound.Play();
    }

    public void Add_Score()
    {
        Score += 100;
        Update_BestScore(Score);
    }

    public void Reset_Score()
    {
        Score = 0;
    }

    public void Update_BestScore(int a)
    {
        if (a>=BestScore)
        {
            BestScore = Score;
        }
    }
    public void GameStart()
    {

        Name_Text.gameObject.SetActive(false);
        Score_Text.gameObject.SetActive(true);
        BestScore_Text.gameObject.SetActive(true);
        Spawn();
        Grid.Init_Grid();
        //Grid.Clear_Grid();
        is_Start = false;
        Score = 0;
    }
    public void GameEnd()
    {
        Name_Text.gameObject.SetActive(true);
        Score_Text.gameObject.SetActive(true);
        BestScore_Text.gameObject.SetActive(true);
        Grid.Clear_Grid();
        GameObject.Find("Director").GetComponent<Button_Manager>().ReStart_Btn.SetActive(true);
    }
}
