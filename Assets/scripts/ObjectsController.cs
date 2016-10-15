using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エディターの動作に合わせてオブジェクトを管理するクラス
/// </summary>
public class ObjectsController : AbstractEditorHandler
{
    /// <summary>
    /// 点オブジェクトのひな型
    /// </summary>
    public GameObject PointPrefab;

    // ポイント集合
    private IDictionary<int, PointController> Points_ = new Dictionary<int, PointController>();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void OnAddPoint(HyperPoint point)
    {
        // ポイント生成
        GameObject newPoint = Instantiate<GameObject>(PointPrefab);
        PointController pointController = newPoint.GetComponent<PointController>();
        Points_.Add(point.Id, pointController);
        pointController.transform.SetParent(transform);
        pointController.SetPosition4D(point.Position);
    }

    public override void OnRemovePoint(HyperPoint point)
    {
        // ポイント破棄
        PointController pointController;
        if(Points_.TryGetValue(point.Id, out pointController))
        {
            Destroy(pointController);
            Points_.Remove(point.Id);
        }
    }
}
