using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ZetLevel : MonoBehaviour
{
    public static ZetLevel instance;

    [Header("레벨")]
    public int playerLevel = 1;
    public float currentXp; // 현재 경험치
    public float xp = 100; // 총경험치


    [SerializeField] Slider xpSlider;
    [SerializeField] TMP_Text levelText;
    [SerializeField] GameObject LevelUpEffect;
    // Start is called before the first frame update

    void Start()
    {
        instance = this;
        Player_XP();
    }

    // Update is called once per frame
    void Update()
    {
        xpSlider.value = Mathf.Lerp(xpSlider.value, currentXp / xp, Time.deltaTime * 40f);
        levelText.text = "LV." + playerLevel.ToString();
    }

    public void LV_UP()
    {
        if (currentXp >= xp)
        {
            currentXp -= xp;
            playerLevel++;
            Player_XP();
            LeveUpEffect();


        }
    }
    public void LeveUpEffect()
    {
        LevelUpEffect.SetActive(false);
        LevelUpEffect.SetActive(true);
    }

    public void Player_XP()
    {
        xp = playerLevel * 100;
    }
}
