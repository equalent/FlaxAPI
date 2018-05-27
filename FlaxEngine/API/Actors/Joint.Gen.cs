// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.
// This code was generated by a tool. Changes to this file may cause
// incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Runtime.CompilerServices;

namespace FlaxEngine
{
    /// <summary>
    /// A base class for all Joint types. Joints constrain how two rigidbodies move relative to one another (for example a door hinge). One of the bodies in the joint must always be movable (non-kinematic).
    /// </summary>
    [Serializable]
    public abstract partial class Joint : Actor
    {
        /// <summary>
        /// Gets or sets the target actor for the joint. It has to be RigidBody or CharacterController.
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(0), EditorDisplay("Joint"), Tooltip("The target actor for the joint. It has to be RigidBody or CharacterController")]
        public Actor Target
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetTarget(unmanagedPtr); }
            set { Internal_SetTarget(unmanagedPtr, Object.GetUnmanagedPtr(value)); }
#endif
        }

        /// <summary>
        /// Gets or sets the break force. Determines the maximum force the joint can apply before breaking. Broken joints no longer participate in physics simulation.
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(10), EditorDisplay("Joint"), Tooltip("Determines the maximum force the joint can apply before breaking. Broken joints no longer participate in physics simulation.")]
        public float BreakForce
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetBreakForce(unmanagedPtr); }
            set { Internal_SetBreakForce(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets or sets the break torque. Determines the maximum torque the joint can apply before breaking. Broken joints no longer participate in physics simulation.
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(20), EditorDisplay("Joint"), Tooltip("Determines the maximum torque the joint can apply before breaking. Broken joints no longer participate in physics simulation.")]
        public float BreakTorque
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetBreakTorque(unmanagedPtr); }
            set { Internal_SetBreakTorque(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Determines whether collisions between the two bodies managed by the joint are enabled.
        /// </summary>
        [UnmanagedCall]
        [EditorOrder(30), EditorDisplay("Joint"), Tooltip("Determines whether collisions between the two bodies managed by the joint are enabled.")]
        public bool EnableCollision
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { return Internal_GetEnableCollision(unmanagedPtr); }
            set { Internal_SetEnableCollision(unmanagedPtr, value); }
#endif
        }

        /// <summary>
        /// Gets or sets the target anchor.
        /// </summary>
        /// <remarks>
        /// This is the relative pose which locates the joint frame relative to the target actor.
        /// </remarks>
        [UnmanagedCall]
        [EditorOrder(40), EditorDisplay("Joint"), Tooltip("This is the relative pose which locates the joint frame relative to the target actor.")]
        public Vector3 TargetAnchor
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { Vector3 resultAsRef; Internal_GetTargetAnchor(unmanagedPtr, out resultAsRef); return resultAsRef; }
            set { Internal_SetTargetAnchor(unmanagedPtr, ref value); }
#endif
        }

        /// <summary>
        /// Gets or sets the target anchor rotation.
        /// </summary>
        /// <remarks>
        /// This is the relative pose rotation which locates the joint frame relative to the target actor.
        /// </remarks>
        [UnmanagedCall]
        [EditorOrder(50), EditorDisplay("Joint"), Tooltip("This is the relative pose rotation which locates the joint frame relative to the target actor.")]
        public Quaternion TargetAnchorRotation
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { Quaternion resultAsRef; Internal_GetTargetAnchorRotation(unmanagedPtr, out resultAsRef); return resultAsRef; }
            set { Internal_SetTargetAnchorRotation(unmanagedPtr, ref value); }
#endif
        }

        /// <summary>
        /// Gets the current force applied by the solver to maintain all constraints.
        /// </summary>
        [UnmanagedCall]
        public Vector3 CurrentForce
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { Vector3 resultAsRef; Internal_GetCurrentForce(unmanagedPtr, out resultAsRef); return resultAsRef; }
#endif
        }

        /// <summary>
        /// Gets the current torque applied by the solver to maintain all constraints.
        /// </summary>
        [UnmanagedCall]
        public Vector3 CurrentTorque
        {
#if UNIT_TEST_COMPILANT
            get; set;
#else
            get { Vector3 resultAsRef; Internal_GetCurrentTorque(unmanagedPtr, out resultAsRef); return resultAsRef; }
#endif
        }

        #region Internal Calls

#if !UNIT_TEST_COMPILANT
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Actor Internal_GetTarget(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetTarget(IntPtr obj, IntPtr val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern float Internal_GetBreakForce(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetBreakForce(IntPtr obj, float val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern float Internal_GetBreakTorque(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetBreakTorque(IntPtr obj, float val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern bool Internal_GetEnableCollision(IntPtr obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetEnableCollision(IntPtr obj, bool val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_GetTargetAnchor(IntPtr obj, out Vector3 resultAsRef);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetTargetAnchor(IntPtr obj, ref Vector3 val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_GetTargetAnchorRotation(IntPtr obj, out Quaternion resultAsRef);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_SetTargetAnchorRotation(IntPtr obj, ref Quaternion val);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_GetCurrentForce(IntPtr obj, out Vector3 resultAsRef);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_GetCurrentTorque(IntPtr obj, out Vector3 resultAsRef);
#endif

        #endregion
    }
}
