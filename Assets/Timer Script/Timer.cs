using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private float timerDuration = 3f * 60f; // TIMER DURATION can change later

    private float timer;
    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI seperator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;    
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
        }
        
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

    }
}
