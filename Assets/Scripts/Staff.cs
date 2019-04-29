using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Staff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, 0.8f));
        StartCoroutine("WaitAndStop");
    }

    IEnumerator WaitAndStop()
    {
        yield return new WaitForSeconds(12);
        GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }



}
