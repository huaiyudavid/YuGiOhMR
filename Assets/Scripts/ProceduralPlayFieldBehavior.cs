using UnityEngine;

public class ProceduralPlayFieldBehavior : MonoBehaviour {

    public const float cardWidth = 1.0F;
    public const float cardHeight = 1.345F;

    public const float fieldPadding = 0.1F;
    public const float interFieldPadding = 0.2F;
    public const float fieldThickness = 0.02F;

    public static Vector3[] monsterFieldOffsets = new Vector3[5] {
        -2.0F * cardHeight * Vector3.right,
        -cardHeight * Vector3.right,
        Vector3.zero,
        +cardHeight * Vector3.right,
        +2.0F * cardHeight * Vector3.right,
    };

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
        foreach (Vector3 offset in monsterFieldOffsets) {
            CreatePlayFieldFrame(parent.transform, offset);
        }
        RotateBehavior rotate = parent.AddComponent<RotateBehavior>();
        rotate.RotationSpeed = 30.0F;
        rotate.RotationDirection = Random.onUnitSphere;
    }

    // Update is called once per frame
    void Update() { }

}
