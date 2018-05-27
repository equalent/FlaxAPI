// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.
// This code was generated by a tool. Changes to this file may cause
// incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Runtime.CompilerServices;

namespace FlaxEngine
{
    /// <summary>
    /// Texture asset contains an image that is usually stored on a GPU and is used during rendering graphics.
    /// </summary>
    public partial class Texture : TextureBase
    {
        /// <summary>
        /// Creates new <see cref="Texture"/> object.
        /// </summary>
        private Texture() : base()
        {
        }

        /// <summary>
        /// Returns true if texture is a normal map, otherwise false.
        /// </summary>
        [UnmanagedCall]
        public bool IsNormalMap
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetIsNormalMap(unmanagedPtr); }
#endif
        }

        #region Internal Calls

#if !UNIT_TEST_COMPILANT
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern bool Internal_GetIsNormalMap(IntPtr obj);
#endif

        #endregion
    }
}
