using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasRenderer))]
public class Radar : MonoBehaviour
{
    [SerializeField] private Material mat;//材质球，自行创建，shader选择 UI/Default
    [SerializeField] private Texture _tex;//贴图，可选
    private CanvasRenderer _render;
    public List<int> dataList = new List<int>(10);//每个点的数据
    [SerializeField] private float radarSize = 10f;//雷达图的长度


    private void Awake()
    {
        _render = GetComponent<CanvasRenderer>();
        Debug.Log(_render);
    }
    // Start is called before the first frame update
    void Start()
    {

       // UpdateRadarVisualData();
    }


    /// <summary>
    /// 生成雷达图
    /// </summary>
    public void UpdateRadarVisualData()
    {
        int _length = dataList.Count;
        if (_length < 3)
        {
            Debug.LogError("边数不能小于3");
            return;
        }
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[_length + 1];
        Vector2[] uv = new Vector2[_length + 1];
        int[] triangles = new int[3 * _length];//一个三角形，三个点，5个图形，乘以5
        float angleIncrement = 360f / _length;//度数

        vertices[0] = Vector3.zero;
        for (int i = 1; i <= _length; i++)
        {
            vertices[i] = Quaternion.Euler(0, 0, -angleIncrement * (i - 1)) * Vector3.up * radarSize * dataList[i - 1];
        }

        uv[0] = Vector2.zero;
        for (int i = 1; i <= _length; i++)
        {
            uv[i] = Vector2.one;
        }

   

        //注释规律总结
        for (int i = 0; i < _length * 3; i++)
        {
            if (i % 3 == 0)
            {
                triangles[i] = 0;
            }
            else
            {
                if ((i - 1) % 3 == 0)
                {
                    triangles[i] = (i - 1) / 3 + 1;
                }
                else
                {
                    triangles[i] = (i - 2) / 3 + 2;
                }
            }
            //最终值为1
            if (i == _length * 3 - 1)
            {
                triangles[i] = 1;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        _render.SetMesh(mesh);
        _render.SetMaterial(mat, _tex);
    }

    public void Clear()
    {
        _render.Clear();
    }
}
