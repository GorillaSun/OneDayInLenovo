using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Working : MonoBehaviour {

	public Text question;
	public Text question2;
	public Text hint;
	public InputField answerInput;
	public Text timeShow;

	private int inMeeting; // 0 start, 1 inMeeting, 2 end
	private float timeTemp;
	private int score;
	private int totalMinutes;
	private string gameFlagList;


	// Use this for initialization
	void Start () {
		gameFlagList = PlayerPrefs.GetString ("gameFlagList");

		answerInput=GameObject.Find("Answer").gameObject.GetComponent<InputField>();
		answerInput.gameObject.SetActive (false);

		question.text = "Ready for debuging the code?";
		question2.text = "";
		hint.text = "Type : 【space】";
		timeShow.text = "";

		inMeeting = 0;
		timeTemp = 0;
		score = PlayerPrefs.GetInt ("Score");
		totalMinutes = PlayerPrefs.GetInt("Time");
	}

	// Update is called once per frame
	void Update () {
		if (inMeeting == 0) {
			if (Input.GetKeyDown ("space")) {
				question.text = "[Debuging]Find and remove the bug:";
				question2.text = "b u g e t";
				hint.text = "Hint : [b][u][g] is here, delete it!!!";
				answerInput.gameObject.SetActive (true);
				answerInput.ActivateInputField ();
				answerInput.text = "";
				inMeeting = 1;
			}
		} else if (inMeeting == 1){
			timeTemp = timeTemp + Time.deltaTime;
			timeShow.text = "Time spent : " + timeTemp.ToString () + "s";

			if (answerInput.text == "et") {
				question.text = "[Debuging]Find and remove the bug:";
				question2.text = "hamburger";
				hint.text = "Hint : [bug] is here, delete it!!!";
				answerInput.text = "";
			} else if (answerInput.text == "hamrer") {
				question.text = "[Debuging]Find and remove the bug:";
				question2.text = "ajwdybodrm";
				hint.text = "Hint : [worm] is here, delete it!!!";
				answerInput.text = "";
			} else if (answerInput.text == "ajdybd") {
				question.text = "[Debuging]Find and remove the bug:";
				question2.text = "insectinsectaaainsectinsectinsect";
				hint.text = "Hint : [insect] is here, delete it!!!";
				answerInput.text = "";
			} else if (answerInput.text == "aaa") {
				question.text = "Finished!";
				question2.text = "";
				answerInput.gameObject.SetActive (false);
				if (timeTemp < 30) {
					score = score + 30;
					gameFlagList = gameFlagList.Substring (0, 11) + "1" + gameFlagList.Substring (12);
					hint.text = "你简直是神一般的存在，这么短的时间解决了复杂的问题。心情+30\n按【SPACE】离开";
				} else if (timeTemp < 60) {
					score = score + 20;
					gameFlagList = gameFlagList.Substring (0, 11) + "2" + gameFlagList.Substring (12);
					hint.text = "解决了相当多的问题。心情+20\n按【SPACE】离开";
				} else {
					score = score + 10;
					gameFlagList = gameFlagList.Substring (0, 11) + "3" + gameFlagList.Substring (12);
					hint.text = "不知道为什么，你好像今天心不在焉。心情+10\n按【SPACE】离开";
				}
				inMeeting = 2;
			}
		} else if (inMeeting == 2) {
			if (Input.GetKeyDown ("space")) {
				PlayerPrefs.SetInt ("Score", score);
				PlayerPrefs.SetInt ("Time", totalMinutes);
				PlayerPrefs.SetString ("gameFlagList", gameFlagList);
				PlayerPrefs.SetString ("FromPlace", "Working");
				SceneManager.LoadScene ("BuildingH2");
			}
		}
	}
}

