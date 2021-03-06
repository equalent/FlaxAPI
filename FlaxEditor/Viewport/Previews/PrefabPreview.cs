// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.

using FlaxEngine;
using Object = FlaxEngine.Object;

namespace FlaxEditor.Viewport.Previews
{
    /// <summary>
    /// Prefab asset preview editor viewport.
    /// </summary>
    /// <seealso cref="AssetPreview" />
    public class PrefabPreview : AssetPreview
    {
        private Prefab _prefab;
        private Actor _instance;

        /// <summary>
        /// Gets or sets the prefab asset to preview.
        /// </summary>
        public Prefab Prefab
        {
            get => _prefab;
            set
            {
                if (_prefab != value)
                {
                    if (_instance)
                    {
                        Task.CustomActors.Remove(_instance);
                        Object.Destroy(_instance);
                    }

                    _prefab = value;

                    if (_prefab)
                    {
                        _prefab.WaitForLoaded(); // TODO: use lazy prefab spawning to reduce stalls
                        _instance = PrefabManager.SpawnPrefab(_prefab, null);
                        if (_instance == null)
                        {
                            _prefab = null;
                            throw new FlaxException("Failed to spawn a prefab for the preview.");
                        }
                        Task.CustomActors.Add(_instance);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the instance of the prefab spawned for the preview.
        /// </summary>
        public Actor Instance => _instance;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PrefabPreview"/> class.
        /// </summary>
        /// <param name="useWidgets">if set to <c>true</c> use widgets.</param>
        public PrefabPreview(bool useWidgets)
        : base(useWidgets)
        {
        }

        /// <inheritdoc />
        public override void OnDestroy()
        {
            // Cleanup
            Prefab = null;

            base.OnDestroy();
        }
    }
}
