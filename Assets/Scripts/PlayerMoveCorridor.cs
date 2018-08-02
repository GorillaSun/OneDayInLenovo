using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMoveCorridor : MonoBehaviour {

	public Text countText;
	public Text timeText;
	public Text eventText;
	public Sprite[] imageList;

	private int score;
	private int totalMinutes;
	private float timeFly;
	private Animator m_animator;
	private string gameFlagList;

	// Use this for initialization
	void Start () {
		timeFly = 0;
		gameFlagList = PlayerPrefs.GetString ("gameFlagList");
		if (PlayerPrefs.GetInt ("Time") > 540) {		
			score = PlayerPrefs.GetInt ("Score");
			totalMinutes = PlayerPrefs.GetInt("Time");
			PlayerPrefs.SetInt ("Time", 540);
			if (PlayerPrefs.GetString("FromPlace") == "H2") {
				transform.position = new Vector3(33, 10, 190);
				transform.localEulerAngles = new Vector3 (0, 180, 0);
			}
			if (PlayerPrefs.GetString ("FromPlace") == "CB") {
				transform.position = new Vector3 (462, 10, 180);
			}
		} else {
			score = 30;
			totalMinutes = 540;
			transform.position = new Vector3(62, 10, 7);
		}
		ScoreSet (0);
		TimeSet(0);

		GameObject lightsRoot = GameObject.Find("Lights"); 
		if (totalMinutes > 720) {
			lightsRoot.transform.Find ("Directional Light Morning").gameObject.SetActive (false);
			lightsRoot.transform.Find ("Directional Light Afternoon").gameObject.SetActive (true);
		} else {
			lightsRoot.transform.Find ("Directional Light Morning").gameObject.SetActive (true);
			lightsRoot.transform.Find ("Directional Light Afternoon").gameObject.SetActive (false);
		}

		GameObject eventRoot = GameObject.Find("Event"); 
		GameObject peopleRoot = GameObject.Find("Peoples"); 
		if (totalMinutes >= 540 && totalMinutes <= 780) {
			//9:00 to 13:00
			if (gameFlagList.Substring(2, 1) == "0") {
				eventRoot.transform.Find ("EventYD").gameObject.SetActive (true);
				eventRoot.transform.Find ("EventYDText").gameObject.SetActive (true);
				peopleRoot.transform.Find ("Male3_YD").gameObject.SetActive (true);
			}
		} else if (totalMinutes > 780 && totalMinutes <= 960) {
			// 13:00 to 16:00
			if (gameFlagList.Substring (3, 1) == "0") {
				eventRoot.transform.Find ("EventYY").gameObject.SetActive (true);
				eventRoot.transform.Find ("EventYYText").gameObject.SetActive (true);
				peopleRoot.transform.Find ("Male3_YY").gameObject.SetActive (true);

			}
		} else if (totalMinutes > 960) {
			// 16:00
			if (gameFlagList.Substring (4, 1) == "0") {
				eventRoot.transform.Find ("EventSN").gameObject.SetActive (true);
				eventRoot.transform.Find ("EventSNText").gameObject.SetActive (true);
				peopleRoot.transform.Find ("Male3_SN").gameObject.SetActive (true);
			}
		}
		if (gameFlagList.Substring (0, 1) == "1") {
			eventRoot.transform.Find ("EventShower").gameObject.SetActive (false);
			eventRoot.transform.Find ("EventShowerText").gameObject.SetActive (false);
		}
		if (gameFlagList.Substring (1, 1) == "1") {
			eventRoot.transform.Find ("EventFishing").gameObject.SetActive (false);
			eventRoot.transform.Find ("EventFishingText").gameObject.SetActive (false);
		}
			
		m_animator = GetComponent<Animator> ();
		m_animator.SetTrigger ("idle");
	}

	// Update is called once per frame
	void Update () {
		GameObject eventRoot = GameObject.Find("Event"); 
		GameObject canvasRoot = GameObject.Find("Canvas"); 
		GameObject eventImage = canvasRoot.transform.Find ("EventImage").gameObject;

		if (eventImage.gameObject.activeSelf == false) {
			if (totalMinutes >= 1110) {
				gameFinished ();
			}
			if (totalMinutes >= 960 && gameFlagList.Substring (5, 1) == "0") {
				eventImage.SetActive (true);
				eventImage.GetComponent<Image>().overrideSprite = imageList [6];
				gameFlagList = gameFlagList.Substring(0, 5) + "2" + gameFlagList.Substring (6);
				ScoreSet (-30);
				eventText.text = "忘了吃中午饭吗，快饿扁了！\n 心情-30\n按【SPACE】离开";
			}
			if (totalMinutes >= 1020 && gameFlagList.Substring (13, 1) == "0") {
				eventImage.SetActive (true);
				eventImage.GetComponent<Image>().overrideSprite = imageList [4];
				gameFlagList = gameFlagList.Substring(0, 13) + "1" + gameFlagList.Substring (14);
				eventText.text = "6:30今天的生活就要结束了，没做的事情赶紧做吧。\n按【SPACE】离开";
			}
		}

		timeFly = timeFly + Time.deltaTime * 15;
		if (timeFly > 100) {
			timeFly = 0;
			TimeSet (5);
		}
		if (eventImage.gameObject.activeSelf == false) {
			if (Input.GetKeyUp ("up") && Input.anyKey == false) {
				m_animator.ResetTrigger ("walk");
				m_animator.SetTrigger ("idle");
			} else if (Input.GetKeyUp ("down") && Input.anyKey == false) {
				m_animator.ResetTrigger ("walk");
				m_animator.SetTrigger ("idle");
			} else if (Input.GetKeyUp ("left") && Input.anyKey == false) {
				m_animator.ResetTrigger ("walk");
				m_animator.SetTrigger ("idle");
			} else if (Input.GetKeyUp ("right") && Input.anyKey == false) {
				m_animator.ResetTrigger ("walk");
				m_animator.SetTrigger ("idle");
			}
			if (Input.GetKeyDown ("up")) {
				m_animator.ResetTrigger ("idle");
				m_animator.SetTrigger ("walk");
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			} else if (Input.GetKeyDown ("down")) {
				m_animator.ResetTrigger ("idle");
				m_animator.SetTrigger ("walk");
				transform.localEulerAngles = new Vector3 (0, 180, 0);
			} else if (Input.GetKeyDown ("left")) {
				m_animator.ResetTrigger ("idle");
				m_animator.SetTrigger ("walk");
				transform.localEulerAngles = new Vector3 (0, -90, 0);
			} else if (Input.GetKeyDown ("right")) {
				m_animator.ResetTrigger ("idle");
				m_animator.SetTrigger ("walk");
				transform.localEulerAngles = new Vector3 (0, 90, 0);
			}

			float moveH = Input.GetAxis ("Horizontal");
			float moveV = Input.GetAxis ("Vertical");

			if (Input.GetKey ("up")) {
				transform.Translate (0, 0, moveV * 1);
			}
			if (Input.GetKey ("down")) {
				transform.Translate (0, 0, moveV * -1);
			}
			if (Input.GetKey ("left")) {
				transform.Translate (0, 0, moveH * -1);
			}
			if (Input.GetKey ("right")) {
				transform.Translate (0, 0, moveH * 1);
			}
		}
		if (Input.GetKeyDown ("space")) {
			float x = transform.position.x;
			float z = transform.position.z;
			// event screen show
			if (eventImage.gameObject.activeSelf == true) {
				eventImage.gameObject.SetActive (false);
				eventText.text = "";
				if (x > 30 && x < 37 && z > 197 && z < 202) {
					//H2
					setPlayPrefsAndChangeScene("BuildingH2");
				}
			} else {
				if (x > 232 && x < 240 && z > 188 && z < 196 && eventRoot.transform.Find ("EventShower").gameObject.activeSelf == true) {
					// Shower
					eventRoot.transform.Find ("EventShower").gameObject.SetActive (false);
					eventRoot.transform.Find ("EventShowerText").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image>().overrideSprite = imageList [0];
					gameFlagList = "1" + gameFlagList.Substring (1);
					ScoreSet (10);
					TimeSet (30);
					eventText.text = "你花了三十分钟洗了个痛快澡，\n虽然是在大庭广众之下，但是你感觉心情舒畅！\n 心情+10\n按【SPACE】离开";
				} else if (x > 39 && x < 46 && z > 167 && z < 173) {
					// Stair
					setPlayPrefsAndChangeScene("BuildingH2");
				} else if ((x > 88 && x < 95 && z > 229 && z < 235) || (x > 299 && x < 307 && z > 257 && z < 262)) {
					// Canteen
					if (totalMinutes < 690) {
						// 11:30
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [4];
						eventText.text = "11:30才开饭呦";
					} else if (totalMinutes >= 690 && totalMinutes <= 750) {
						// 11:30 to 12:40
						if (gameFlagList.Substring(5, 1) == "0") {
							eventImage.SetActive (true);
							eventImage.GetComponent<Image>().overrideSprite = imageList [2];
							gameFlagList = gameFlagList.Substring(0, 5) + "1" + gameFlagList.Substring (6);
							ScoreSet (20);
							TimeSet (30);
							eventText.text = "你花了三十分钟痛痛快快的吃了顿饭，感觉心情舒畅！\n 心情+20\n按【SPACE】离开";
						} else {
							eventImage.SetActive (true);
							eventImage.GetComponent<Image>().overrideSprite = imageList [4];
							eventText.text = "咱们又不是猪，吃一顿就得了。\n按【SPACE】离开";
						}
					} else if (totalMinutes > 750 && totalMinutes <= 780 ) {
						//12:30 to 13:00
						if (gameFlagList.Substring(5, 1) == "0") {
							eventImage.SetActive (true);
							eventImage.GetComponent<Image>().overrideSprite = imageList [3];
							gameFlagList = gameFlagList.Substring(0, 5) + "1" + gameFlagList.Substring (6);
							ScoreSet (5);
							TimeSet (30);
							eventText.text = "你到了食堂，但是发现吃的东西已经所剩无几，但是起码填饱了肚子！\n 心情+5\n按【SPACE】离开";
						} else {
							eventImage.SetActive (true);
							eventImage.GetComponent<Image>().overrideSprite = imageList [4];
							eventText.text = "咱们又不是猪，吃一顿就得了。\n按【SPACE】离开";
						}
					} else {
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [4];
						ScoreSet (-5);
						TimeSet (10);
						eventText.text = "因为尝试在这个时间吃饭失败，被路人笑话！\n 心情-5\n按【SPACE】离开";
					}
				} else if (x > 283 && x < 292 && z > 218 && z < 223 && eventRoot.transform.Find ("EventFishing").gameObject.activeSelf == true) {
					// Fishing
					eventRoot.transform.Find ("EventFishing").gameObject.SetActive (false);
					eventRoot.transform.Find ("EventFishingText").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image>().overrideSprite = imageList [1];
					gameFlagList = gameFlagList.Substring(0, 1) + "1" + gameFlagList.Substring (2);
					ScoreSet (-10);
					TimeSet (30);
					eventText.text = "你花了三十分钟在这里钓鱼，\n然而什么都没有钓上来，在众目睽睽之下，你觉得有点尴尬。\n 心情-10\n按【SPACE】离开";
				} else if (x > 410 && x < 417 && z > 299 && z < 305 && eventRoot.transform.Find ("EventYD").gameObject.activeSelf == true) {
					eventRoot.transform.Find ("EventYD").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image>().overrideSprite = imageList [5];
					gameFlagList = gameFlagList.Substring(0, 2) + "1" + gameFlagList.Substring (3);
					ScoreSet (15);
					TimeSet (30);
					eventText.text = "你花了半个小时和Ruodong探讨了一下\n最新的科技走势，茅塞顿开。\n 心情+15\n按【SPACE】离开";
				} else if (x > 417 && x < 423 && z > 295 && z < 301 && eventRoot.transform.Find ("EventYY").gameObject.activeSelf == true) {
					eventRoot.transform.Find ("EventYY").gameObject.SetActive (false);
					eventImage.gameObject.SetActive (true);
					eventImage.GetComponent<Image>().overrideSprite = imageList [8];
					gameFlagList = gameFlagList.Substring(0, 3) + "1" + gameFlagList.Substring (4);
					ScoreSet (20);
					TimeSet (30);
					eventText.text = "你花了半个小时和Yuanqing畅谈了当今时代下企业发展的规划和策略，收获颇丰！\n 心情+20\n按【SPACE】离开";
				} else if (x > 422 && x < 429 && z > 290 && z < 296 && eventRoot.transform.Find ("EventSN").gameObject.activeSelf == true) {
					eventRoot.transform.Find ("EventSN").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image>().overrideSprite = imageList [9];
					gameFlagList = gameFlagList.Substring(0, 4) + "1" + gameFlagList.Substring (5);
					ScoreSet (30);
					TimeSet (30);
					eventText.text = "你花了半个小时和Sunning畅聊，你对人生有了新的认识，又有了新的理想新的目标，" +
						"\n一个小时后你还觉得没有聊够，但是碍于时间太长只好作罢，你觉得人生得到了升华！\n 心情+30\n按【SPACE】离开";
				} else if (x > 30 && x < 37 && z > 197 && z < 202) {
					// H2
					if (totalMinutes < 570) {
						// elevtor crowd before 9:30
						eventImage.gameObject.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [7];
						ScoreSet (-10);
						TimeSet (30);
						eventText.text = "早高峰的电梯拥挤让你花了三十分钟才到H2，你虽然心有不甘，但也只能接受这个事实！\n 心情-10\n按【SPACE】离开";
					} else {
						setPlayPrefsAndChangeScene("BuildingH2");
					}
				} else if (x > 468 && x < 475 && z > 151 && z < 158) {
					// Conference building
					setPlayPrefsAndChangeScene("ConferenceBuilding");
				}
			}
		}
	}

	void TimeSet(int addValue) {
		totalMinutes = totalMinutes + addValue;
		timeText.text = "时间" + (totalMinutes/60).ToString() + ":" +(totalMinutes % 60).ToString ();
	}

	void ScoreSet(int addValue) {
		score = score + addValue;
		countText.text = "心情 : " + score.ToString ();
	}

	void setPlayPrefsAndChangeScene(string place) {
		PlayerPrefs.SetInt ("Score", score);
		PlayerPrefs.SetInt ("Time", totalMinutes);
		PlayerPrefs.SetString ("gameFlagList", gameFlagList);
		PlayerPrefs.SetString ("FromPlace", "Corridor");
		SceneManager.LoadScene (place);
	}

	void gameFinished() {
		PlayerPrefs.SetInt ("Score", score);
		PlayerPrefs.SetInt ("Time", totalMinutes);
		PlayerPrefs.SetString ("gameFlagList", gameFlagList);
		SceneManager.LoadScene ("StartAndFin");
	}
}

