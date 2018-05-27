// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.
// This code was generated by a tool. Changes to this file may cause
// incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Runtime.CompilerServices;

namespace FlaxEngine
{
    /// <summary>
    /// Scene root actor object
    /// </summary>
    [Serializable]
    public sealed partial class Scene : Actor
    {
        /// <summary>
        /// Creates new <see cref="Scene"/> object.
        /// </summary>
        private Scene() : base()
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="Scene"/> object.
        /// </summary>
        /// <returns>Created object.</returns>
#if UNIT_TEST_COMPILANT
        [Obsolete("Unit tests, don't support methods calls.")]
#endif
        [UnmanagedCall]
        public static Scene New()
        {
#if UNIT_TEST_COMPILANT
            throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
            return Internal_Create(typeof(Scene)) as Scene;
#endif
        }

        /// <summary>
        /// Gets path to the scene file. It's valid only in Editor.
        /// </summary>
        [UnmanagedCall]
        [HideInEditor]
        public string Path
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetPath(unmanagedPtr); }
#endif
        }

        /// <summary>
        /// Gets filename of the scene file. It's valid only in Editor.
        /// </summary>
        [UnmanagedCall]
        [HideInEditor]
        public string Filename
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetFilename(unmanagedPtr); }
#endif
        }

        /// <summary>
        /// Gets path to the scene data folder. It's valid only in Editor.
        /// </summary>
        [UnmanagedCall]
        [HideInEditor]
        public string DataFolderPath
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetDataFolderPath(unmanagedPtr); }
#endif
        }

        /// <summary>
        /// Removes all baked lightmap textures from the scene.
        /// </summary>
#if UNIT_TEST_COMPILANT
        [Obsolete("Unit tests, don't support methods calls.")]
#endif
        [UnmanagedCall]
        public void ClearLightmaps()
        {
#if UNIT_TEST_COMPILANT
            throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
            Internal_ClearLightmaps(unmanagedPtr);
#endif
        }

        /// <summary>
        /// Builds the CSG geometry for the given scene.
        /// </summary>
#if UNIT_TEST_COMPILANT
        [Obsolete("Unit tests, don't support methods calls.")]
#endif
        [UnmanagedCall]
        public void BuildCSG()
        {
#if UNIT_TEST_COMPILANT
            throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
            Internal_BuildCSG(unmanagedPtr);
#endif
        }

        #region Internal Calls

#if !UNIT_TEST_COMPILANT
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string Internal_GetPath(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string Internal_GetFilename(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string Internal_GetDataFolderPath(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_ClearLightmaps(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_BuildCSG(IntPtr obj);
#endif

        #endregion
    }
}
