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
    [SerializeField] private TMP_Text _numberPinFirstText;
    [SerializeField] private TMP_Text _numberPinSecondText;
    [SerializeField] private TMP_Text _numberPinThirdText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _panelWinner;
    [SerializeField] private GameObject _panelLoser;
    [SerializeField] private UnityEngine.UI.Button _buttonResetNumber;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private GameObject[] _imagesPin;
    [SerializeField] private GameObject[] _buttonsTools;
    [SerializeField] private GameObject _panelTimer;
    [SerializeField] private float _timerTime;
    private int _randomNumberFirstPin;
    private int _randomNumberSecondPin;
    private int _randomNumberThirdPin;
    

    
    private void Start()
    {
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _timerText.text = _timerTime.ToString();
        StartCoroutine(Countdown());
    }
    void Update()
    {
        
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
    public float GetTimerTime()
    {
        return _timerTime;
    }

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

    IEnumerator Countdown()
    {
        while (_timerTime > 0)
        {
            _timerTime--;
            _timerText.text = _timerTime.ToString();
            yield return new WaitForSeconds(1f);
        }
        LoseGame();
    }

    IEnumerator DelayAndEnableButton()
    {
        yield return new WaitForSeconds(20f);
        _buttonResetNumber.interactable = true;
    }
    private void CheckWinCondition()
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

    private void WinGame()
    {
        StopAllCoroutines();
        AudioSource.PlayClipAtPoint(winSound, transform.position);
        _panelWinner.SetActive(true);
        DisableGameObjects(_imagesPin);
        DisableGameObjects(_buttonsTools);
        _panelTimer.SetActive(false);
        
    }

    private void LoseGame()
    {
        _panelLoser.SetActive(true);
        DisableGameObjects(_imagesPin);
        DisableGameObjects(_buttonsTools);
        _panelTimer.SetActive(false);
        AudioSource.PlayClipAtPoint(loseSound, transform.position);
    }

   
    private void EnableGameObjects(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }

    private void DisableGameObjects(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}

