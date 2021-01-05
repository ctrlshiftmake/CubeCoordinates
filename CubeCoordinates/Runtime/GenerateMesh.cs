using UnityEngine;
using System.Collections.Generic;

namespace CubeCoordinates
{
    public class GenerateMesh : MonoBehaviour
    {
        private static GenerateMesh instance;
        public static GenerateMesh Instance
        {
            get { return instance ?? (instance = new GameObject("GenerateMesh").AddComponent<GenerateMesh>()); }
        }

        public GameObject CreateGameObject(float radius)
        {
            GameObject go = new GameObject("generated");

            Mesh hexBase = GenerateMesh.Instance.GetHexBase(radius);
            PrepareGameObject(go, hexBase, Color.white);

            GameObject go_outline = new GameObject("outline");
            go_outline.transform.parent = go.transform;

            Mesh hexOutline = GenerateMesh.Instance.GetHexOutline(radius);
            PrepareGameObject(go_outline, hexOutline, Color.black);

            return go;
        }

        private void PrepareGameObject(GameObject go, Mesh mesh, Color color)
        {
            MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
                meshRenderer = go.AddComponent<MeshRenderer>();
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;
            meshRenderer.material = new Material(Shader.Find("Standard"));

            MeshFilter meshFilter = go.GetComponent<MeshFilter>();
            if (meshFilter == null)
                meshFilter = go.AddComponent<MeshFilter>();
            meshRenderer.material.color = color;
            meshFilter.mesh = mesh;
        }

        private Mesh GetHexBase(float radius)
        {
            Mesh mesh = new Mesh();
            mesh.name = "hex_base";

            Vector3[] verts = new Vector3[6];

            verts[0] = GetVertex(0, radius);
            verts[1] = GetVertex(1, radius);
            verts[2] = GetVertex(2, radius);
            verts[3] = GetVertex(3, radius);
            verts[4] = GetVertex(4, radius);
            verts[5] = GetVertex(5, radius);

            mesh.vertices = verts;

            int[] triangles = new int[12];

            triangles[0] = 0;
            triangles[1] = 5;
            triangles[2] = 1;
            triangles[3] = 1;
            triangles[4] = 5;
            triangles[5] = 2;
            triangles[6] = 5;
            triangles[7] = 4;
            triangles[8] = 2;
            triangles[9] = 4;
            triangles[10] = 3;
            triangles[11] = 2;

            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }

        private Mesh GetHexOutline(float radius)
        {
            Mesh mesh = new Mesh();
            mesh.name = "hexTileOutline";

            Vector3[] verts = new Vector3[6];

            verts[0] = GetVertex(0, radius);
            verts[1] = GetVertex(1, radius);
            verts[2] = GetVertex(2, radius);
            verts[3] = GetVertex(3, radius);
            verts[4] = GetVertex(4, radius);
            verts[5] = GetVertex(5, radius);

            Vector3[] v = new Vector3[12];

            float upOffset = radius * 0.005f;
            float innerRadius = radius * 0.9f;

            v[0] = verts[0] + (Vector3.up * upOffset);
            v[1] = verts[1] + (Vector3.up * upOffset);
            v[2] = verts[2] + (Vector3.up * upOffset);
            v[3] = verts[3] + (Vector3.up * upOffset);
            v[4] = verts[4] + (Vector3.up * upOffset);
            v[5] = verts[5] + (Vector3.up * upOffset);
            v[6] = (verts[0] * innerRadius) + (Vector3.up * upOffset);
            v[7] = (verts[1] * innerRadius) + (Vector3.up * upOffset);
            v[8] = (verts[2] * innerRadius) + (Vector3.up * upOffset);
            v[9] = (verts[3] * innerRadius) + (Vector3.up * upOffset);
            v[10] = (verts[4] * innerRadius) + (Vector3.up * upOffset);
            v[11] = (verts[5] * innerRadius) + (Vector3.up * upOffset);

            mesh.vertices = v;

            int[] triangles = new int[36];

            triangles[0] = 0;
            triangles[1] = 5;
            triangles[2] = 11;

            triangles[3] = 0;
            triangles[4] = 11;
            triangles[5] = 6;

            triangles[6] = 1;
            triangles[7] = 0;
            triangles[8] = 6;

            triangles[9] = 1;
            triangles[10] = 6;
            triangles[11] = 7;

            triangles[12] = 2;
            triangles[13] = 1;
            triangles[14] = 7;

            triangles[15] = 2;
            triangles[16] = 7;
            triangles[17] = 8;

            triangles[18] = 2;
            triangles[19] = 8;
            triangles[20] = 9;

            triangles[21] = 2;
            triangles[22] = 9;
            triangles[23] = 3;

            triangles[24] = 3;
            triangles[25] = 9;
            triangles[26] = 10;

            triangles[27] = 3;
            triangles[28] = 10;
            triangles[29] = 4;

            triangles[30] = 4;
            triangles[31] = 10;
            triangles[32] = 11;

            triangles[33] = 4;
            triangles[34] = 11;
            triangles[35] = 5;

            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }

        private Vector3 GetVertex(int i, float radius)
        {
            float angle_deg = 60.0f * (float)i;
            float angle_rad = (Mathf.PI / 180.0f) * angle_deg;
            return new Vector3((radius * Mathf.Cos(angle_rad)),
                0.0f,
                (radius * Mathf.Sin(angle_rad))
            );
        }
    }
}