using UnityEngine;
using System.Collections;

/// <summary>
/// 4次元点オブジェクト
/// </summary>
public struct HyperPoint
{
    /// <summary>
    /// 点のID
    /// </summary>
    public int Id;

    /// <summary>
    /// 点の位置
    /// </summary>
    public Vector4 Position;

    /// <summary>
    /// ID・位置を指定して生成する。
    /// </summary>
    /// <param name="id">ポイントID</param>
    /// <param name="pos">位置</param>
    public HyperPoint(int id, Vector4 pos) : this()
    {
        this.Id = id;
        this.Position = pos;
    }

    /// <summary>
    /// ハッシュ計算を行う。
    /// </summary>
    /// <returns>ハッシュコード</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            return Id.GetHashCode() + Position.GetHashCode();
        }
    }

    /// <summary>
    /// 比較メソッド
    /// </summary>
    /// <param name="obj">比較対象</param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }

        if (obj is HyperPoint)
        {
            HyperPoint other = (HyperPoint)obj;
            return Id == other.Id && Position == other.Position;
        }
        return false;
    }
}
