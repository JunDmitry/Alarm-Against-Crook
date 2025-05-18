using UnityEngine;

public class Movement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const float Epsilon = 1e-3f;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private float _direction;
    private float _angle;

    private void FixedUpdate()
    {
        if (Mathf.Approximately(_direction, Epsilon) && Mathf.Approximately(_angle, Epsilon))
            return;

        transform.Rotate(_angle * _rotationSpeed * Time.deltaTime * Vector3.up);
        transform.Translate(_direction * _moveSpeed * Time.deltaTime * Vector3.forward);
    }

    private void Update()
    {
        _direction = Input.GetAxis(Vertical);
        _angle = Input.GetAxis(Horizontal);
    }
}