using UnityEngine;

public class RotateBehavior : MonoBehaviour {

    public float TranslationSpeed = 0.0F;
    public Vector3 TranslationDirection = Vector3.zero;
    public float RotationSpeed = 0.0F;
    public Vector3 RotationDirection = Vector3.zero;

    void Start() { }

    void Update() {
        transform.Rotate(RotationDirection, RotationSpeed * Time.deltaTime);
        transform.Translate(TranslationSpeed * Time.deltaTime * TranslationDirection);
    }

}
