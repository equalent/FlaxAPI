// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.
// This code was generated by a tool. Changes to this file may cause
// incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Runtime.CompilerServices;

namespace FlaxEngine
{
    /// <summary>
    /// Base class from which every every script derives.
    /// </summary>
    public abstract partial class Script : Object
    {
        /// <summary>
        /// Enable/disable script updates.
        /// </summary>
        [UnmanagedCall]
        [HideInEditor]
        public bool Enabled
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetEnabled(unmanagedPtr); }
            set { Internal_SetEnabled(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets the actor owning that script.
        /// </summary>
        [UnmanagedCall]
        public Actor Actor
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetActor(unmanagedPtr); }
#endif
        }

        /// <summary>
        /// Gets or sets zero-based index in parent actor scripts list.
        /// </summary>
        [UnmanagedCall]
        [HideInEditor]
        public int OrderInParent
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetOrderInParent(unmanagedPtr); }
            set { Internal_SetOrderInParent(unmanagedPtr, value); }
#endif
        }

        #region Internal Calls

#if !UNIT_TEST_COMPILANT
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern bool Internal_GetEnabled(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetEnabled(IntPtr obj, bool val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Actor Internal_GetActor(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern int Internal_GetOrderInParent(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetOrderInParent(IntPtr obj, int val);
#endif

        #endregion
    }
}
