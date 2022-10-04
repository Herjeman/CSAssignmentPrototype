using UnityEngine;

public class RemoveSelf : MonoBehaviour
{
    private float _timer;
    [SerializeField] private float _removeTime;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > _removeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
