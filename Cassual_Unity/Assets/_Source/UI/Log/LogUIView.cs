using System.Collections;
using TMPro;
using UI.Log.Signals;
using UnityEngine;

namespace UI.Log
{
    public class LogUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI logUIText;
        [SerializeField] private float showTime;

        private LogMessageSignal _logMessageSignal;
        
        private void Awake()
        {
            _logMessageSignal = deVoid.Utils.Signals.Get<LogMessageSignal>();
            
            _logMessageSignal.AddListener(Log);

            logUIText.text = string.Empty;
        }

        private void Log(string message)
        {
            logUIText.text = message;
            
            StopAllCoroutines();
            StartCoroutine(DisappearMessage());
        }

        private IEnumerator DisappearMessage()
        {
            yield return new WaitForSeconds(showTime);
            logUIText.text = string.Empty;
        }
    }
}