using UnityEngine;
using System.Collections;

public class TestTongXing : MonoBehaviour {

	private A aa = null;
	
	void Start () {
		// 注册观察者,注册后才会接受消息，对消息进行对应的处理
		NotificationCenter.DafaultCenter().AddObserver(this, "printShow"); 
	}
	
	void printShow(Notification note) {
		Debug.Log(transform + "从," + note.sender + "," + "接收一个信息内容:" + note.data + ",通知名称为:" + note.name);
		aa = ((TestTongXing_source)(note.data)).getA();
		transform.position += new Vector3 (3, 0, 0);
		Debug.Log("i = " + aa.i + ", j = " + aa.j);
	}
}
