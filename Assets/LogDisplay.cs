using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
public class LogDisplay : MonoBehaviour
{
    private Text logText;
    // ログの最大個数
    [SerializeField] int m_MaxLogCount = 20;

    // ログの文字列を入れておくためのQueue
    Queue<string> m_LogMessages = new Queue<string>();

    // 文字列の結合
    StringBuilder m_StringBuilder = new StringBuilder();
 
    void Awake()
    {
        // Application.logMessageReceivedに関数を登録しておくと、
        // ログが出力される際に呼んでくれる
        Application.logMessageReceived += LogReceived;
        logText = GetComponent<Text>();
    }

    // ログが出力される際に呼んでもらう関数
    void LogReceived(string text, string stackTrace, LogType type)
    {
        // ログをQueueに追加
        m_LogMessages.Enqueue(text);

        // ログの個数が上限を超えていたら、最古のものを削除する
        while(m_LogMessages.Count > m_MaxLogCount)
        {
            m_LogMessages.Dequeue();
        }
    }
    void Update()
    {
        m_StringBuilder.Length = 0;
        foreach(string s in m_LogMessages)
        {
            m_StringBuilder.Append(s).Append(System.Environment.NewLine);
        }
        logText.text = m_StringBuilder.ToString();
    }
}