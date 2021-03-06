// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.
// This code was generated by a tool. Changes to this file may cause
// incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Runtime.CompilerServices;

namespace FlaxEngine
{
    /// <summary>
    /// Describes the camera projection and view. Provides information about how to render scene (viewport location and direction, etc.).
    /// </summary>
    [Serializable]
    public sealed partial class Camera : Actor
    {
        /// <summary>
        /// Creates new <see cref="Camera"/> object.
        /// </summary>
        private Camera() : base()
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="Camera"/> object.
        /// </summary>
        /// <returns>Created object.</returns>
#if UNIT_TEST_COMPILANT
        [Obsolete("Unit tests, don't support methods calls.")]
#endif
        [UnmanagedCall]
        public static Camera New()
        {
#if UNIT_TEST_COMPILANT
            throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
            return Internal_Create(typeof(Camera)) as Camera;
#endif
        }

        /// <summary>
        /// Gets or sets value indicating if camera should use perspective rendering mode, otherwise it will use orthographic projection.
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(20), EditorDisplay("Camera"), Tooltip("Enables perspective projection mode, otherwise uses ortho")]
        public bool UsePerspective
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetUsePerspective(unmanagedPtr); }
            set { Internal_SetUsePerspective(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets or sets camera's field of view (in degrees)
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(10), Limit(0, 179), EditorDisplay("Camera", "Field Of View"), Tooltip("Field of view angle in degrees")]
        public float FieldOfView
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetFOV(unmanagedPtr); }
            set { Internal_SetFOV(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets or sets the custom aspect ratio. 0 if not use custom value.
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(50), Limit(0, 10, 0.01f), EditorDisplay("Camera"), Tooltip("Custom aspect ratio to use. Set to 0 to disable.")]
        public float CustomAspectRatio
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetCustomAspectRatio(unmanagedPtr); }
            set { Internal_SetCustomAspectRatio(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets or sets camera's near plane distance
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(30), Limit(0, 1000, 0.05f), EditorDisplay("Camera"), Tooltip("Near clipping plane distance")]
        public float NearPlane
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetNearPlane(unmanagedPtr); }
            set { Internal_SetNearPlane(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets or sets camera's far plane distance
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(40), Limit(0, 1000000, 5), EditorDisplay("Camera"), Tooltip("Far clipping plane distance")]
        public float FarPlane
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetFarPlane(unmanagedPtr); }
            set { Internal_SetFarPlane(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets or sets the orthographic projection scale
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(60), Limit(0.0001f, 1000, 0.01f), EditorDisplay("Camera"), Tooltip("Orthographic projection scale")]
        public float OrthographicScale
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetOrthographicScale(unmanagedPtr); }
            set { Internal_SetOrthographicScale(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets camera viewport
        /// </summary>
        [UnmanagedCall]
        public Viewport Viewport
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { Viewport resultAsRef; Internal_GetViewport(unmanagedPtr, out resultAsRef); return resultAsRef; }
#endif
        }

        /// <summary>
        /// Gets camera view matrix.
        /// </summary>
        [UnmanagedCall]
        public Matrix View
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { Matrix resultAsRef; Internal_GetView(unmanagedPtr, out resultAsRef); return resultAsRef; }
#endif
        }

        /// <summary>
        /// Gets camera projection matrix.
        /// </summary>
        [UnmanagedCall]
        public Matrix Projection
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { Matrix resultAsRef; Internal_GetProjection(unmanagedPtr, out resultAsRef); return resultAsRef; }
#endif
        }

        /// <summary>
        /// Gets the current main camera. This object is used for scene rendering. May be null if no camera is available.
        /// </summary>
        [UnmanagedCall]
        public static Camera MainCamera
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetMainCamera(); }
#endif
        }

        #region Internal Calls

#if !UNIT_TEST_COMPILANT
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern bool Internal_GetUsePerspective(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetUsePerspective(IntPtr obj, bool val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern float Internal_GetFOV(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetFOV(IntPtr obj, float val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern float Internal_GetCustomAspectRatio(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetCustomAspectRatio(IntPtr obj, float val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern float Internal_GetNearPlane(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetNearPlane(IntPtr obj, float val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern float Internal_GetFarPlane(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetFarPlane(IntPtr obj, float val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern float Internal_GetOrthographicScale(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetOrthographicScale(IntPtr obj, float val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_GetViewport(IntPtr obj, out Viewport resultAsRef);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_GetView(IntPtr obj, out Matrix resultAsRef);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_GetProjection(IntPtr obj, out Matrix resultAsRef);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Camera Internal_GetMainCamera();
#endif

        #endregion
    }
}
