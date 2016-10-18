using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LogViewer : MonoSingleton<LogViewer>
{
    // 顺序不能变
    private static readonly string[] ColorFmt = new string[] {
        "<color=#FF0000><i>{0:D3}$</i> {1}\n{2}</color>", // Error
		"<color=#FF00FF><i>{0:D3}$</i> {1}\n{2}</color>", // Assert
		"<color=#FFFF00><i>{0:D3}$</i> {1}</color>", // Warning
		"<i>{0:D3}$</i> {1}", // Log
		"<color=#FF00FF><i>{0:D3}$</i> {1}\n{2}</color>", //Exception
	};

    public GameObject root;    
    public Transform grpText;
    public int Kcapacity = 64;

    private ScrollRect scrollRect;
    private System.Text.StringBuilder logBuilder;
    private Text m_LogContent;
    private int counting;
    private bool hasUpdate;

    private GameObject entText;
    private List<GameObject> listText = new List<GameObject>();

    protected override void Awaking()
    {
        Application.logMessageReceived += logMessageReceived;
        logBuilder = new System.Text.StringBuilder(1024 * Kcapacity);
        counting = 0;
        hasUpdate = false;

        entText = grpText.GetChild(0).gameObject;
        GameObject newText = GoTools.AddChild(grpText.gameObject, entText);
        newText.name = "entText1";
        newText.SetActive(true);
        m_LogContent = newText.GetComponent<Text>();
        listText.Add(newText);
    }

    private void Start()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        root.SetActive(false);
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= logMessageReceived;
    }

    private void OnEnable()
    {
        if (m_LogContent) {
            m_LogContent.text = logBuilder.ToString();
        }
        if (scrollRect) {
            scrollRect.verticalNormalizedPosition = 0;
        }
    }

    private bool multiTouched = false;
    private Vector2 m_TouchBegan, m_TouchEnd;
    private void processTouch()
    {
        if (multiTouched) {
            if (Input.touchCount > 0) {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended) {
                    m_TouchEnd = touch.position;
                }
            } else {
                var vector = m_TouchEnd - m_TouchBegan;
                var distance = vector.magnitude;
                if (distance > 100) {
                    var dot = Vector3.Dot(Vector3.up, vector.normalized);
                    if (dot < -0.8f) {
                        root.SetActive(true);
                    } else if (dot > 0.8f) {
                        root.SetActive(false);
                    }
                }
                multiTouched = false;
            }
        } else if (Input.touchCount == 3) {
            multiTouched = true;
            m_TouchBegan = Input.GetTouch(0).position;
        }
    }

    private void LateUpdate ()
    {
        if (hasUpdate && root.activeSelf) {
            hasUpdate = false;
            m_LogContent.text = logBuilder.ToString();
            scrollRect.verticalNormalizedPosition = 0;
        }
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyUp(KeyCode.Tab)) {
            root.SetActive(!root.activeSelf);
        }
        processTouch();
#elif UNITY_IOS || UNITY_ANDROID
        processTouch();
#endif
    }

    private void logMessageReceived(string condition, string stackTrace, LogType logType)
    {
        counting += 1;
        string toAppend = null;
        switch (logType) {
            case LogType.Log:
            case LogType.Warning:
                toAppend = string.Format(ColorFmt[(int)logType], counting, condition);
                break;
            case LogType.Assert:
            case LogType.Error:
            case LogType.Exception:
                toAppend = string.Format(ColorFmt[(int)logType], counting, condition, stackTrace);
                break;
            default:
                break;
        }
        var logLength = logBuilder.Length + toAppend.Length;
        if (logLength >= logBuilder.Capacity) {
            m_LogContent.text = m_LogContent.text.Remove(m_LogContent.text.Length - 1);
            logBuilder = new System.Text.StringBuilder(Kcapacity * 1024);
            GameObject newText = GoTools.AddChild(grpText.gameObject, entText);
            newText.name = "entText" + (listText.Count + 1);
            newText.SetActive(true);
            m_LogContent = newText.GetComponent<Text>();
            listText.Add(newText);
        }
        logBuilder.AppendLine(toAppend);
        hasUpdate = true;
    }
}
