using UnityEngine;

public static class GeometryUtilities {

    public static GameObject CreateSphere(
        Transform parent,
        Vector3 positionOffset,
        float radius
    ) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.SetParent(parent);
        sphere.transform.Translate(positionOffset);
        sphere.transform.localScale = new Vector3(radius, radius, radius);
        return sphere;
    }

    public static GameObject CreateCylinder(Transform parent, Vector3 startOffset, Vector3 endOffset, float thickness) {
        Vector3 mid = 0.5F * (startOffset + endOffset);
        Vector3 delta = endOffset - startOffset;
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.SetParent(parent);
        cylinder.transform.SetPositionAndRotation(mid, Quaternion.FromToRotation(Vector3.up, delta));
        cylinder.transform.localScale = new Vector3(thickness, 0.5F * delta.magnitude, thickness);
        return cylinder;
    }

    // Precondition: normalDirection is perpendicular to (endOffset - startOffset).
    public static GameObject CreateQuadLine(Transform parent, Vector3 startOffset, Vector3 endOffset, Vector3 normalDirection, float thickness) {
        Vector3 mid = 0.5F * (startOffset + endOffset);
        Vector3 delta = endOffset - startOffset;
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.SetParent(parent);
        Quaternion q = Quaternion.FromToRotation(Vector3.up, delta);
        // TODO: How to fix normal?
        // Quaternion r = Quaternion.FromToRotation(q * Vector3.forward, normalDirection);
        quad.transform.SetPositionAndRotation(mid, q);
        quad.transform.localScale = new Vector3(thickness, delta.magnitude, thickness);
        return quad;
    }

    public static GameObject[] CreateCylinderFrame(Transform parent, Vector3[] vertexOffsets, float thickness) {
        if (vertexOffsets.Length <= 1) { return new GameObject[0]; }
        GameObject[] frameElements = new GameObject[2 * vertexOffsets.Length];
        int n = vertexOffsets.Length - 1;
        for (int i = 0; i < n; ++i) {
            frameElements[2 * i] = CreateSphere(parent, vertexOffsets[i], thickness);
            frameElements[2 * i + 1] = CreateCylinder(parent, vertexOffsets[i], vertexOffsets[i + 1], thickness);
        }
        frameElements[2 * n] = CreateSphere(parent, vertexOffsets[n], thickness);
        frameElements[2 * n + 1] = CreateCylinder(parent, vertexOffsets[n], vertexOffsets[0], thickness);
        return frameElements;
    }

    // Precondtion: all vertexOffsets must lie in the plane perpendicular to normalDirection.
    public static GameObject[] CreateQuadFrame(Transform parent, Vector3[] vertexOffsets, Vector3 normalDirection, float thickness) {
        if (vertexOffsets.Length <= 1) { return new GameObject[0]; }
        GameObject[] frameElements = new GameObject[2 * vertexOffsets.Length];
        int n = vertexOffsets.Length - 1;
        for (int i = 0; i < n; ++i) {
            frameElements[2 * i] = CreateSphere(parent, vertexOffsets[i], thickness);
            frameElements[2 * i + 1] = CreateQuadLine(parent, vertexOffsets[i], vertexOffsets[i + 1], normalDirection, thickness);
        }
        frameElements[2 * n] = CreateSphere(parent, vertexOffsets[n], thickness);
        frameElements[2 * n + 1] = CreateQuadLine(parent, vertexOffsets[n], vertexOffsets[0], normalDirection, thickness);
        return frameElements;
    }

}
