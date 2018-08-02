using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Welcome : MonoBehaviour {

	public Text welcomeText;

	private int totalMinutes;
	private int score;

	/*
		1	Corridor	showing
		2	Corridor	fishing
		3	Corridor	YD
		4	Corridor	YY
		5	Corridor	SN
		6	Corridor	have eating	0:not	1:ate	2:msgshow
		7	CB			starbucks
		8	CB			ATM
		9	CB			toilet
		10	CB			perform
		11	H2			VProom
		12	H2			working
		13	H2			meetingWithBT
		14	EndNotice
		15	reserve
	*/
	private string gameFlagList;

	// Use this for initialization
	void Start () {

		totalMinutes = PlayerPrefs.GetInt ("Time");
		score = PlayerPrefs.GetInt ("Score");

		if (totalMinutes < 1080) {
			// 6:00
			welcomeText.text = "欢迎来到Lenovo！\n\n" +
				"你将会体验Lenovo的IT部门员工的一天，\n " +
				"在一天结束时，会根据你的心情获得相应的奖励。\n\n" +
				"对了，你在早晨10点需要在H203和BT同事开会讨论需求，\n" + 
				"下午3点在圆楼4楼和Basis同事有solution讨论，\n" + 
				"切记不能迟到，会影响心情呦。\n" +
				"另外别忘了在工位H2-07F完成今天的工作，\n" +
				"下午5点半去H2-12J找文迪汇报工作。\n" +
				"完美的一天会在6点半结束。\n\n" +
				"Tips1: 在黄色方块上按【SPACE】会有好事情发生\n" +
				"Tips2: 如果你看到这些字很小，还是从新调整分辨率再来吧\n\n" +
				"按【SPACE】开始精彩的一天吧！";
		} else {
			gameFlagList = PlayerPrefs.GetString ("gameFlagList");
			if (score > 160) {
				welcomeText.text = "心情值 ： " + score.ToString() + "\n" +
					finisheText(gameFlagList) +
					"\n完美的一天\n你简直就是IT的精英！\n\n";
			} else if (score > 120) {
				welcomeText.text = "心情值 ： " + score.ToString() + "\n" + 
					finisheText(gameFlagList) +
					"平常的一天\n要对生活充满希望啊，再来一次吧\n\n";
			} else {
				welcomeText.text = "心情值 ： " + score.ToString() +  "\n" +
					finisheText(gameFlagList) +
					"失败的一天\n再来一次游戏吧。。。。。。\n\n";
			}
			welcomeText.text = welcomeText.text +
				"\n\n截屏发给下面的邮箱换取奖励吧\n"+
				"sunning2@lenovo.com\n"+
				"密码 : " + gameFlagList;
			PlayerPrefs.SetInt ("Time", 540);
			PlayerPrefs.SetInt ("score", 30);
			PlayerPrefs.SetString ("gameFlagList", "000000000000000");
			PlayerPrefs.SetString ("FromPlace", "Welcome");
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") && totalMinutes < 1080) {
			PlayerPrefs.SetInt ("Time", 540);
			PlayerPrefs.SetInt ("score", 30);
			PlayerPrefs.SetString ("gameFlagList", "000000000000000");
			PlayerPrefs.SetString ("FromPlace", "Welcome");
			SceneManager.LoadScene ("Corridor");
		}
	}
	string finisheText(string gameFlagList) {
		string text = "";
		if (gameFlagList.Substring (0, 1) == "1") {
			text = text + "洗了澡,";
		}
		if (gameFlagList.Substring (1, 1) == "1") {
			text = text + "钓了鱼，";
		}
		if (gameFlagList.Substring (2, 1) == "1") {
			text = text + "和若东探讨了技术，";
		}
		if (gameFlagList.Substring (3, 1) == "1") {
			text = text + "和元庆聊了趋势，";
		}
		if (gameFlagList.Substring (4, 1) == "1") {
			text = text + "和宁谈了人生，";
		}
		if (gameFlagList.Substring (5, 1) == "1") {
			text = text + "吃了中午饭，";
		}
		if (gameFlagList.Substring (6, 1) == "1") {
			text = text + "喝了咖啡，";
		}
		if (gameFlagList.Substring (9, 1) == "1") {
			text = text + "练了胸口碎大石，";
		}
		if (gameFlagList.Substring (10, 1) == "1") {
			text = text + "review了code，";
		}
		if (gameFlagList.Substring (11, 1) == "1") {
			text = text + "完成了工作，";
		}
		if (gameFlagList.Substring (12, 1) == "1") {
			text = text + "和业务开了会，";
		}
		return text;
	}

}

