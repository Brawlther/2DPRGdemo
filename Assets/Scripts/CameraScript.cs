using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    private Transform _targetTrans;
    private Vector3 _offset;
    private float _smoothSpeed;
    private float _lockedY;
    private bool _lockY;
    private float _fixedXPosition;

    void Awake()
    {
        _targetTrans = target.GetComponent<Transform>();
        _offset = new Vector3(0, 2, -10f);
        _smoothSpeed = 0.125f;
        _fixedXPosition = transform.position.x;
        _lockedY = _targetTrans.position.y;
        _lockY = false;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition;

        if (_lockY)
        {
            desiredPosition = new Vector3(
                _fixedXPosition,           // X fixed
                _lockedY + _offset.y,      // Y absolutely fixed
                _offset.z                  // Z fixed
            );
            transform.position = desiredPosition;
        }
        else
        {
            // Only smooth follow when unlocked
            desiredPosition = new Vector3(
                _fixedXPosition, 
                _targetTrans.position.y + _offset.y, 
                _offset.z
            );

            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothed;
        }
    }


    public void LockY()
    {
        _lockedY = transform.position.y - _offset.y;
        _lockY = true;
    }

    public void UnlockY()
    {
        _lockY = false;
    }
}
