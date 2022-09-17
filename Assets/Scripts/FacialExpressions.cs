using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressions : MonoBehaviour
{
    [SerializeField] private GameObject _eyebrowRight;
    [SerializeField] private GameObject _eyebrowLeft;
    [SerializeField] private float _expressiveRotation;

    private Quaternion _neutralRotationRight;
    private Quaternion _neutralRotationLeft;

    private bool _isNeutral;
    private bool _isConcerned;
    private bool _isAngry;

    private void Start()
    {
        _neutralRotationRight = _eyebrowRight.transform.rotation;
        _neutralRotationLeft = _eyebrowLeft.transform.rotation;
    }
    public void SetNeutralExpression()
    {
        if (!_isNeutral)
        {
            _eyebrowRight.transform.rotation = _neutralRotationRight;
            _eyebrowLeft.transform.rotation = _neutralRotationLeft;

            _isNeutral = true;
            _isConcerned = false;
            _isAngry = false;
        }

    }

    public void SetAngryExpression()
    {
        if (!_isAngry)
        {
            _eyebrowRight.transform.Rotate(0, 0, _expressiveRotation);
            _eyebrowLeft.transform.Rotate(0, 0, -_expressiveRotation);

            _isNeutral = false;
            _isConcerned = false;
            _isAngry = true;
        }

    }

    public void SetConcernedExpression()
    {
        if (!_isConcerned)
        {
            _eyebrowRight.transform.Rotate(0, 0, -_expressiveRotation);
            _eyebrowLeft.transform.Rotate(0, 0, _expressiveRotation);

            _isNeutral = false;
            _isConcerned = true;
            _isAngry = false;
        }
    }
}
