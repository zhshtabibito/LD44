using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkBar : MonoBehaviour
{
    public float addInterval;
    public float minusInterval;
    public Image contentImage; 
    public Father996 father;
    private LevelManager lm;
    private bool cleared;
    // Start is called before the first frame update
    void Start()
    {
        contentImage.fillAmount = 0;
        cleared = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if(father.GetWorkingState() == true && contentImage.fillAmount < 1)
        {
            contentImage.fillAmount += addInterval;
        }
        if(father.GetWorkingState() == false && contentImage.fillAmount > 0)
        {
            contentImage.fillAmount -= minusInterval;
        }
        if(contentImage.fillAmount == 1 && cleared == false)
        {
            StartCoroutine(FatherDead());
            cleared = true;
        }
    }
    public IEnumerator FatherDead()
    {
        father.Dead();
        yield return new WaitForSeconds(3.0f);
        lm = FindObjectOfType<LevelManager>();
        lm.LevelClear();
    }
}
