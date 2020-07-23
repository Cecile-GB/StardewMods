namespace ContentPatcher.Framework.Patches
{
    /// <summary>An invalid patch that couldn't be loaded.</summary>
    internal class DisabledPatch
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The path to the patch from the root content file.</summary>
        public LogPathBuilder Path { get; }

        /// <summary>The raw patch type.</summary>
        public string Type { get; }

        /// <summary>The raw asset name to intercept.</summary>
        public string AssetName { get; }

        /// <summary>The content pack which requested the patch.</summary>
        public ManagedContentPack ContentPack { get; }

        /// <summary>The reason this patch is disabled.</summary>
        public string ReasonDisabled { get; }

        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="path">The path to the patch from the root content file.</param>
        /// <param name="type">The raw patch type.</param>
        /// <param name="assetName">The raw asset name to intercept.</param>
        /// <param name="contentPack">The content pack which requested the patch.</param>
        /// <param name="reasonDisabled">The reason this patch is disabled.</param>
        public DisabledPatch(LogPathBuilder path, string type, string assetName, ManagedContentPack contentPack, string reasonDisabled)
        {
            this.Path = path;
            this.Type = type;
            this.ContentPack = contentPack;
            this.AssetName = assetName;
            this.ReasonDisabled = reasonDisabled;
        }
    }
}
