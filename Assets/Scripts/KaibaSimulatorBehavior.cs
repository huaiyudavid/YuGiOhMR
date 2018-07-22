using System.Collections;
using UnityEngine;

public class KaibaSimulatorBehavior : MonoBehaviour {

    public GameObject card;

    public const float cardWidth = 1.0F;
    public const float cardHeight = 1.345F;

    public const float fieldPadding = 0.1F;
    public const float interFieldPadding = 0.2F;
    public const float fieldThickness = 0.02F;

    public static Vector3 backRowOffset = (cardHeight + 2.0F * fieldPadding + interFieldPadding) * Vector3.back;

    public static Vector3[] monsterZoneOffsets = new Vector3[5] {
        -2.0F * cardHeight * Vector3.right,
        -cardHeight * Vector3.right,
        Vector3.zero,
        +cardHeight * Vector3.right,
        +2.0F * cardHeight * Vector3.right,
    };

    public static Vector3[] magicZoneOffsets = new Vector3[5] {
        -2.0F * cardHeight * Vector3.right + backRowOffset,
        -cardHeight * Vector3.right + backRowOffset,
        Vector3.zero + backRowOffset,
        +cardHeight * Vector3.right + backRowOffset,
        +2.0F * cardHeight * Vector3.right + backRowOffset,
    };

    public static Vector3 mainDeckZoneOffset = +3.0F * cardHeight * Vector3.right + backRowOffset;
    public static Vector3 graveyardZoneOffset = +3.0F * cardHeight * Vector3.right;
    public static Vector3 fieldSpellZoneOffset = -3.0F * cardHeight * Vector3.right;
    public static Vector3 extraDeckZoneOffset = -3.0F * cardHeight * Vector3.right + backRowOffset;

    public static GameObject[] CreatePlayFieldFrame(Transform parent, Vector3 offset) {
        float x = 0.5F * cardWidth + fieldPadding;
        float z = 0.5F * cardHeight + fieldPadding;
        return GeometryUtilities.CreateCylinderFrame(parent, new Vector3[4] {
            new Vector3(-x, 0.0F, -z) + offset,
            new Vector3(+x, 0.0F, -z) + offset,
            new Vector3(+x, 0.0F, +z) + offset,
            new Vector3(-x, 0.0F, +z) + offset,
        }, fieldThickness);
    }

    private GameObject[] deck;
    private int k;

    static Quaternion faceDownRotation =
        Quaternion.Euler(0.0F, 180.0F, 0.0F) *
        Quaternion.FromToRotation(Vector3.back, Vector3.down);

    static Quaternion faceUpRotation =
        Quaternion.Euler(0.0F, 0.0F, 180.0F) * faceDownRotation;

    private static float LerpEasingFunction(float t) {
        return 0.5F * (1.0F - Mathf.Cos(Mathf.PI * t));
    }

    private static IEnumerator FlipCard(GameObject card, Vector3 targetPosition, float duration) {
        Vector3 originalPosition = card.transform.position;
        Quaternion originalRotation = card.transform.rotation;
        Quaternion targetRotation = faceUpRotation;
        float elapsedTime = 0.0F;
        while (elapsedTime <= duration) {
            elapsedTime += Time.deltaTime;
            card.transform.position = Vector3.Lerp(originalPosition, targetPosition, LerpEasingFunction(elapsedTime / duration));
            card.transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, LerpEasingFunction(elapsedTime / duration));
            yield return null;
        }
        card.transform.position = targetPosition;
        card.transform.rotation = targetRotation;
    }

    // Use this for initialization
    void Start() {
        GameObject parent = new GameObject();
        foreach (Vector3 offset in monsterZoneOffsets) {
            CreatePlayFieldFrame(parent.transform, offset);
        }
        foreach (Vector3 offset in magicZoneOffsets) {
            CreatePlayFieldFrame(parent.transform, offset);
        }
        CreatePlayFieldFrame(parent.transform, mainDeckZoneOffset);
        CreatePlayFieldFrame(parent.transform, graveyardZoneOffset);
        CreatePlayFieldFrame(parent.transform, fieldSpellZoneOffset);
        CreatePlayFieldFrame(parent.transform, extraDeckZoneOffset);

        deck = new GameObject[40];
        k = 40;

        for (int i = 0; i < 40; ++i) {
            deck[i] = Instantiate(card, mainDeckZoneOffset + Vector3.up * (0.3F / 40) * i, faceDownRotation);
        }
    }

    private static Vector3[] allZoneOffsets = new Vector3[40] {
        monsterZoneOffsets[0],
        monsterZoneOffsets[1],
        monsterZoneOffsets[2],
        monsterZoneOffsets[3],
        monsterZoneOffsets[4],
        magicZoneOffsets[0],
        magicZoneOffsets[1],
        magicZoneOffsets[2],
        magicZoneOffsets[3],
        magicZoneOffsets[4],
        fieldSpellZoneOffset,
        graveyardZoneOffset,
        extraDeckZoneOffset,
        monsterZoneOffsets[0] - backRowOffset,
        monsterZoneOffsets[1] - backRowOffset,
        monsterZoneOffsets[2] - backRowOffset,
        monsterZoneOffsets[3] - backRowOffset,
        monsterZoneOffsets[4] - backRowOffset,
        monsterZoneOffsets[0] - 2.0F * backRowOffset,
        monsterZoneOffsets[1] - 2.0F * backRowOffset,
        monsterZoneOffsets[2] - 2.0F * backRowOffset,
        monsterZoneOffsets[3] - 2.0F * backRowOffset,
        monsterZoneOffsets[4] - 2.0F * backRowOffset,
        monsterZoneOffsets[0] - 3.0F * backRowOffset,
        monsterZoneOffsets[1] - 3.0F * backRowOffset,
        monsterZoneOffsets[2] - 3.0F * backRowOffset,
        monsterZoneOffsets[3] - 3.0F * backRowOffset,
        monsterZoneOffsets[4] - 3.0F * backRowOffset,
        monsterZoneOffsets[0] - 4.0F * backRowOffset,
        monsterZoneOffsets[1] - 4.0F * backRowOffset,
        monsterZoneOffsets[2] - 4.0F * backRowOffset,
        monsterZoneOffsets[3] - 4.0F * backRowOffset,
        monsterZoneOffsets[4] - 4.0F * backRowOffset,
        monsterZoneOffsets[0] - 5.0F * backRowOffset,
        monsterZoneOffsets[1] - 5.0F * backRowOffset,
        monsterZoneOffsets[2] - 5.0F * backRowOffset,
        monsterZoneOffsets[3] - 5.0F * backRowOffset,
        monsterZoneOffsets[4] - 5.0F * backRowOffset,
        monsterZoneOffsets[1] - 6.0F * backRowOffset,
        monsterZoneOffsets[3] - 6.0F * backRowOffset,
    };

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            --k;
            StartCoroutine(FlipCard(deck[k], allZoneOffsets[39 - k], 0.5F));
        }
    }

}
