using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSwapper : MonoBehaviour
{
    [SerializeField] private GameObject _neutralFace;
    [SerializeField] private GameObject _angryFace;
    [SerializeField] private GameObject _concernedFace;
    [SerializeField] private GameObject _deadFace;

    public void SetNeutralFace()
    {
        _neutralFace.SetActive(true);
        _angryFace.SetActive(false);
        _concernedFace.SetActive(false);
        _deadFace.SetActive(false);
    }

    public void SetAngryFace()
    {
        _neutralFace.SetActive(false);
        _angryFace.SetActive(true);
        _concernedFace.SetActive(false);
        _deadFace.SetActive(false);
    }

    public void SetConcernedFace()
    {
        _neutralFace.SetActive(false);
        _angryFace.SetActive(false);
        _concernedFace.SetActive(true);
        _deadFace.SetActive(false);
    }

    public void SetDeadFace()
    {
        _neutralFace.SetActive(false);
        _angryFace.SetActive(false);
        _concernedFace.SetActive(false);
        _deadFace.SetActive(true);
    }
}
