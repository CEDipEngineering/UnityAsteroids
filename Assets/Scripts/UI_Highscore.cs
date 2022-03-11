using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Highscore : MonoBehaviour
{
    // Start is called before the first frame update
   public Text textComp;
   public int highscore;
   void Start()
   {
       textComp = GetComponent<Text>();
   }
   
   void Update()
   {
      highscore = PlayerPrefs.GetInt ("highscore", highscore);
       textComp.text = $"Highscore: {highscore}";
   }
}

