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


    public GameObject imagePinFirst;
    public GameObject imagePinSecond;
    public GameObject imagePinThird;
    public GameObject _buttonDrill;
    public GameObject _buttonHammer;
    public GameObject _buttonLockPick;
    public GameObject _buttonDynamite;
    public GameObject panelTimer;



    private int _randomNumberFirstPin;
    private int _randomNumberSecondPin;
    private int _randomNumberThirdPin;
    private int _timer;
    

    void Start()
    {
        _randomNumberFirstPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberSecondPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberThirdPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
        _numberPinThirdText.text = _randomNumberThirdPin.ToString();
        _timerText.text = _timer.ToString();
        _timer = 60;
        StartCoroutine(Countdown());
    }

    public void OnClickButtonRestart()
    {
        imagePinFirst.SetActive(true);
        imagePinSecond.SetActive(true);
        imagePinThird.SetActive(true);
        _buttonDrill.SetActive(true);
        _buttonHammer.SetActive(true);
        _buttonLockPick.SetActive(true);
        _buttonDynamite.SetActive(true);
        panelTimer.SetActive(true);
        _buttonResetNumber.interactable = true;
        _randomNumberFirstPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberSecondPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberThirdPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
        _numberPinThirdText.text = _randomNumberThirdPin.ToString();
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
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
       
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
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
        _numberPinThirdText.text = _randomNumberThirdPin.ToString();
        
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
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
        _numberPinThirdText.text = _randomNumberThirdPin.ToString();
        
    }

    public void OnClickResetNumber()
    {
        _randomNumberFirstPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberSecondPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _randomNumberThirdPin = Random.Range(_minHiddenNumber, _maxHiddenNumber + 1);
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
        _numberPinThirdText.text = _randomNumberThirdPin.ToString();
        _buttonResetNumber.interactable = false;
        StartCoroutine(DelayAndEnableButton());
    }

    IEnumerator DelayAndEnableButton()
    {
        yield return new WaitForSeconds(20f);
        _buttonResetNumber.interactable = true;
    }


    void Update()
    {
        if (_randomNumberFirstPin == 5 && _randomNumberSecondPin == 5 && _randomNumberThirdPin == 5)
        {
            _panelWinner.SetActive(true);
            imagePinFirst.SetActive(false);
            imagePinSecond.SetActive(false);
            imagePinThird.SetActive(false);
            _buttonDrill.SetActive(false);
            _buttonHammer.SetActive(false);
            _buttonLockPick.SetActive(false);
            _buttonDynamite.SetActive(false);
            panelTimer.SetActive(false);
            soundWinnerPanel.Play();
            _timer = 0;
            StopCoroutine(Countdown());
        }
        else if (_randomNumberFirstPin == 7 && _randomNumberSecondPin == 7 && _randomNumberThirdPin == 7)
        {
            _panelWinner.SetActive(true);
            imagePinFirst.SetActive(false);
            imagePinSecond.SetActive(false);
            imagePinThird.SetActive(false);
            _buttonDrill.SetActive(false);
            _buttonHammer.SetActive(false);
            _buttonLockPick.SetActive(false);
            _buttonDynamite.SetActive(false);
            panelTimer.SetActive(false);
            soundWinnerPanel.Play();
            _timer = 0;
            StopCoroutine(Countdown());
        }
        else if (_timer == 0)
        {
            _panelLoser.SetActive(true);
            imagePinFirst.SetActive(false);
            imagePinSecond.SetActive(false);
            imagePinThird.SetActive(false);
            _buttonDrill.SetActive(false);
            _buttonHammer.SetActive(false);
            _buttonLockPick.SetActive(false);
            _buttonDynamite.SetActive(false);
            panelTimer.SetActive(false);
            soundLoserPanel.Play();
        }
    }
}

