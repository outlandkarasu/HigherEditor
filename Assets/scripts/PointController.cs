using UnityEngine;
using System.Collections;

/**
 *  4次元ポイントコントローラー
 */
public class PointController : MonoBehaviour
{
    /// <summary>
    /// W座標の値
    /// </summary>
    [TooltipAttribute("W座標")]
    public float W;

    // XYW空間オブジェクト
    private Transform xyw_;

    // WYZ空間オブジェクト
    private Transform wyz_;

    /// <summary>
    /// 4次元座標を設定する
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition4D(Vector4 position)
    {
        transform.position = position;
        xyw_.position = new Vector3(position.x, position.y, position.w);
        wyz_.position = new Vector3(position.w, position.y, position.z);
        W = position.w;
    }


    // 初期化処理
    void Awake()
    {
        xyw_ = transform.Find("XYW");
        wyz_ = transform.Find("WYZ");
    }

    // 毎フレームの処理
    void Update ()
    {
        // W座標のある空間のオブジェクトの位置を調整する。
        Vector3 pos = transform.position;
        xyw_.position = new Vector3(pos.x, pos.y, W);
        wyz_.position = new Vector3(W, pos.y, pos.z);
    }
}
