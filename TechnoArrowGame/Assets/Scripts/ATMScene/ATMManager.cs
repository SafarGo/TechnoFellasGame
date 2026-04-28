using System.Collections;
using TMPro;
using UnityEngine;

public class ATMManager : MonoBehaviour
{
    private enum ATMState
    {
        WaitingForCard,
        EnteringPin,
        ChooseOperation,
        ChooseSum,
        TakeCash,
        Thanks
    }

    public AudioSource source;
    public AudioClip clip;

    [Header("Screens")]
    [SerializeField] private GameObject screenMain;              // Приложите карту
    [SerializeField] private GameObject screenPinCode;           // Введите ПИН
    [SerializeField] private GameObject screenChooseOperation;   // Выберите операцию
    [SerializeField] private GameObject screenChooseSum;         // Выберите сумму
    [SerializeField] private GameObject screenPutMoney;          // Заберите деньги
    [SerializeField] private GameObject screenThanks;            // Спасибо

    [Header("PIN")]
    [SerializeField] private string correctPin = "5269";
    [SerializeField] private TMP_Text pinText;

    [Header("Cash")]
    [SerializeField] private GameObject cashObject;

    [Header("Optional StepManager")]
    [SerializeField] private StepManager stepManager;
    [SerializeField] private int completeStepIndex = -1;

    [Header("Debug")]
    [SerializeField] private bool keyboardDebug = true;

    private ATMState currentState;
    private string currentPin = "";

    private void Start()
    {
        if (cashObject != null)
            cashObject.SetActive(false);

        ShowMainScreen();
    }
    private void HideAllScreens()
    {
        if (screenMain != null) screenMain.SetActive(false);
        if (screenPinCode != null) screenPinCode.SetActive(false);
        if (screenChooseOperation != null) screenChooseOperation.SetActive(false);
        if (screenChooseSum != null) screenChooseSum.SetActive(false);
        if (screenPutMoney != null) screenPutMoney.SetActive(false);
        if (screenThanks != null) screenThanks.SetActive(false);
    }

    public void ShowMainScreen()
    {
        HideAllScreens();

        currentState = ATMState.WaitingForCard;
        currentPin = "";
        UpdatePinText();

        if (cashObject != null)
            cashObject.SetActive(false);

        if (screenMain != null)
            screenMain.SetActive(true);

        Debug.Log("ATM: waiting for card");
    }

    public void OnCardPresented()
    {
        Debug.Log($"ATM: OnCardPresented called. State = {currentState}");

        if (currentState != ATMState.WaitingForCard)
        {
            Debug.Log("ATM: card ignored, ATM is not waiting for card");
            return;
        }

        ShowPinScreen();
    }

    private void ShowPinScreen()
    {
        HideAllScreens();

        currentState = ATMState.EnteringPin;
        currentPin = "";
        UpdatePinText();

        if (screenPinCode != null)
            screenPinCode.SetActive(true);

        Debug.Log("ATM: enter PIN");
    }

    public void PressDigit(int digit)
    {
        Debug.Log($"ATM: PressDigit({digit}) called. State = {currentState}, CurrentPin = {currentPin}");

        if (currentState != ATMState.EnteringPin)
        {
            Debug.Log("ATM: digit ignored, not in EnteringPin state");
            return;
        }

        if (currentPin.Length >= 4)
        {
            Debug.Log("ATM: PIN already has 4 digits");
            return;
        }

        currentPin += digit.ToString();
        UpdatePinText();

        Debug.Log("ATM PIN: " + currentPin);
    }

    public void ClearPin()
    {
        Debug.Log($"ATM: ClearPin called. State = {currentState}");

        if (currentState != ATMState.EnteringPin)
            return;

        currentPin = "";
        UpdatePinText();

        Debug.Log("ATM: PIN cleared");
    }

    public void SubmitPin()
    {
        Debug.Log($"ATM: SubmitPin called. State = {currentState}, CurrentPin = {currentPin}");

        if (currentState != ATMState.EnteringPin)
        {
            Debug.Log("ATM: submit ignored, not in EnteringPin state");
            return;
        }

        if (currentPin == correctPin)
        {
            Debug.Log("ATM: PIN correct");
            ShowChooseOperationScreen();
        }
        else
        {
            Debug.Log("ATM: wrong PIN, try again");

            currentPin = "";
            UpdatePinText();

            // Важно: остаёмся на этом же экране и в этом же состоянии
            currentState = ATMState.EnteringPin;

            if (screenPinCode != null && !screenPinCode.activeSelf)
                screenPinCode.SetActive(true);
        }
    }

    private void ShowChooseOperationScreen()
    {
        HideAllScreens();

        currentState = ATMState.ChooseOperation;

        if (screenChooseOperation != null)
            screenChooseOperation.SetActive(true);

        Debug.Log("ATM: choose operation");
    }

    public void OnWithdrawCashClicked()
    {
        Debug.Log($"ATM: OnWithdrawCashClicked called. State = {currentState}");

        if (currentState != ATMState.ChooseOperation)
        {
            Debug.Log("ATM: withdraw ignored, not in ChooseOperation state");
            return;
        }

        HideAllScreens();

        currentState = ATMState.ChooseSum;

        if (screenChooseSum != null)
            screenChooseSum.SetActive(true);

        Debug.Log("ATM: choose sum");
    }

    public void OnAmount100Clicked()
    {
        Debug.Log("ATM: amount 100 is disabled");
    }

    public void OnAmount500Clicked()
    {
        Debug.Log("ATM: amount 500 is disabled");
    }

    public void OnAmount1000Clicked()
    {
        Debug.Log("ATM: amount 1000 is disabled");
    }

    public void OnAmount5000Clicked()
    {
        Debug.Log($"ATM: OnAmount5000Clicked called. State = {currentState}");

        if (currentState != ATMState.ChooseSum)
        {
            Debug.Log("ATM: amount ignored, not in ChooseSum state");
            return;
        }

        StartCoroutine(Amount5000Routine());
    }

    private IEnumerator Amount5000Routine()
    {
        HideAllScreens();
        if (screenPutMoney != null)
            screenPutMoney.SetActive(true);

        if (source != null && clip != null)
            source.PlayOneShot(clip);

        yield return new WaitForSeconds(6f);
        currentState = ATMState.TakeCash;

        if (cashObject != null)
            cashObject.SetActive(true);

        Debug.Log("ATM: take cash");

    }

    public void OnCashGrabbed()
    {
        Debug.Log($"ATM: OnCashGrabbed called. State = {currentState}");

        if (currentState != ATMState.TakeCash)
        {
            Debug.Log("ATM: cash grab ignored, not in TakeCash state");
            return;
        }

        HideAllScreens();

        currentState = ATMState.Thanks;

        if (screenThanks != null)
            screenThanks.SetActive(true);

        Debug.Log("ATM: thanks");

        if (stepManager != null && completeStepIndex >= 0)
            stepManager.CompleteStep(completeStepIndex);
    }

    private void UpdatePinText()
    {
        if (pinText == null)
            return;

        pinText.text = new string('*', currentPin.Length);
    }
}