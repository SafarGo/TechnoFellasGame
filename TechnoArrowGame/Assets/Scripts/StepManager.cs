using System;
using UnityEngine;
using UnityEngine.Events;

public class StepManager : MonoBehaviour
{
    public static StepManager Instance { get; private set; }

    [Serializable]
    public class Step
    {
        [Header("Help Object / Подсказка")]
        public GameObject helpObject;

        [Header("Audio / Озвучка")]
        public AudioSource audioSource;
        public AudioClip audioClip;

        [Header("Events")]
        public UnityEvent onStepStarted;
        public UnityEvent onStepCompleted;
    }

    [Header("Steps")]
    [SerializeField] private Step[] steps;

    [Header("Global Events")]
    public UnityEvent onScenarioCompleted;

    private int currentStepIndex = -1;
    private bool scenarioCompleted = false;

    public int CurrentStepIndex => currentStepIndex;
    public bool ScenarioCompleted => scenarioCompleted;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideAllHelpObjects();
        StartStep(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CompleteCurrentStep();

        if (Input.GetKeyDown(KeyCode.H))
            ShowCurrentHelp();

        if (Input.GetKeyDown(KeyCode.R))
            RestartSteps();
    }

    public void StartStep(int stepIndex)
    {
        if (steps == null || steps.Length == 0)
        {
            CompleteScenario();
            return;
        }

        if (stepIndex < 0 || stepIndex >= steps.Length)
        {
            CompleteScenario();
            return;
        }

        StopAllStepAudio();
        HideAllHelpObjects();

        currentStepIndex = stepIndex;
        scenarioCompleted = false;

        Step step = steps[currentStepIndex];

        step.onStepStarted?.Invoke();

        ShowCurrentHelp();
        PlayStepAudio(step);

        Debug.Log($"Step {currentStepIndex} started");
    }

    public void CompleteStep(int stepIndex)
    {
        if (scenarioCompleted)
            return;

        if (stepIndex != currentStepIndex)
        {
            Debug.Log($"Wrong step. Current: {currentStepIndex}, received: {stepIndex}");
            return;
        }

        CompleteCurrentStep();
    }

    public void CompleteCurrentStep()
    {
        if (scenarioCompleted)
            return;

        if (currentStepIndex < 0 || currentStepIndex >= steps.Length)
            return;

        Step step = steps[currentStepIndex];

        HideCurrentHelp();
        StopCurrentStepAudio();

        step.onStepCompleted?.Invoke();

        Debug.Log($"Step {currentStepIndex} completed");

        StartStep(currentStepIndex + 1);
    }

    public void ShowCurrentHelp()
    {
        if (currentStepIndex < 0 || currentStepIndex >= steps.Length)
            return;

        GameObject helpObject = steps[currentStepIndex].helpObject;

        if (helpObject != null)
            helpObject.SetActive(true);
    }

    public void HideCurrentHelp()
    {
        if (currentStepIndex < 0 || currentStepIndex >= steps.Length)
            return;

        GameObject helpObject = steps[currentStepIndex].helpObject;

        if (helpObject != null)
            helpObject.SetActive(false);
    }

    public void HideAllHelpObjects()
    {
        if (steps == null)
            return;

        foreach (Step step in steps)
        {
            if (step != null && step.helpObject != null)
                step.helpObject.SetActive(false);
        }
    }

    public bool IsCurrentStep(int stepIndex)
    {
        return currentStepIndex == stepIndex;
    }

    public void RestartSteps()
    {
        scenarioCompleted = false;

        StopAllStepAudio();
        HideAllHelpObjects();

        StartStep(0);
    }

    private void CompleteScenario()
    {
        scenarioCompleted = true;
        currentStepIndex = -1;

        StopAllStepAudio();
        HideAllHelpObjects();

        onScenarioCompleted?.Invoke();

        Debug.Log("Scenario completed");
    }

    private void PlayStepAudio(Step step)
    {
        if (step == null)
            return;

        if (step.audioSource == null)
            return;

        if (step.audioClip == null)
            return;

        step.audioSource.Stop();
        step.audioSource.clip = step.audioClip;
        step.audioSource.Play();

        Debug.Log($"Step {currentStepIndex} audio started: {step.audioClip.name}");
    }

    private void StopCurrentStepAudio()
    {
        if (currentStepIndex < 0 || currentStepIndex >= steps.Length)
            return;

        Step step = steps[currentStepIndex];

        if (step.audioSource != null)
            step.audioSource.Stop();
    }

    private void StopAllStepAudio()
    {
        if (steps == null)
            return;

        foreach (Step step in steps)
        {
            if (step != null && step.audioSource != null)
                step.audioSource.Stop();
        }
    }
}