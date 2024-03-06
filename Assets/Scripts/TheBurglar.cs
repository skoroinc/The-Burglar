using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class TheBurglar : MonoBehaviour
{
    [SerializeField] private int _maxHiddenNumber;
    [SerializeField] private int _minHiddenNumber;
    public TMP_Text _numberPinFirstText;
    public TMP_Text _numberPinSecondText;
    public TMP_Text _numberPinThirdText;
    public TMP_Text _timerText;
    public GameObject _panelWinner;
    public GameObject _panelLoser;
    public UnityEngine.UI.Button _buttonResetNumber;
    public AudioSource soundWinnerPanel;
    public AudioSource soundLoserPanel;

    public GameObject[] _imagesPin;
    public GameObject[] _buttonsTools;
    public GameObject _panelTimer;

    private int _randomNumberFirstPin;
    private int _randomNumberSecondPin;
    private int _randomNumberThirdPin;
    private int _timer;
    

    void Start()
    {
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _timerText.text = _timer.ToString();
        _timer = 60;
        StartCoroutine(Countdown());
    }

    void GenerateRandomNumbers()
    {
        _randomNumberFirstPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberSecondPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberThirdPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
    }

    void ChangeNumberInPins()
    {
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
        _numberPinThirdText.text = _randomNumberThirdPin.ToString();
    }

    public void OnClickButtonRestart()
    {
        EnableGameObjects(_imagesPin);
        EnableGameObjects(_buttonsTools);
        _buttonResetNumber.interactable = true;
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _panelTimer.SetActive(true);
        _timerText.text = _timer.ToString();
        _panelWinner.SetActive(false);
        _panelLoser.SetActive(false);
        _timer = 60;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (_timer > 0)
        {
            _timer--;
            _timerText.text = _timer.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

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
    }

    public void OnClickResetNumber()
    {
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _buttonResetNumber.interactable = false;
        StartCoroutine(DelayAndEnableButton());
    }

    IEnumerator DelayAndEnableButton()
    {
        yield return new WaitForSeconds(20f);
        _buttonResetNumber.interactable = true;
    }
    void EnableGameObjects(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }

    void DisableGameObjects(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }

    void WinGame()
    {
        _panelWinner.SetActive(true);
        DisableGameObjects(_imagesPin);
        DisableGameObjects(_buttonsTools);
        _panelTimer.SetActive(false);
        soundWinnerPanel.Play();
        _timer = 0;
        StopCoroutine(Countdown());
    }

    void LoseGame()
    {
        _panelLoser.SetActive(true);
        DisableGameObjects(_imagesPin);
        DisableGameObjects(_buttonsTools);
        _panelTimer.SetActive(false);
        soundLoserPanel.Play();
    }

    void ResetGame()
    {
        _panelWinner.SetActive(false);
        _panelLoser.SetActive(false);
        EnableGameObjects(_imagesPin);
        EnableGameObjects(_buttonsTools);
        _panelTimer.SetActive(true);
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _timer = 60;
        StartCoroutine(Countdown());
    }

    void CheckWinCondition()
    {
        if (_randomNumberFirstPin == 5 && _randomNumberSecondPin == 5 && _randomNumberThirdPin == 5)
        {
            WinGame();
        }
        else if (_randomNumberFirstPin == 7 && _randomNumberSecondPin == 7 && _randomNumberThirdPin == 7)
        {
            WinGame();
        }
    }

    void CheckLoseCondition()
    {
        if (_timer == 0)
        {
            LoseGame();
        }
    }
    void Update()
    {
        CheckWinCondition();
        CheckLoseCondition();
    }
}

