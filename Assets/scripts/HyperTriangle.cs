using UnityEngine;
using System.Collections;

/// <summary>
/// 4次元三角形オブジェクト
/// </summary>
public struct HyperTriangle
{
    /// <summary>
    /// 点のID
    /// </summary>
    public int Point1;

    /// <summary>
    /// 点のID
    /// </summary>
    public int Point2;

    /// <summary>
    /// 点のID
    /// </summary>
    public int Point3;

    /// <summary>
    /// ハッシュ計算を行う。
    /// </summary>
    /// <returns>ハッシュコード</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            return Point1.GetHashCode() + Point2.GetHashCode() + Point3.GetHashCode();
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

        if(obj is HyperTriangle)
        {
            HyperTriangle other = (HyperTriangle)obj;
            return Point1 == other.Point1 && Point2 == other.Point2 && Point3 == other.Point3;
        }

        return false;
    }
}
