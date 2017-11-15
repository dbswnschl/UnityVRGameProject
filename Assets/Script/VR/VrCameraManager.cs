using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using VRStandardAssets.Utils;

public class VrCameraManager : MonoBehaviour
{
    public static VrCameraManager instance { get; private set; }
    public VRInput vrInput { get; private set; }

    [Header("VR Reticle")]
    public Image reticleUi;
    public Image selectionRadiusBar;

    [Header("Cardboard Assets")] // 1.20 version
    public bool useGoogleCardboard = false;
    public GameObject gvrReticle;
    public GameObject GvrViewerMain;
    public GameObject eventSystem;

    void Awake()
    {
        if( instance == null)
            instance = this;

        vrInput = GetComponentInChildren<VRInput>();

        reticleUi.gameObject.SetActive(!useGoogleCardboard);

        if (gvrReticle != null)
            gvrReticle.SetActive(useGoogleCardboard);
        if (GvrViewerMain != null)
            GvrViewerMain.SetActive(useGoogleCardboard);

        if (useGoogleCardboard == true)
        {
            gameObject.AddComponent<GvrPointerPhysicsRaycaster>();
            eventSystem.AddComponent<GvrPointerManager>();
            eventSystem.AddComponent<GvrPointerInputModule>();
           
            GvrPointerManager.Pointer = gvrReticle.GetComponent<GvrBasePointer>();
            Debug.Log(GvrPointerManager.Pointer);
        }
    }

    // ========================== Reticle
    public void SetReticleRadiusValue(float fillValue)
    {
        if (selectionRadiusBar)
        {
            selectionRadiusBar.fillAmount = fillValue;
        }
    }

    public void ReticleOn()
    {
        Color baseColor = reticleUi.color;
        baseColor.a = 1.0f;
        reticleUi.color = baseColor;
    }

    public void ReticleOff()
    {
        Color baseColor = reticleUi.color;
        baseColor.a = 0.0f;
        reticleUi.color = baseColor;

        SetReticleRadiusValue(0.0f);
    }
}
