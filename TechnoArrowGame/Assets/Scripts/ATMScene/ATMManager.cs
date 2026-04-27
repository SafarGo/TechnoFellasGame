using TMPro;
using UnityEngine;

public class ATMManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject bringCardScreen;
    [SerializeField] private GameObject pinScreen;
    [SerializeField] private GameObject amountScreen;
    [SerializeField] private GameObject cashReadyScreen;

    [Header("PIN")]
    [SerializeField] private string correctPin = "6752";
    [SerializeField] private TMP_Text pinText;

    [Header("Cash")]
    [SerializeField] private GameObject cashObject;

    [Header("Optional Step Manager")]
    [SerializeField] private StepManager stepManager;
    [SerializeField] private int completeStepIndex = -1;

    private string currentPin = "";
    private bool isWaitingForCard = false;
    private bool isEnteringPin = false;

    private void Start()
    {
        HideAllScreens();
        cashObject.SetActive(false);
        ShowMainMenu();
    }

    private void HideAllScreens()
    {
        mainMenuScreen.SetActive(false);
        bringCardScreen.SetActive(false);
        pinScreen.SetActive(false);
        amountScreen.SetActive(false);
        cashReadyScreen.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllScreens();

        isWaitingForCard = false;
        isEnteringPin = false;
        currentPin = "";
        UpdatePinText();

        if (mainMenuScreen != null)
            mainMenuScreen.SetActive(true);
    }

    public void StartWithdrawCash()
    {
        HideAllScreens();

        isWaitingForCard = true;
        isEnteringPin = false;
        currentPin = "";
        UpdatePinText();

        if (bringCardScreen != null)
            bringCardScreen.SetActive(true);

        Debug.Log("ATM: Поднесите карту");
    }

    public void OnCardPresented()
    {
        if (!isWaitingForCard)
            return;

        HideAllScreens();

        isWaitingForCard = false;
        isEnteringPin = true;
        currentPin = "";
        UpdatePinText();

        if (pinScreen != null)
            pinScreen.SetActive(true);

        Debug.Log("ATM: Введите ПИН-код");
    }

    public void PressDigit(int digit)
    {
        if (!isEnteringPin)
            return;

        if (currentPin.Length >= 4)
            return;

        currentPin += digit.ToString();
        UpdatePinText();

        Debug.Log("ATM PIN: " + currentPin);
    }

    public void ClearPin()
    {
        if (!isEnteringPin)
            return;

        currentPin = "";
        UpdatePinText();
    }

    public void SubmitPin()
    {
        if (!isEnteringPin)
            return;

        if (currentPin == correctPin)
        {
            ShowAmountScreen();
        }
        else
        {
            Debug.Log("ATM: Неверный ПИН");

            currentPin = "";
            UpdatePinText();
        }
    }

    private void ShowAmountScreen()
    {
        HideAllScreens();

        isEnteringPin = false;

        if (amountScreen != null)
            amountScreen.SetActive(true);

        Debug.Log("ATM: Выберите сумму");
    }

    public void SelectAmount(int amount)
    {
        if (amount != 5000)
        {
            Debug.Log("ATM: Эта сумма сейчас недоступна");
            return;
        }

        GiveCash();
    }

    private void GiveCash()
    {
        HideAllScreens();

        if (cashObject != null)
            cashObject.SetActive(true);

        if (cashReadyScreen != null)
            cashReadyScreen.SetActive(true);

        Debug.Log("ATM: Заберите деньги");
    }

    public void ExitTerminal()
    {
        ShowMainMenu();

        Debug.Log("ATM: Выход из терминала");

        if (stepManager != null && completeStepIndex >= 0)
        {
            stepManager.CompleteStep(completeStepIndex);
        }
    }

    private void UpdatePinText()
    {
        if (pinText == null)
            return;

        pinText.text = new string('*', currentPin.Length);
    }
}