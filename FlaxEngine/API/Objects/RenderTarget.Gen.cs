////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2017 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;

namespace FlaxEngine.Rendering
{
	/// <summary>
	/// Allows to perform custom rendering to texture.
	/// </summary>
	public partial class RenderTarget : Object
	{
		/// <summary>
		/// Gets texture surface format.
		/// </summary>
		[UnmanagedCall]
		public PixelFormat Format
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetFormat(unmanagedPtr); }
#endif
		}

		/// <summary>
		/// Gets a value indicating whether this texture has been allocated.
		/// </summary>
		[UnmanagedCall]
		public bool IsAllocated
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetIsAllocated(unmanagedPtr); }
#endif
		}

		/// <summary>
		/// Gets or sets texture surface width (in pixels).
		/// </summary>
		[UnmanagedCall]
		public int Width
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetWidth(unmanagedPtr); }
			set { Internal_SetWidth(unmanagedPtr, value); }
#endif
		}

		/// <summary>
		/// Gets or sets texture surface height (in pixels).
		/// </summary>
		[UnmanagedCall]
		public int Height
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetHeight(unmanagedPtr); }
			set { Internal_SetHeight(unmanagedPtr, value); }
#endif
		}

		/// <summary>
		/// Gets or sets texture surface size (in pixels).
		/// </summary>
		[UnmanagedCall]
		public Vector2 Size
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { Vector2 resultAsRef; Internal_GetSize(unmanagedPtr, out resultAsRef); return resultAsRef; }
			set { Internal_SetSize(unmanagedPtr, ref value); }
#endif
		}

		/// <summary>
		/// Creates the new render target object.
		/// </summary>
		/// <param name="format">The surface pixels format.</param>
		/// <param name="width">The surface width in pixels.</param>
		/// <param name="height">The surface height in pixels.</param>
		/// <param name="flags">The surface usage flags.</param>
		/// <returns>Created render target object which is already allocated or null if cannot perform this action.</returns>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static RenderTarget Create(PixelFormat format, int width, int height, TextureFlags flags = TextureFlags.ShaderResource | TextureFlags.RenderTarget) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			return Internal_Create(format, width, height, flags);
#endif
		}

		/// <summary>
		/// Disposes render target surface data.
		/// </summary>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public void Dispose() 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_Dispose(unmanagedPtr);
#endif
		}

#region Internal Calls
#if !UNIT_TEST_COMPILANT
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern PixelFormat Internal_GetFormat(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_GetIsAllocated(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetWidth(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetWidth(IntPtr obj, int val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetHeight(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetHeight(IntPtr obj, int val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_GetSize(IntPtr obj, out Vector2 resultAsRef);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetSize(IntPtr obj, ref Vector2 val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RenderTarget Internal_Create(PixelFormat format, int width, int height, TextureFlags flags);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Dispose(IntPtr obj);
#endif
#endregion
	}
}

