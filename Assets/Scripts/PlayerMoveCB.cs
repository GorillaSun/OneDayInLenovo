using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMoveCB : MonoBehaviour {

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
		gameFlagList = PlayerPrefs.GetString ("gameFlagList");
		totalMinutes = PlayerPrefs.GetInt ("Time");
		TimeSet (0);
		score = PlayerPrefs.GetInt ("Score");
		ScoreSet (0);

		GameObject eventRoot = GameObject.Find("Events"); 
		if (gameFlagList.Substring (7, 1) == "1") {
			eventRoot.transform.Find ("EventATM").gameObject.SetActive (false);
			eventRoot.transform.Find ("EventATMText").gameObject.SetActive (false);
		}
		if (gameFlagList.Substring (9, 1) == "1") {
			eventRoot.transform.Find ("EventPerform").gameObject.SetActive (false);
			eventRoot.transform.Find ("EventPerformText").gameObject.SetActive (false);
		}

		m_animator = GetComponent<Animator> ();
		m_animator.SetTrigger ("idle");
	}

	// Update is called once per frame
	void Update () {
		GameObject eventRoot = GameObject.Find("Events"); 
		GameObject canvasRoot = GameObject.Find("Canvas"); 
		GameObject eventImage = canvasRoot.transform.Find ("EventImage").gameObject;

		if (eventImage.gameObject.activeSelf == false) {
			if (totalMinutes >= 1110) {
				gameFinished ();
			}
			if (totalMinutes >= 960 && gameFlagList.Substring (5, 1) == "0") {
				eventImage.SetActive (true);
				eventImage.GetComponent<Image>().overrideSprite = imageList [6];
				gameFlagList = gameFlagList.Substring (0, 5) + "2" + gameFlagList.Substring (6);
				ScoreSet (-30);
				eventText.text = "忘了吃中午饭吗，快饿扁了！\n 心情-30";
			}
			if (totalMinutes >= 1020 && gameFlagList.Substring (13, 1) == "0") {
				eventImage.SetActive (true);
				eventImage.GetComponent<Image>().overrideSprite = imageList [2];
				gameFlagList = gameFlagList.Substring(0, 13) + "1" + gameFlagList.Substring (14);
				eventText.text = "6:30今天的生活就要结束了，没做的事情赶紧做吧。";
			}
		}

		timeFly = timeFly + Time.deltaTime * 15;
		if (timeFly > 100) {
			timeFly = 0;
			TimeSet (5);
		}

		if (eventImage.activeSelf == false) {
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
			if (Input.GetKeyDown ("left")) {
				m_animator.ResetTrigger ("idle");
				m_animator.SetTrigger ("walk");
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			} else if (Input.GetKeyDown ("right")) {
				m_animator.ResetTrigger ("idle");
				m_animator.SetTrigger ("walk");
				transform.localEulerAngles = new Vector3 (0, 180, 0);
			} else if (Input.GetKeyDown ("down")) {
				m_animator.ResetTrigger ("idle");
				m_animator.SetTrigger ("walk");
				transform.localEulerAngles = new Vector3 (0, -90, 0);
			} else if (Input.GetKeyDown ("up")) {
				m_animator.ResetTrigger ("idle");
				m_animator.SetTrigger ("walk");
				transform.localEulerAngles = new Vector3 (0, 90, 0);
			}

			float moveH = Input.GetAxis ("Horizontal");
			float moveV = Input.GetAxis ("Vertical");

			if (Input.GetKey ("up")) {
				transform.Translate (0, 0, moveV / 10);
			}
			if (Input.GetKey ("down")) {
				transform.Translate (0, 0, moveV / -10);
			}
			if (Input.GetKey ("left")) {
				transform.Translate (0, 0, moveH / -10);
			}
			if (Input.GetKey ("right")) {
				transform.Translate (0, 0, moveH / 10);
			}
		}

		if (Input.GetKeyDown ("space")) {
			// event screen show
			if (eventImage.activeSelf == true) {
				eventImage.SetActive (false);
				eventText.text = "";
			} else {
				float x = transform.position.x;
				float z = transform.position.z;

				if (x > -13.2 && x < -11.2 && z > -12.2 && z < -10.3 && eventRoot.transform.Find ("EventStarbucks").gameObject.activeSelf == true) {
					// starbucks
					eventRoot.transform.Find ("EventStarbucks").gameObject.SetActive (false);
					eventRoot.transform.Find ("EventStarbucksText").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image> ().overrideSprite = imageList [0];
					gameFlagList = gameFlagList.Substring (0, 6) + "1" + gameFlagList.Substring (7);
					ScoreSet (10);
					TimeSet (20);
					eventText.text = "你花了二十分钟舒舒服服的喝了杯咖啡！\n 心情+10\n按【SPACE】离开";
				} else if (x > -0.2 && x < 0.6 && z > -10.8 && z < -9.7 && eventRoot.transform.Find ("EventATM").gameObject.activeSelf == true) {
					// ATM
					eventRoot.transform.Find ("EventATM").gameObject.SetActive (false);
					eventRoot.transform.Find ("EventATMText").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image> ().overrideSprite = imageList [3];
					gameFlagList = gameFlagList.Substring(0, 7) + "1" + gameFlagList.Substring (8);
					ScoreSet (-20);
					TimeSet (20);
					eventText.text = "你花了整整二十分尝试在ATM取钱，但是发现你的卡里没有半毛钱！\n 心情-20\n按【SPACE】离开";
				} else if (x > -0.5 && x < 0.3 && z > -19.6 && z < -18.7 && eventRoot.transform.Find ("EventToilet").gameObject.activeSelf == true) {
					// Toilet
					eventRoot.transform.Find ("EventToilet").gameObject.SetActive (false);
					eventRoot.transform.Find ("EventToiletText").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image>().overrideSprite = imageList [1];
					gameFlagList = gameFlagList.Substring(0, 8) + "1" + gameFlagList.Substring (9);
					ScoreSet (1);
					TimeSet (5);
					eventText.text = "厕所！\n 心情+1";
				} else if (x > -7.8 && x < 8.5 && z > -2.9 && z < -2.1 && eventRoot.transform.Find ("EventPerform").gameObject.activeSelf == true) {
					// Perform
					eventRoot.transform.Find ("EventPerform").gameObject.SetActive (false);
					eventRoot.transform.Find ("EventPerformText").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image> ().overrideSprite = imageList [5];
					gameFlagList = gameFlagList.Substring(0, 9) + "1" + gameFlagList.Substring (10);
					ScoreSet (20);
					TimeSet (30);
					eventText.text = "你用了半个小时在CB的正中间卖艺，挣到了一笔大钱，心清气爽！\n 心情+20\n按【SPACE】离开";
				} else if (x > 3 && x < 3.9 && z > 13.8 && z < 14.8 && eventRoot.transform.Find ("EventElevator").gameObject.activeSelf == true) {
					// Elevator
					if (totalMinutes > 870 && totalMinutes <= 900) {
						// 14:30 to 15:00
						eventImage.SetActive (true);
						eventImage.GetComponent<Image> ().overrideSprite = imageList [4];
						ScoreSet (10);
						TimeFixSet (960);
						eventText.text = "和basis开会，解决了项目中的关键问题！\n 心情+10\n按【SPACE】离开";
					} else if (totalMinutes > 900 && totalMinutes < 960) {
						// 15:00 to 16:00
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [4];
						ScoreSet (-5);
						TimeFixSet (960);
						eventText.text = "这么重要的会，你居然迟到了！\n 心情-5\n按【SPACE】离开";
					} else {
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [2];
						TimeSet (10);
						eventText.text = "并没有来这里的必要。\n按【SPACE】离开";
					}
				} else if (x > -8.8 && x < -8 && z > -27 && z < -26.2) {
					// Corridor
					PlayerPrefs.SetInt ("Score", score);
					PlayerPrefs.SetInt ("Time", totalMinutes);
					PlayerPrefs.SetString ("gameFlagList", gameFlagList);
					PlayerPrefs.SetString ("FromPlace", "CB");
					SceneManager.LoadScene ("Corridor");
				}
			}
		}
	}

	void TimeSet(int addValue) {
		totalMinutes = totalMinutes + addValue;
		timeText.text = "时间" + (totalMinutes/60).ToString() + ":" +(totalMinutes % 60).ToString ();
	}

	void TimeFixSet(int fixValue) {
		totalMinutes = fixValue;
		timeText.text = "时间" + (totalMinutes/60).ToString() + ":" +(totalMinutes % 60).ToString ();
	}

	void ScoreSet(int addValue) {
		score = score + addValue;
		countText.text = "心情 : " + score.ToString ();
	}

	void gameFinished() {
		PlayerPrefs.SetInt ("Score", score);
		PlayerPrefs.SetInt ("Time", totalMinutes);
		PlayerPrefs.SetString ("gameFlagList", gameFlagList);
		SceneManager.LoadScene ("StartAndFin");
	}

}

