using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

public class VirtualKeyboardS : MonoBehaviour
{
    public static VirtualKeyboardS Instance;

    [SerializeField] float speedAnim = 0.5f;
    [Space]
    [SerializeField] Transform frameCont;
    [SerializeField] Sprite[] frameSp;
    Transform frame;

    RectTransform rectTransform;
    Image frameImg;
    TMP_InputField tempIF;

    private void Start()
    {
        Instance = this;

        rectTransform = frameCont.GetComponent<RectTransform>();
        frame = frameCont.GetChild(0);
        frameImg = frame.GetComponent<Image>();

        ShowChild(false);
    }

    private void ShowChild(bool v)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(v);
        }
    }

    public void ActiveKV(bool v)
    {
        if (v)
        {
            ShowChild(v);

            rectTransform.anchoredPosition = new Vector2(0f, -850f);
            rectTransform.DOAnchorPosY(460.1943f, speedAnim);
        }
        else
        {
            rectTransform.DOAnchorPosY(-850f, speedAnim).OnComplete(FinishAnimHideFrame);
        }
    }

    private void FinishAnimHideFrame()
    {
        ShowChild(false);
    }

    public void SetIF(TMP_InputField tmpif)
    {
        tempIF = tmpif;
        if (tmpif.contentType == TMP_InputField.ContentType.IntegerNumber)
        {
            frameImg.sprite = frameSp[0];

            setButtonContActive(0);
        }
        else
        {
            frameImg.sprite = frameSp[1];

            setButtonContActive(1);
        }
    }

    private void setButtonContActive(int v)
    {
        for (int i = 0; i < frame.childCount; i++)
        {
            if (v == i)
            {
                frame.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                frame.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void AddText(string n)
    {
        if (tempIF.text.Length > tempIF.characterLimit)
        {
            return;
        }
        tempIF.text += n;
        //Debug.Log("cp: " + tempIF.caretPosition);
        tempIF.caretPosition = tempIF.text.Length;
        tempIF.ActivateInputField();
    }
    public void MinText()
    {
        if (tempIF.text.Length <= 0)
        {
            return;
        }
        tempIF.text = tempIF.text.Substring(0, tempIF.text.Length - 1);
        tempIF.caretPosition = tempIF.text.Length;
        tempIF.ActivateInputField();
    }
}
