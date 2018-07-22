using UnityEngine;

public class CardSpawnerBehavior : MonoBehaviour {

    public GameObject blueEyesWhiteDragon;
    private float elapsedTime;

    const float cardWidth = 1.0F;
    const float cardHeight = 1.345F;

    public float fieldPadding = 0.1F;
    public float interFieldPadding = 0.2F;
    public float fieldThickness = 0.02F;

    public GameObject[] CreatePlayFieldFrame(Transform parent, Vector3 offset) {
        float x = 0.5F * cardWidth + fieldPadding;
        float z = 0.5F * cardHeight + fieldPadding;
        return GeometryUtilities.CreateCylinderFrame(parent, new Vector3[4] {
            new Vector3(-x, 0.0F, -z) + offset,
            new Vector3(+x, 0.0F, -z) + offset,
            new Vector3(+x, 0.0F, +z) + offset,
            new Vector3(-x, 0.0F, +z) + offset,
        }, fieldThickness);
    }

    void Start() {
        GameObject parent = new GameObject();
        CreatePlayFieldFrame(parent.transform, Vector3.zero);
        CreatePlayFieldFrame(parent.transform, new Vector3(+cardHeight, 0.0F));
        CreatePlayFieldFrame(parent.transform, new Vector3(-cardHeight, 0.0F));
        CreatePlayFieldFrame(parent.transform, new Vector3(+2.0F * cardHeight, 0.0F));
        CreatePlayFieldFrame(parent.transform, new Vector3(-2.0F * cardHeight, 0.0F));
        RotateBehavior rotate = parent.AddComponent<RotateBehavior>();
        rotate.RotationSpeed = 30.0F;
        rotate.RotationDirection = Random.onUnitSphere;

    }

    void Update() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.1F) {
            elapsedTime = 0.0F;
            GameObject bewd = Instantiate(blueEyesWhiteDragon, Vector3.zero, Quaternion.identity);
            bewd.transform.localScale = new Vector3(2.0F, 2.0F, 2.0F);
            RotateBehavior rotate = bewd.AddComponent<RotateBehavior>();
            rotate.RotationSpeed = Random.Range(180.0F, 720.0F);
            rotate.RotationDirection = Random.onUnitSphere;
            rotate.TranslationSpeed = Random.Range(10.0F, 20.0F);
            rotate.TranslationDirection = Random.onUnitSphere;
        }
    }
}
