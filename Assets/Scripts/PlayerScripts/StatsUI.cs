using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    private bool statsOpen=false;
    private void Start()
    {
        UpdateAllstats();
    }
    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            Debug.Log("Toggling Stats" + statsOpen);
            if (statsOpen)
            {
                 Time.timeScale = 1;
                  UpdateAllstats();
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllstats();
                Debug.Log("Opening Stats");
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                statsOpen = true;
            }
        }

    }
    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage.ToString();
    }
    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed.ToString();
    }
    public void UpdateAllstats()
    {
        UpdateDamage();
        UpdateSpeed();
    }
}
