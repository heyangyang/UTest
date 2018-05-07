
public enum EnExportType : byte
{
    None,
    /// <summary>
    /// 普通素材，被根素材依赖的
    /// </summary>
    Asset,
    /// <summary>
    /// 根
    /// </summary>
    Root,
    /// <summary>
    /// 需要单独打包，说明这个素材是被两个或以上的素材依赖的
    /// </summary>
    Standalone,
    /// <summary>
    /// 既是根又是被别人依赖的素材
    /// </summary>
    RootAsset,
    /// <summary>
    /// 合并资源
    /// </summary>
    RootMerge,
}

