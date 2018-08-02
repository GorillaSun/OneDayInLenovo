using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MeetingWithBT : MonoBehaviour {

	public Text BTword;
	public Text hint;
	public InputField ITword;
	public Text timeShow;

	private int inMeeting; // 0 start, 1 inMeeting, 2 end
	private float timeTemp;
	private int score;
	private int totalMinutes;
	private string gameFlagList;


	// Use this for initialization
	void Start () {
		gameFlagList = PlayerPrefs.GetString ("gameFlagList");

		ITword=GameObject.Find("ITword").gameObject.GetComponent<InputField>();
		ITword.gameObject.SetActive (false);

		BTword.text = "Ready for meeting?";
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
				BTword.text = "What happened? Why need so much time?";
				hint.text = "Type : [you do not understand] or [we need time]";
				ITword.gameObject.SetActive (true);
				ITword.ActivateInputField ();
				ITword.text = "";
				inMeeting = 1;
			}
		} else if (inMeeting == 1){
			timeTemp = timeTemp + Time.deltaTime;
			timeShow.text = "Time spent : " + timeTemp.ToString () + "s";

			if (ITword.text == "you do not understand" || ITword.text == "we need time") {
				BTword.text = "Let’s please have a review of this before it goes to production given the issues.  ";
				hint.text = "Type : [you are the boss] or [of course]";
				ITword.text = "";
			} else if (ITword.text == "you are the boss" || ITword.text == "of course") {
				BTword.text = "Been over 48 hours and we are still trying to connect?  Customers are a sensitive thing.";
				hint.text = "Type : [do not push me] or [that is the thing]";
				ITword.text = "";
			} else if (ITword.text == "do not push me" || ITword.text == "that is the thing") {
				BTword.text = "What is the timeline for these changes being proposed? ";
				hint.text = "Type : [will be soon] or [no duedate man]";
				ITword.text = "";
			} else if (ITword.text == "will be soon" || ITword.text == "no duedate man") {
				BTword.text = "That is great. Appreciate your quickly resolving and keep production stable.";
				hint.text = "Type : [thank you so much] or [we will make it]";
				ITword.text = "";
			} else if (ITword.text == "thank you so much" || ITword.text == "we will make it") {
				BTword.text = "Finished!";
				ITword.gameObject.SetActive (false);
				if (timeTemp < 20) {
					score = score + 30;
					hint.text = "一场精彩的博弈！心情+30\n按【SPACE】离开";
					gameFlagList = gameFlagList.Substring (0, 12) + "1" + gameFlagList.Substring (13);
				} else if (timeTemp < 40) {
					score = score + 20;
					hint.text = "关键的问题都得到了解决。心情+20\n按【SPACE】离开";
					gameFlagList = gameFlagList.Substring (0, 12) + "2" + gameFlagList.Substring (13);
				} else {
					score = score + 10;
					hint.text = "解决了最基本的问题。心情+10\n按【SPACE】离开";
					gameFlagList = gameFlagList.Substring (0, 12) + "3" + gameFlagList.Substring (13);
				}
				inMeeting = 2;
			}
		} else if (inMeeting == 2) {
			if (Input.GetKeyDown ("space")) {
				PlayerPrefs.SetInt ("Score", score);
				PlayerPrefs.SetInt ("Time", totalMinutes);
				PlayerPrefs.SetString ("FromPlace", "Meeting");
				PlayerPrefs.SetString ("gameFlagList", gameFlagList);
				SceneManager.LoadScene ("BuildingH2");
			}
		}
	}
}

