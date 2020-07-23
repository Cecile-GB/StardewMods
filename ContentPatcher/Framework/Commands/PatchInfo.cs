using System.Collections.Generic;
using System.Linq;
using ContentPatcher.Framework.Conditions;
using ContentPatcher.Framework.Patches;

namespace ContentPatcher.Framework.Commands
{
    /// <summary>A summary of patch info shown in the SMAPI console.</summary>
    internal class PatchInfo
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The path to the patch from the root content file.</summary>
        public LogPathBuilder Path { get; }

        /// <summary>The <see cref="Path"/> without the content pack prefix.</summary>
        public string PathWithoutContentPackPrefix { get; }

        /// <summary>The patch type.</summary>
        public string Type { get; }

        /// <summary>The asset name to intercept.</summary>
        public string RawTargetAsset { get; }

        /// <summary>The parsed asset name (if available).</summary>
        public ITokenString ParsedTargetAsset { get; }

        /// <summary>The parsed conditions (if available).</summary>
        public Condition[] ParsedConditions { get; }

        /// <summary>The content pack which requested the patch.</summary>
        public ManagedContentPack ContentPack { get; }

        /// <summary>Whether the patch is loaded.</summary>
        public bool IsLoaded { get; }

        /// <summary>Whether the patch should be applied in the current context.</summary>
        public bool MatchesContext { get; }

        /// <summary>Whether the patch is currently applied.</summary>
        public bool IsApplied { get; }

        /// <summary>Diagnostic info about the patch.</summary>
        public IContextualState State { get; }

        /// <summary>The underlying patch, if any.</summary>
        public IPatch Patch { get; }


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="patch">The patch to represent.</param>
        public PatchInfo(DisabledPatch patch)
            : this(patch.Path, patch.Type)
        {
            this.RawTargetAsset = patch.AssetName;
            this.ContentPack = patch.ContentPack;
            this.State = new ContextualState().AddErrors(patch.ReasonDisabled);
        }

        /// <summary>Construct an instance.</summary>
        /// <param name="patch">The patch to represent.</param>
        public PatchInfo(IPatch patch)
            : this(patch.Path, patch.Type.ToString())
        {
            this.RawTargetAsset = patch.RawTargetAsset.Raw;
            this.ParsedTargetAsset = patch.RawTargetAsset;
            this.ParsedConditions = patch.Conditions;
            this.ContentPack = patch.ContentPack;
            this.IsLoaded = true;
            this.MatchesContext = patch.IsReady;
            this.IsApplied = patch.IsApplied;
            this.State = patch.GetDiagnosticState();
            this.Patch = patch;
        }

        /// <summary>Get a human-readable list of changes applied to the asset for display when troubleshooting.</summary>
        public IEnumerable<string> GetChangeLabels()
        {
            return this.Patch?.GetChangeLabels() ?? Enumerable.Empty<string>();
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="path">The path to the patch from the root content file.</param>
        /// <param name="type">The patch type.</param>
        private PatchInfo(LogPathBuilder path, string type)
        {
            this.Path = path;
            this.Type = type;

            this.PathWithoutContentPackPrefix = new LogPathBuilder(path.Segments.Skip(1).ToArray()).ToString();
        }
    }
}
