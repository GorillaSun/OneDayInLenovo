using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMoveH2 : MonoBehaviour {

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
		if (PlayerPrefs.GetString ("FromPlace") == "Meeting") {
			transform.position = new Vector3 (0, 0, 4);
			transform.localEulerAngles = new Vector3 (0, 270, 0);
		} else if (PlayerPrefs.GetString ("FromPlace") == "Working") {
			transform.position = new Vector3 (0, 0, -1);
			transform.localEulerAngles = new Vector3 (0, 270, 0);
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
				eventImage.GetComponent<Image>().overrideSprite = imageList [4];
				gameFlagList = gameFlagList.Substring(0, 5) + "2" + gameFlagList.Substring (6);
				ScoreSet (-30);
				eventText.text = "忘了吃中午饭吗，快饿扁了！\n 心情-30";
			}
			if (totalMinutes >= 1020 && gameFlagList.Substring (13, 1) == "0") {
				eventImage.SetActive (true);
				eventImage.GetComponent<Image>().overrideSprite = imageList [1];
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
				transform.Translate (0, 0, moveV / 30);
			}
			if (Input.GetKey ("down")) {
				transform.Translate (0, 0, moveV / -30);
			}
			if (Input.GetKey ("left")) {
				transform.Translate (0, 0, moveH / -30);
			}
			if (Input.GetKey ("right")) {
				transform.Translate (0, 0, moveH / 30);
			}
		}
		if (Input.GetKeyDown ("space")) {
			float x = transform.position.x;
			float z = transform.position.z;
			// event screen show
			if (eventImage.activeSelf == true) {
				eventImage.SetActive (false);
				eventText.text = "";
				if (x > -0.5 && x < 0.1 && z > 3.6 && z < 4 && totalMinutes > 570 && totalMinutes <= 690) {
					//meetingwithBT
					setPlayPrefsAndChangeScene ("MeetingWithBT");
				}
			} else {
				if (x > 4.8 && x < 5.5 && z > -7.3 && z < -6.7 && eventRoot.transform.Find ("EventMoney").gameObject.activeSelf == true) {
					// Money
					eventRoot.transform.Find ("EventMoney").gameObject.SetActive (false);
					eventImage.SetActive (true);
					eventImage.GetComponent<Image>().overrideSprite = imageList [2];
					ScoreSet (10);
					TimeSet (5);
					eventText.text = "捡到200元！\n 心情+10\n按【SPACE】离开";
				} else if (x > 1.1 && x < 1.5 && z > -1.6 && z < -1.2) {
					// Working
					if (totalMinutes > 690 && totalMinutes <= 810) {
						// 11:30 to 13:30
						eventImage.SetActive (true);
						TimeSet (5);
						eventText.text = "中午还是吃个饭，溜溜弯吧。\n按【SPACE】离开";
					} else {
						if (gameFlagList.Substring (11, 1) == "1") {
							eventImage.SetActive (true);
							eventImage.GetComponent<Image>().overrideSprite = imageList [1];
							eventText.text = "有点累，待会再来吧。\n按【SPACE】离开";
						} else {
							TimeSet (60);
							setPlayPrefsAndChangeScene ("Working");
						}
					}
				} else if (x > -2.6 && x < 3.1 && z > 1.5 && z < 1.9) {
					// VProom
					if (gameFlagList.Substring (10, 1) == "1") {
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [1];
						eventText.text = "什么人都没有。";
					} else {
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [0];
						gameFlagList = gameFlagList.Substring(0, 10) + "1" + gameFlagList.Substring (11);
						ScoreSet (20);
						TimeSet (30);
						eventText.text = "参与了一次惊天地泣鬼神的code review\n 心情+20\n按【SPACE】离开";
					}
				} else if (x > 4.8 && x < 5.4 && z > 7 && z < 7.6 && eventRoot.transform.Find ("EventBoss").gameObject.activeSelf == true) {
					// Boss
					if (totalMinutes >= 960 && totalMinutes < 1050) {
						// 16:00 to 17:30
						eventImage.SetActive (true);
						TimeSet (5);
						eventImage.GetComponent<Image>().overrideSprite = imageList [1];
						eventText.text = "五点半再来回报工作吧。\n按【SPACE】离开";
					} else if (totalMinutes >= 1050) {
						// 17:30
						eventRoot.transform.Find ("EventBoss").gameObject.SetActive (false);
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [3];
						ScoreSet (20);
						TimeSet (10);
						eventText.text = "你花了十分钟回报了今天精彩的工作，得到了文迪的夸奖！\n 心情+20\n按【SPACE】离开";
					} else {
						eventImage.SetActive (true);
						TimeSet (5);
						eventImage.GetComponent<Image>().overrideSprite = imageList [1];
						eventText.text = "并没有来这里的必要。";
					}
				} else if (x > -0.6 && x < -0.1 && z > -9.3 && z < -8.8) {
					// Corridor
					setPlayPrefsAndChangeScene ("Corridor");
				} else if (x > -0.5 && x < 0.1 && z > 3.6 && z < 4) {
					// MeetingWithBT
					if (totalMinutes <= 570) {
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [1];
						eventText.text = "会议还没有开始。";
					} else if (totalMinutes > 570 && totalMinutes <= 600) {
						// 9:30 to 10:00
						TimeFixSet (690);
						setPlayPrefsAndChangeScene ("MeetingWithBT");
 					} else if (totalMinutes > 600 && totalMinutes <= 690) {
						// 10:00 to 11:30
						eventImage.SetActive (true);
						eventImage.GetComponent<Image>().overrideSprite = imageList [1];
						ScoreSet (-10);
						TimeFixSet (690);
						eventText.text = "这么重要的会，居然还迟到！\n 心情-10\n按【SPACE】离开";
					} else {
						eventImage.SetActive (true);
						TimeSet (5);
						eventImage.GetComponent<Image>().overrideSprite = imageList [1];
						eventText.text = "并没有来这里的必要。\n按【SPACE】离开";
					}
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

	void setPlayPrefsAndChangeScene(string place) {
		PlayerPrefs.SetInt ("Score", score);
		PlayerPrefs.SetInt ("Time", totalMinutes);
		PlayerPrefs.SetString ("gameFlagList", gameFlagList);
		PlayerPrefs.SetString ("FromPlace", "H2");
		SceneManager.LoadScene (place);
	}

	void gameFinished() {
		PlayerPrefs.SetInt ("Score", score);
		PlayerPrefs.SetInt ("Time", totalMinutes);
		PlayerPrefs.SetString ("gameFlagList", gameFlagList);
		SceneManager.LoadScene ("StartAndFin");
	}

}

