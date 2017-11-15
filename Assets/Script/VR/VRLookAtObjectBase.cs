using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class VRLookAtObjectBase : MonoBehaviour
{
    VRInteractiveItem interactiveItem;       // Reference to the VRInteractiveItem to determine when to fill the bar.
    VRInput vrInput;                         // Reference to the VRInput to detect button presses.
    Slider uiSlider;

    public event System.Action<VRInput.SwipeDirection> ButtonSwipe;

    // Touch Input event
    public event System.Action ButtonDoubleClick;
    public event System.Action ButtonOnClick;
    public event System.Action ButtonOnCancel;
    public event System.Action ButtonOnDown;
    public event System.Action ButtonOnUp;

    // Look At Input event
    public event System.Action ButtonOnOver;
    public event System.Action ButtonOnOut;

    // Fill Finish event
    public event System.Action ButtonFillFinish;

    void OnEnable()
    {
        // Set Component
        uiSlider = GetComponentInChildren<Slider>();

        vrInput = VrCameraManager.instance.vrInput;
        if (vrInput != null)
        {
            // Swipe Action
            vrInput.OnSwipe += OnSwipe;

            // Touch Input
            vrInput.OnDoubleClick += OnDoubleClick;
            vrInput.OnClick += OnClick;
            vrInput.OnCancel += OnCancel;
            vrInput.OnDown += OnDown;
            vrInput.OnUp += OnUp;
        }

        interactiveItem = GetComponent<VRInteractiveItem>();
        if (interactiveItem != null)
        {
            // Look At Input
            interactiveItem.OnOver += OnOver;
            interactiveItem.OnOut += OnOut;
        }

        if (VrCameraManager.instance.useGoogleCardboard)
        {
            UnityEngine.EventSystems.EventTrigger et = gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();
            if (et != null)
            {
                AddEventTrigger(et, UnityEngine.EventSystems.EventTriggerType.PointerEnter, OnPointerEnter);
                AddEventTrigger(et, UnityEngine.EventSystems.EventTriggerType.PointerExit, OnPointerExit);
                //AddEventTrigger(et, UnityEngine.EventSystems.EventTriggerType.PointerDown, OnPointerDown);
                //AddEventTrigger(et, UnityEngine.EventSystems.EventTriggerType.PointerUp, OnPointerUp);

                useReticleOutline = false;
            }
        }
        //else
        //{
        //    GvrViewer.Instance.VRModeEnabled = true / false;
        //}

		InitSetting();
    }

    public void OnDisable()
    {
        // Touch Input
        if (vrInput != null)
        {
            vrInput.OnDoubleClick -= OnDoubleClick;
            vrInput.OnClick -= OnClick;
            vrInput.OnCancel -= OnCancel;
            vrInput.OnDown -= OnDown;
            vrInput.OnUp -= OnUp;
        }

        // Look At Input
        if (interactiveItem != null)
        {
            interactiveItem.OnOver -= OnOver;
            interactiveItem.OnOut -= OnOut;
        }

        InitSetting();
    }

    void InitSetting()
    {
        lookAtOver = false;
        pressed = false;

        StopFillSlider();
    }

    void Update()
    {
        if( VrCameraManager.instance.useGoogleCardboard)
            GvrViewer.Instance.UpdateState();

        if (lookAtOver == false)
        {
            return;
        }

        if (fillMode == FILLMODE.LOOKAT)
        {
            if (fillSliderRoutine == null)
            {
                fillSliderRoutine = StartCoroutine(FillBar());
            }
        }
        else if (fillMode == FILLMODE.LOOKATWITHTAP)
        {
            if (pressed)
            {
                if (fillSliderRoutine == null)
                {
                    fillSliderRoutine = StartCoroutine(FillBar());
                }
            }
            else
            {
                StopFillSlider();
            }
        }
    }

    // ============================== Touch Input

    bool pressed = false;

    public void OnSwipe(VRInput.SwipeDirection swipeDirection)
    {
        //Debug.Log("======== OnSwipe ==========");

        if (ButtonSwipe != null)
            ButtonSwipe(swipeDirection);
    }

    public void OnDoubleClick()
    {
        //Debug.Log("======== OnDoubleClick ==========");

        if (ButtonDoubleClick != null)
            ButtonDoubleClick();
    }

    public void OnCancel()
    {
        //Debug.Log("======== OnCancel ==========");

        if (ButtonOnCancel != null)
            ButtonOnCancel();
    }

    public void OnClick()
    {
        //Debug.Log("======== OnClick ==========" + Time.realtimeSinceStartup);

        if (ButtonOnClick != null)
            ButtonOnClick();
    }

    public void OnDown()
    {
        Debug.Log("======== OnDown ==========");
        pressed = true;

        if (ButtonOnDown != null)
            ButtonOnDown();
    }

    public void OnUp()
    {
        pressed = false;

        if (ButtonOnUp != null)
            ButtonOnUp();
        Debug.Log("======== OnUp ==========" + Time.realtimeSinceStartup);
    }

    // ============================== Look at Input

    [Header("Interaction Mode")]
    public FILLMODE fillMode = FILLMODE.LOOKATWITHTAP;

    Coroutine fillSliderRoutine;
    bool lookAtOver = false;
    public void OnOver()
    {
        lookAtOver = true;

        if (ButtonOnOver != null)
            ButtonOnOver();

        //Debug.Log("======== OnOver ==========");
    }

    public void OnOut()
    {
        lookAtOver = false;

        if (ButtonOnOut != null)
            ButtonOnOut();

        //Debug.Log("======== OnOut ==========");
        StopFillSlider();
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        OnOver();
    }

    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        OnOut();
    }

    //public void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    //{
    //    OnDown();
    //}

    //public void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    //{
    //    OnUp();
    //}

    public void FillFinish()
    {
        if (ButtonFillFinish != null)
            ButtonFillFinish();

        Debug.LogWarning("===== Fill MAX ==================");
    }

    [Tooltip("Only for Unity VR SDK")]
    public bool useReticleOutline = true;
    [Header("Look At Fill Time")]
    public float fillTime = 1.0f;

    IEnumerator FillBar()
    {
        float calcTime = 0.0f;

        while (calcTime < fillTime)
        {
            calcTime += Time.deltaTime;
            float fillRatio = calcTime / fillTime;
            SetSliderValue(fillRatio);

            yield return new WaitForEndOfFrame();
        }

        FillFinish();
    }

    void SetSliderValue(float sliderValue)
    {
        if (uiSlider)
            uiSlider.value = sliderValue;

        if(useReticleOutline)
            VrCameraManager.instance.SetReticleRadiusValue(sliderValue);
    }

    void StopFillSlider()
    {
        if (fillSliderRoutine != null)
            StopCoroutine(fillSliderRoutine);

        fillSliderRoutine = null;
        SetSliderValue(0f);
    }

    void AddEventTrigger(UnityEngine.EventSystems.EventTrigger et, UnityEngine.EventSystems.EventTriggerType eventType, System.Action<UnityEngine.EventSystems.PointerEventData> func)
    {
        UnityEngine.EventSystems.EventTrigger.Entry entry = new UnityEngine.EventSystems.EventTrigger.Entry();
        entry.eventID = eventType;
        et.triggers.Add(entry);
        entry.callback.AddListener((eventData) =>
        {
            func((UnityEngine.EventSystems.PointerEventData)eventData);
        });
    }
}
