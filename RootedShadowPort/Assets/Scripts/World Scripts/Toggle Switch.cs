using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [Header("Toggle Switch Settings")]
    [SerializeField, Range(0,1f)] private float switchValue = 0f; // 0 = off, 1 = on

    public bool CurrentValue { get; private set; }
    private bool previousValue;
    private Slider slider;

    [Header("Animations")]
    [SerializeField, Range(0, 1f)] private float animationSpeed = 0.5f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine animationSlider;

    [Header("Events")]
    [SerializeField] private UnityEvent onSwitchOn;
    [SerializeField] private UnityEvent onSwitchOff;

    protected void OnValidate()
    {
        SetupToggleComponents();
        slider.value = switchValue;
    }

    private void SetupToggleComponents()
    {
        if (slider != null) 
        {
            return;

        }

        SetupSliderComponents();
    }
    private void SetupSliderComponents()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("No Slider component found on the GameObject.");
            return;
        }
        slider.interactable = false; // Disable interaction with the slider
        var sliderColors = slider.colors;
        sliderColors.disabledColor = Color.white; // Set the disabled color to white
        slider.colors = sliderColors;
        slider.transition = Selectable.Transition.None; // Disable the transition effect

    }

    void Awake()
    {
        SetupToggleComponents();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    public void Toggle()
    {
        SetStateAndStartAnim(!CurrentValue);
    }
    private void SetStateAndStartAnim(bool state)
    {
        previousValue = CurrentValue;
        CurrentValue = state;
        if (CurrentValue)
        {
            onSwitchOn?.Invoke();
        }
        else
        {
            onSwitchOff?.Invoke();
        }
        if (animationSlider != null)
        {
            StopCoroutine(animationSlider);
        }
        animationSlider = StartCoroutine(AnimationSlider());
    }

    private IEnumerator AnimationSlider()
    {
        float startValue = slider.value;
        float targetValue = CurrentValue ? 1f : 0f;
        float elapsedTime = 0f;
        while (elapsedTime < animationSpeed)
        {
            elapsedTime += Time.deltaTime;
            float lerpfactor = slideEase.Evaluate(elapsedTime / animationSpeed);
            slider.value = Mathf.Lerp(startValue, targetValue, lerpfactor);
            yield return null;
        }
        slider.value = targetValue; // Ensure the final value is set
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
