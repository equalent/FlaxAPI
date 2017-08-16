////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2017 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System;

/// <summary>
/// Attibute stops serialization of given property since it is calling unmanaged code.
/// </summary>
internal class UnmanagedCallAttribute : Attribute
{
}