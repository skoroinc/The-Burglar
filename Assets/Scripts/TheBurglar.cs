using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TheBurglar : MonoBehaviour
{
    
    private const int VictoryNumber1 = 5;
    private const int VictoryNumber2 = 7;
    private const int MinPinValue = 0;
    private const int MaxPinValue = 10;
    private const float ResetButtonCooldown = 20f;
    

    
    [SerializeField] private TMP_Text _numberPinFirstText;
    [SerializeField] private TMP_Text _numberPinSecondText;
    [SerializeField] private TMP_Text _numberPinThirdText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _panelWinner;
    [SerializeField] private GameObject _panelLoser;
    [SerializeField] private Button _buttonResetNumber;
    [SerializeField] private GameObject[] _imagesPin;
    [SerializeField] private GameObject[] _buttonsTools;
    [SerializeField] private GameObject _panelTimer;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private float _timerTime;
    

    
    private AudioSource _audioSource;
    private int _randomNumberFirstPin;
    private int _randomNumberSecondPin;
    private int _randomNumberThirdPin;
    private float _remainingTime;
    private Coroutine _countdownCoroutine;
    

    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        RestartGame();
    }
    

    
    public void OnClickButtonDrill()
    {
        UpdatePinValues(1, -1, 0);
    }

    public void OnClickButtonHammer()
    {
        UpdatePinValues(-1, 2, -1);
    }

    public void OnClickButtonLockPick()
    {
        UpdatePinValues(-1, 1, 1);
    }

    public void OnClickResetNumber()
    {
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _buttonResetNumber.interactable = false;
        StartCoroutine(EnableButtonAfterDelay());
    }

    public void OnClickButtonRestart()
    {
        RestartGame();
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    

    
    private void RestartGame()
    {
        EnableGameObjects(_imagesPin);
        EnableGameObjects(_buttonsTools);
        _buttonResetNumber.interactable = true;
        GenerateRandomNumbers();
        ChangeNumberInPins();
        _panelWinner.SetActive(false);
        _panelLoser.SetActive(false);
        _panelTimer.SetActive(true);
        _timerText.text = _timerTime.ToString();
        if (_countdownCoroutine != null) StopCoroutine(_countdownCoroutine);
        _countdownCoroutine = StartCoroutine(Countdown());
    }

    private void UpdatePinValues(int firstDelta, int secondDelta, int thirdDelta)
    {
        _randomNumberFirstPin = Mathf.Clamp(_randomNumberFirstPin + firstDelta, MinPinValue, MaxPinValue);
        _randomNumberSecondPin = Mathf.Clamp(_randomNumberSecondPin + secondDelta, MinPinValue, MaxPinValue);
        _randomNumberThirdPin = Mathf.Clamp(_randomNumberThirdPin + thirdDelta, MinPinValue, MaxPinValue);

        ChangeNumberInPins();
        CheckWinCondition();
    }

    private void GenerateRandomNumbers()
    {
        _randomNumberFirstPin = Random.Range(MinPinValue, MaxPinValue + 1);
        _randomNumberSecondPin = Random.Range(MinPinValue, MaxPinValue + 1);
        _randomNumberThirdPin = Random.Range(MinPinValue, MaxPinValue + 1);
    }

    private void ChangeNumberInPins()
    {
        _numberPinFirstText.text = _randomNumberFirstPin.ToString();
        _numberPinSecondText.text = _randomNumberSecondPin.ToString();
        _numberPinThirdText.text = _randomNumberThirdPin.ToString();
    }

    private void CheckWinCondition()
    {
        if ((_randomNumberFirstPin == VictoryNumber1 && _randomNumberSecondPin == VictoryNumber1 && _randomNumberThirdPin == VictoryNumber1) ||
            (_randomNumberFirstPin == VictoryNumber2 && _randomNumberSecondPin == VictoryNumber2 && _randomNumberThirdPin == VictoryNumber2))
        {
            WinSequence();
        }
    }

    private void WinSequence()
    {
        _panelWinner.SetActive(true);
        _panelTimer.SetActive(false);
        if (_countdownCoroutine != null) StopCoroutine(_countdownCoroutine);
        PlaySound(_winSound);
    }

    private IEnumerator Countdown()
    {
        _remainingTime = _timerTime;

        while (_remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            _remainingTime--;
            _timerText.text = _remainingTime.ToString();
        }

        LoseSequence();
    }

    private void LoseSequence()
    {
        _panelLoser.SetActive(true);
        _panelTimer.SetActive(false);
        PlaySound(_loseSound);
    }

    private void EnableGameObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator EnableButtonAfterDelay()
    {
        yield return new WaitForSeconds(ResetButtonCooldown);
        _buttonResetNumber.interactable = true;
    }

    private void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
    
}
