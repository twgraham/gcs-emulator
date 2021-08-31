namespace GCSEmulator.Dtos
{
    /// <summary>
    /// Set of properties to return. Defaults to noAcl.
    /// Acceptable values are:
    /// <list type="bullet">
    ///     <item>
    ///         <term>full: </term>
    ///         <description>Include all properties.</description>
    ///     </item>
    ///     <item>
    ///         <term>noAcl: </term>
    ///         <description>Omit owner, acl, and defaultObjectAcl properties.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public enum Projection
    {
        Full,
        NoAcl
    }
}