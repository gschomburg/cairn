using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;
using UnityStandardAssets.ImageEffects;


public class MagicFieldDetect : MonoBehaviour {
    public int MAGIC_FIELD_LAYER = 11;
    // Use this for initialization
    public Color defaultColor;
    public Color disabledColor;

    public bool blurVision;
    public float targetDistort = 0;
    public float currentDistort = 0;
    public float magicRadius = 10;
    public float maxDistort = 1;
    public float distortSpeed = .2f;
    Camera eyes;

    void Start()
    {
        defaultColor = HandParts[0].GetComponent<Renderer>().material.GetColor("_Color");
        eyes = GetComponentInParent<Camera>();
    }

    //// Update is called once per frame
    void Update()
    {
        if(eyes != null)
        {
            updateVision();
        }
    }
    void updateVision()
    {
        if (blurVision)
        {
            float dis = Vector3.Distance(gameObject.transform.position, new Vector3(0, gameObject.transform.position.y, 0));
            float disOutside = Mathf.Clamp((dis - magicRadius)/maxDistort, 0f, 1f);
            targetDistort = disOutside;
        }
        else
        {
            targetDistort = 0;
        }
        //currentDistort += (currentDistort - targetDistort) * .2f; //lerp
        currentDistort = Mathf.Lerp(currentDistort, targetDistort, Time.deltaTime * distortSpeed);
        //add image effects
        ColorCorrectionCurves colors = GetComponentInParent<ColorCorrectionCurves>();
        colors.saturation = Mathf.Clamp(1 - currentDistort, 0, 1);

        Fisheye fisheye = GetComponentInParent<Fisheye>();
        fisheye.strengthX = Mathf.Clamp(currentDistort* currentDistort, 0, 1);
        fisheye.strengthY = Mathf.Clamp(currentDistort* currentDistort, 0, 1);

        VignetteAndChromaticAberration vignette = GetComponentInParent<VignetteAndChromaticAberration>();
        vignette.intensity = Mathf.Clamp(currentDistort , 0, .8f);
    }

    public List<GameObject> HandParts; 
    void FieldExit()
    {
        //get the controlelr
        VRTK_InteractGrab grabber = GetComponentInParent<VRTK_InteractGrab>();
        if (grabber)
        {
            grabber.ForceRelease();
        }
        
        if (eyes)
        {
            blurVision = true;
        }
        setColors(disabledColor);
    }
    void FieldEnter()
    {
        setColors(defaultColor);

        if (eyes)
        {
            blurVision = false;
        }
    }

    

    void setColors(Color col)
    {
        foreach(GameObject part in HandParts)
        {
            part.GetComponent<Renderer>().material.SetColor("_Color", col);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.gameObject);
        if (other.gameObject.layer == MAGIC_FIELD_LAYER)
        {
            //lockPoint = other.gameObject;
            Debug.Log("OnTriggerEnter MAGIC_FIELD_LAYER");
            FieldEnter();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == MAGIC_FIELD_LAYER)
        {
            //lockPoint = other.gameObject;
            Debug.Log("OnTriggerExit MAGIC_FIELD_LAYER");
            FieldExit();
        }
    }
}
