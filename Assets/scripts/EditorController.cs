using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// エディターシーンのコントローラー
/// </summary>
public class EditorController : MonoBehaviour {

    /// <summary>
    /// 点オブジェクトのひな型
    /// </summary>
    public GameObject PointPrefab;

    /// <summary>
    /// 生成される全オブジェクトの親
    /// </summary>
    public Transform Objects;

    // 各レイヤー番号
    private int xyzLayer_;
    private int xywLayer_;
    private int wyzLayer_;

    // ドラッグ中の点
    private PointController draggingPoint_;
    private float draggingPointDistance_;

    /// <summary>
    /// オブジェクト生成時の処理
    /// </summary>
    void Awake()
    {
        xyzLayer_ = LayerMask.NameToLayer("SpaceXYZ");
        xywLayer_ = LayerMask.NameToLayer("SpaceXYW");
        wyzLayer_ = LayerMask.NameToLayer("SpaceWYZ");
    }

    /// <summary>
    /// クリック時の処理
    /// </summary>
    /// <param name="worldPosition">クリックされたワールド座標</param>
    /// <param name="layer">クリックされたレイヤー</param>
    public void OnClick(Vector3 worldPosition, int layer)
    {
        // 新しい点の生成
        GameObject newPoint = Instantiate<GameObject>(PointPrefab);
        newPoint.transform.parent = Objects;
        
        // 4次元位置を設定
        Vector4 pos = GetPosition4D(worldPosition, layer);
        newPoint.GetComponent<PointController>().SetPosition4D(pos);
    }

    /// <summary>
    /// ドラッグ開始時の処理
    /// </summary>
    /// <param name="point">ドラッグを開始した点オブジェクト</param>
    /// <param name="distance">カメラから点までの距離</param>
    internal void OnBeginDrag(PointController point, float distance)
    {
        draggingPoint_ = point;
        draggingPointDistance_ = distance;
    }

    /// <summary>
    /// ドラッグ中の処理
    /// </summary>
    /// <param name="ray">ドラッグ中のRay</param>
    /// <param name="layer">ドラッグされているオブジェクトのレイヤー</param>
    internal void OnDrag(Ray ray, int layer)
    {
        if(draggingPoint_ != null)
        {
            draggingPoint_.SetPosition4D(GetPosition4D(ray.GetPoint(draggingPointDistance_), layer));
        }
    }

    /// <summary>
    /// ドラッグ終了
    /// </summary>
    internal void OnEndDrag()
    {
        draggingPoint_ = null;
        draggingPointDistance_ = float.NaN;
    }

    // 3次元座標とレイヤーから4次元座標を取得する。
    private Vector4 GetPosition4D(Vector3 pos, int layer)
    {
        if (layer == xyzLayer_)
        {
            return new Vector4(pos.x, pos.y, pos.z, 0.0f);
        }
        else if (layer == xywLayer_)
        {
            return new Vector4(pos.x, pos.y, 0.0f, pos.z);
        }
        else if(layer == wyzLayer_)
        {
            return new Vector4(0.0f, pos.y, pos.z, pos.x);
        }
        else
        {
            return new Vector4(pos.x, pos.y, pos.z, 0.0f);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
