using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// エディターシーンのコントローラー
/// </summary>
public class EditorController : MonoBehaviour
{
    // クリック箇所のカメラからの距離
    private const float CLICK_DISTANCE = 1.0f;

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
    
    // 選択中の点
    private PointController selectedPoint_;

    // 移動中の点
    private PointController movingPoint_;

    // 移動中の点の距離
    private float movingDistance_;

    /// <summary>
    /// 選択中の点を返す
    /// </summary>
    public PointController SelectedPoint
    {
        get { return selectedPoint_; }
        private set
        {
            PointController oldPoint = selectedPoint_;
            selectedPoint_ = value;

            // イベント発生
            if(oldPoint != value)
            {
                if (oldPoint != null)
                {
                    oldPoint.OnDeselected();
                }
                if (value != null)
                {
                    value.OnSelected();
                }
            }
        }
    }

    /// <summary>
    /// 移動中の点を返す
    /// </summary>
    public PointController MovingPoint
    {
        get { return movingPoint_; }
    }

    // 移動する点を設定する
    private void SetMovingPoint(PointController point, float distance)
    {
        PointController oldPoint = movingPoint_;
        movingPoint_ = point;
        movingDistance_ = distance;

        // イベント発生
        if (oldPoint != point)
        {
            if (oldPoint != null)
            {
                oldPoint.OnEndMove();
            }
            if (point != null)
            {
                point.OnBeginMove();
            }
        }
    }

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
    /// <param name="ray">クリック時のRay</param>
    /// <param name="layer">クリックされたレイヤー</param>
    public void OnClick(Ray ray, int layer)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << layer))
        {
            // 点をクリックした場合、選択を切り替える
            SelectedPoint = hit.transform.GetComponentInParent<PointController>();
        }
        else if (SelectedPoint != null)
        {
            // 選択中だが非ヒット時、選択を解除
            SelectedPoint = null;
        }
        else
        {
            // 未選択時、クリック位置に点を生成
            Vector4 pos = GetPosition4D(ray.GetPoint(CLICK_DISTANCE), layer);
            GeneratePoint(pos);
        }
    }

    // 新しい点を生成する
    private void GeneratePoint(Vector4 pos)
    {
        GameObject newPoint = Instantiate<GameObject>(PointPrefab);
        newPoint.transform.SetParent(Objects);
        newPoint.GetComponent<PointController>().SetPosition4D(pos);
    }

    /// <summary>
    /// ドラッグ開始時の処理
    /// </summary>
    /// <param name="hit">ドラッグを開始した点オブジェクトへのRaycastHit</param>
    internal void OnBeginDrag(RaycastHit hit)
    {
        // 選択中の点であれば移動開始
        PointController point = hit.transform.GetComponentInParent<PointController>();
        if (point.IsSelected)
        {
            SetMovingPoint(point, hit.distance);
        }
    }

    /// <summary>
    /// ドラッグ中の処理
    /// </summary>
    /// <param name="ray">ドラッグ中のRay</param>
    /// <param name="layer">ドラッグされているオブジェクトのレイヤー</param>
    internal void OnDrag(Ray ray, int layer)
    {
        if(movingPoint_ != null)
        {
            movingPoint_.SetPosition4D(GetPosition4D(ray.GetPoint(movingDistance_), layer));
        }
    }

    /// <summary>
    /// ドラッグ終了
    /// </summary>
    internal void OnEndDrag()
    {
        SetMovingPoint(null, float.NaN);
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
