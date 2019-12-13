using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using ReflexCLI.Attributes;

[ConsoleCommandClassCustomizer("TM")]
public class TimeManager : MonoBehaviour {
    static TimeManager _instance;
    public static TimeManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Start()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        
    }
    public float secondsInADay = 60f;
    float elapsed;
    [SerializeField]
    RectTransform clock;

    [SerializeField]
    TMP_Text dayText;


    int currentDay = 1;

    float _fuel = 100f;
    float fuel
    {
        get
        {
            return _fuel;
        }
        set
        {
            _fuel = value;
            rtFuel.localScale = new Vector3(fuel/100f,1f,1f);
        }
    }
    [SerializeField]
    float fuelLoosingSpeed = 1f;

    [SerializeField]
    RectTransform rtFuel;

    [ConsoleCommand]
    public static void Refill()
    {
        _instance.fuel = 100f;
    }

    public static bool HasFuel()
    {
        return _instance.fuel > 0f;
    }

    public void Progress()
    {
        Vector3 r = clock.eulerAngles;
        r.z = -360f*(elapsed/secondsInADay);
        clock.eulerAngles = r;
        elapsed += Time.deltaTime;
        if(elapsed >= secondsInADay)
            NextDay();

        if(fuel > 0f)
        {
            fuel -= fuelLoosingSpeed * Time.deltaTime;
            if(fuel <= 0f)
                fuel = 0f;
        }
        
    }

    public void NextDay()
    {
        elapsed = 0f;
        currentDay++;
        dayText.DOText("Day " + currentDay, 0.5f, scrambleMode:ScrambleMode.All);
    }
}