using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static int seconds = 60;
    public Image buildimage;
    public static Sprite buildImage;
    public Sprite startIm;
    public static int typeB = 2;
    public Text textTimer;

    public Text textBuilding;

    private DateTime timerEnd;
    public bool isTimerOn = false;

    private DateTime risingTime;
    public static bool risingSun = false;

    public static bool isItemSelected = false;

    public static bool destroyed = false;


    public bool isNight = false;

    private bool exitButReady = true;
    private bool newB = false;

    public Animation animmain;
    public Animation animmenu;
    public Animation giveUpBut;


    public Button menu;
    public Button play;
    public Button town;

    public GameObject flag;

    public static int[] itemofProgress = new int[9];
    public static int[] scoreofProgress = new int[9];
    public static int[] normofProgress = new int[9];

    void Start()
    {
        textTimer.text = seconds.ToString();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (!buildImage)
        {
            buildImage = startIm;
        }
        buildimage.sprite = buildImage;
        
    }


    void Update()
    {
        if (destroyed)
        {
            print('a');
            destroyed = false;
            textBuilding.gameObject.SetActive(true);
            string buf = textBuilding.text;
            textBuilding.text = "Ваше здание было разрушено!";
            StartCoroutine(Alert(buf));
            
            

        }

        if (isItemSelected)
        {
            menuClose();        
            buildimage.sprite = buildImage;
            
            isItemSelected = false;
        }
        if (isTimerOn)
        {
            TimeSpan delta = timerEnd - DateTime.Now;
            textTimer.text = "<b>" + delta.Minutes.ToString("00") + ":" + delta.Seconds.ToString("00") + "</b>";
            TimeSpan risingTimedelta = risingTime - DateTime.Now;


            if (seconds - delta.TotalSeconds >= 30 && seconds >= 60 && exitButReady)
            {
                giveUpBut.Play();
                exitButReady = false;
            }

            if (delta.TotalSeconds <= 15)
            {
                nightModeOff();
            }

            else if (risingSun)
            {
                risingTime = DateTime.Now.AddSeconds(15);
                nightModeOff();
                risingSun = false;
            }

            else if (risingTimedelta.TotalSeconds <= -1) textTimer.text = delta.Minutes.ToString("00") + ":" + delta.Seconds.ToString("00");

            else if (risingTimedelta.TotalSeconds <= 0) {
                nightMode();
                
            }

            

            if (delta.TotalSeconds <= 0)
            {
                CheckSuccess();
                Back();
                newB = true;
                flag.SetActive(true);
            }

        }
        else
        {
            TimeSpan total = new TimeSpan(0, 0, 0, seconds);
            textTimer.text = "<b>" + total.Minutes.ToString("00") + ":" + total.Seconds.ToString("00") + "</b>";
        }
    }

    public void CheckSuccess()
    { 
        for(int i = 0; i < 9; i++) 
        {
            if (seconds == normofProgress[i]) {
                scoreofProgress[i]++;
                print(scoreofProgress[i].ToString());
                print(normofProgress[i].ToString() + "if1 norm");
                break;
                
            }
            else if(normofProgress[i] != seconds && scoreofProgress[i] < 1)
            {
                itemofProgress[i] = typeB;
                normofProgress[i] = seconds;
                scoreofProgress[i]++;
                print(itemofProgress[i].ToString());
                print(itemofProgress[i].ToString() + "if2 item");
                break;
                
            }
            
        }
        

    }

    public void Play()
    {
        timerEnd = DateTime.Now.AddSeconds(seconds);
        risingTime = DateTime.Now.AddSeconds(15);
        isTimerOn = true;
        animmain.Play("mainAnimations");
        buildimage.gameObject.SetActive(false);       
    }

    public void Back() 
    {
        isTimerOn = false;
        animmain.Play("mainAnimReverse");
        exitButReady = true;
        buildimage.gameObject.SetActive(true);
        textBuilding.gameObject.SetActive(false);
    }

    public void menuOpen()
    {
        animmenu.Play();
        menu.interactable = false;
        play.interactable = false;
        town.interactable = false;
        flag.SetActive(false);
        buildimage.gameObject.SetActive(false);
    }

    public void menuClose()
    {
        animmenu.Play("menuBack");
        menu.interactable = true;
        play.interactable = true;
        town.interactable = true;
        buildimage.gameObject.SetActive(true);
        if(newB) flag.SetActive(true);
    }

    public void nightMode()
    {
        if (!isNight)
        {
            animmain.Play("TimerShadow");
            isNight = true;
        }
    }

    public void nightModeOff()
    {      
        if (isNight)
        {
            animmain.Play("TimerShadowBack");
            isNight = false;
        }
    }

    public void giveUp()
    {
        giveUpBut.Play("GiveUpReverse");
        Back();
    }

    public void loadTown()
    {
        newB = false;
        flag.SetActive(false);
        SceneManager.LoadScene("City");
        SaveProgress.loadCity = true;
    } 
    void OnApplicationPause()
    {
        
        if (isTimerOn)
        {
            
            nightModeOff();
            StartCoroutine(playAnimDelay());
        }
    }
    IEnumerator playAnimDelay()
    {        
        isTimerOn = false;
        yield return new WaitForSeconds(1);
        Back();      
        destroyed = true;
    }

    IEnumerator Alert(string buf)
    {
        yield return new WaitForSeconds(10);
        textBuilding.text = buf;
        textBuilding.gameObject.SetActive(false);
    }
}  
