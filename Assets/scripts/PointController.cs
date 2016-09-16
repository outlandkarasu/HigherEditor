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
    
    /// <summary>
    /// 通常時のマテリアル
    /// </summary>
    [TooltipAttribute("通常時のマテリアル")]
    public Material NormalMaterial;
    
    /// <summary>
    /// 選択時のマテリアル
    /// </summary>
    [TooltipAttribute("選択時のマテリアル")]
    public Material SelectedMaterial;

    // 各空間オブジェクト
    private Transform xyz_;
    private Transform xyw_;
    private Transform wyz_;

    // エディターコントローラー
    private EditorController editorController_;

    /// <summary>
    /// 選択されているかどうか返す
    /// </summary>
    public bool IsSelected
    {
        get
        {
            return editorController_.SelectedPoint == this;
        }
    }

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
        xyz_ = transform.Find("XYZ");
        xyw_ = transform.Find("XYW");
        wyz_ = transform.Find("WYZ");
        editorController_ = GetComponentInParent<EditorController>();
    }

    // 毎フレームの処理
    void Update ()
    {
        // W座標のある空間のオブジェクトの位置を調整する。
        Vector3 pos = transform.position;
        xyw_.position = new Vector3(pos.x, pos.y, W);
        wyz_.position = new Vector3(W, pos.y, pos.z);
    }

    /// <summary>
    /// 選択時の処理
    /// </summary>
    public void OnSelected()
    {
        SetMaterial(SelectedMaterial);
    }

    /// <summary>
    /// 非選択時の処理
    /// </summary>
    public void OnDeselected()
    {
        SetMaterial(NormalMaterial);
    }

    // 色を変更する
    private void SetMaterial(Material material)
    {
        xyz_.GetComponent<Renderer>().material = material;
        xyw_.GetComponent<Renderer>().material = material;
        wyz_.GetComponent<Renderer>().material = material;
    }
}
