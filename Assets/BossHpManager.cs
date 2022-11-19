using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpManager : MonoBehaviour
{
    private Image hpImg;
    
    // Start is called before the first frame update
    void Start()
    {
        hpImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hpImg.fillAmount = BossManager.instance.Health / 1000;
    }
}
