using UnityEngine;

public class ProceduralPlayFieldBehavior : MonoBehaviour {

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

    // Use this for initialization
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

    // Update is called once per frame
    void Update() { }

}
