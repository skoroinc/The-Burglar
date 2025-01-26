using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TheBurglar : MonoBehaviour
{
    #region Singleton Implementation
    private static TheBurglar instance;

    public static TheBurglar Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<TheBurglar>();
                if (instance == null)
                {
                    Debug.LogError("An instance of TheBurglar is needed in the scene.");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private int _maxHiddenNumber;
    [SerializeField] private int _minHiddenNumber;
    [SerializeField] private TMP_Text _numberPinFirstText;
    [SerializeField] private TMP_Text _numberPinSecondText;
    [SerializeField] private TMP_Text _numberPinThirdText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _panelWinner;
    [SerializeField] private GameObject _panelLoser;
    [SerializeField] private Button _buttonResetNumber;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private GameObject[] _imagesPin;
    [SerializeField] private GameObject[] _buttonsTools;
    [SerializeField] private GameObject _panelTimer;
    [SerializeField] private float _timerTime;

    private AudioSource soundWinnerPanel;
    private AudioSource soundLoserPanel;

    private int _randomNumberFirstPin;
    private int _randomNumberSecondPin;
    private int _randomNumberThirdPin;

    private float remainingTime;

    private void Start()
    {
        soundWinnerPanel = GetComponent<AudioSource>();
        soundLoserPanel= GetComponent<AudioSource>();
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _timerText.text = _timerTime.ToString();
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        
    }

    #region Game Logic Methods
    public void OnClickButtonDrill()
    {
        if (_randomNumberFirstPin != 10)
        {
            _randomNumberFirstPin++;
        }
        if (_randomNumberSecondPin != 0)
        {
            _randomNumberSecondPin--;
        }
        ChangeNumberInPins();
        CheckWinCondition();
    }

    public void OnClickButtonHammer()
    {
        if (_randomNumberFirstPin != 0)
        {
            _randomNumberFirstPin--;
        }
        if (_randomNumberSecondPin != 10)
        {
            if (_randomNumberSecondPin == 9)
            {
                _randomNumberSecondPin++;
            }
            else
            {
                _randomNumberSecondPin += 2;
            }
        }
        if (_randomNumberThirdPin != 0)
        {
            _randomNumberThirdPin--;
        }
        ChangeNumberInPins();
        CheckWinCondition();
    }

    public void OnClickButtonLockPick()
    {
        if (_randomNumberFirstPin != 0)
        {
            _randomNumberFirstPin--;
        }
        if (_randomNumberSecondPin != 10)
        {
            _randomNumberSecondPin++;
        }
        if (_randomNumberThirdPin != 10)
        {
            _randomNumberThirdPin++;
        }
        ChangeNumberInPins();
        CheckWinCondition();
    }

    public void OnClickResetNumber()
    {
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _buttonResetNumber.interactable = false;
        StartCoroutine(DelayAndEnableButton());
    }

    public void OnClickButtonRestart()
    {
        EnableGameObjects(_imagesPin);
        EnableGameObjects(_buttonsTools);
        _buttonResetNumber.interactable = true;
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _panelTimer.SetActive(true);
        _timerText.text = GetTimerTime().ToString();
        _panelWinner.SetActive(false);
        _panelLoser.SetActive(false);
        StartCoroutine(Countdown());
    }
    #endregion

    #region Helper Methods
    private void GenerateRandomNumbers()
    {
        _randomNumberFirstPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberSecondPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberThirdPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
    }

    private void ChangeNumberInPins()
    {
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
        _numberPinThirdText.text = _randomNumberThirdPin.ToString();
    }

    private void CheckWinCondition()
    {
        if (( _randomNumberFirstPin == 5 && _randomNumberSecondPin == 5 && _randomNumberThirdPin == 5) ||
            (_randomNumberFirstPin == 7 && _randomNumberSecondPin == 7 && _randomNumberThirdPin == 7))
        {
            WinSequence();
        }
    }

    private void WinSequence()
    {
        _panelWinner.SetActive(true);
        _panelLoser.SetActive(false);
        StopAllCoroutines();
        PlayWinSound();
    }

    private void CheckLoseCondition()
    {
        _panelLoser.SetActive(true); 
        _panelWinner.SetActive(false); 
        _panelTimer.SetActive(false); 
        _buttonResetNumber.interactable = true; 
        PlayLoseSound(); 
    }

    private void PlayWinSound()
    {
        soundWinnerPanel.PlayOneShot(winSound);
    }

    private void PlayLoseSound()
    {
        soundLoserPanel.PlayOneShot(loseSound);
    }

    private IEnumerator Countdown()
    {
        remainingTime = _timerTime;

        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
            _timerText.text = remainingTime.ToString();
        }

        CheckLoseCondition();
        
    }

    private IEnumerator DelayAndEnableButton()
    {
        
        yield break;
    }

    private void EnableGameObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }

    private float GetTimerTime()
    {
        
        return _timerTime;
    }
    #endregion
}