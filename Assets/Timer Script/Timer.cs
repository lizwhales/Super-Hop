using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
 
    private float timerDuration = 1f * 30f; // TIMER DURATION can change later

    private float timer;
    [SerializeField]
    public TextMeshProUGUI firstMinute;
    [SerializeField]
    public TextMeshProUGUI secondMinute;
    [SerializeField]
    public TextMeshProUGUI seperator;
    [SerializeField]
    public TextMeshProUGUI firstSecond;
    [SerializeField]
    public TextMeshProUGUI secondSecond;
    [SerializeField]
    public GameObject losePanel;
    // set curr time to time duration, etc.
    
    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if(timer > 0){
        timer -= Time.deltaTime;
        UpdateTimerDisplay(timer);
        }else{
            Flash();
            GameObject Player = GameObject.Find("Player");
            Player.GetComponent<Respawn>().RespawnPoint();
            LoseGame();
            // Respawn.RespawnPoint();
            Debug.Log("TIME UP YOU DED");
        }

        if(timer == 0){
            GameObject Player = GameObject.Find("Player");
            Player.GetComponent<Respawn>().RespawnPoint();
            // Respawn.RespawnPoint();
            Debug.Log("TIME UP YOU DED");
        }
        
    }

    private void LoseGame(){
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        losePanel.SetActive(true);
    }

    void ResetTimer(){
        timer = timerDuration;
    }

    private void UpdateTimerDisplay(float time){
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{01:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();

    }

    private void Flash(){
       // can do smthing here if wanted 
        firstMinute.text = "d";
        secondMinute.text = "e";
        firstSecond.text = "a";
        secondSecond.text = "d";

    }
}
