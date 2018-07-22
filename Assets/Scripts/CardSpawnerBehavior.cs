using UnityEngine;

public class CardSpawnerBehavior : MonoBehaviour {

    public GameObject card;
    private float elapsedTime;

    void Start() { }

    void Update() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.1F) {
            elapsedTime = 0.0F;
            GameObject cardInstance = Instantiate(card, Vector3.zero, Quaternion.FromToRotation(Vector3.up, Random.onUnitSphere));
            cardInstance.transform.localScale = new Vector3(2.0F, 2.0F, 2.0F);
            RotateBehavior rotate = cardInstance.AddComponent<RotateBehavior>();
            rotate.RotationSpeed = Random.Range(180.0F, 720.0F);
            rotate.RotationDirection = Random.onUnitSphere;
            rotate.TranslationSpeed = Random.Range(10.0F, 20.0F);
            rotate.TranslationDirection = Random.onUnitSphere;
        }
    }

}
