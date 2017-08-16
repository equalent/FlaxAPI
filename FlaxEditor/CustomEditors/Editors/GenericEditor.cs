////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2017 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FlaxEditor.CustomEditors.Elements;
using FlaxEngine;

namespace FlaxEditor.CustomEditors.Editors
{
    /// <summary>
    /// Default implementation of the inspector used when no specified inspector is provided for the type. Inspector 
    /// displays GUI for all the inspectable fields in the object.
    /// </summary>
    public sealed class GenericEditor : CustomEditor
    {
        private readonly List<CustomEditor> children = new List<CustomEditor>();

        private struct PropertyItemInfo : IComparable
        {
            public PropertyInfo Info;
            public EditorOrderAttribute Order;
            public EditorDisplayAttribute Display;

            /// <summary>
            /// Gets the display name.
            /// </summary>
            /// <value>
            /// The display name.
            /// </value>
            public string DisplayName => Display?.Name ?? Info.Name;

            /// <summary>
            /// Gets a value indicating whether use dedicated group.
            /// </summary>
            /// <value>
            ///   <c>true</c> if use group; otherwise, <c>false</c>.
            /// </value>
            public bool UseGroup => Display?.Group != null;

            /// <inheritdoc />
            public int CompareTo(object obj)
            {
                if (obj is PropertyItemInfo other)
                {
                    // By order
                    if (Order != null)
                    {
                        if (other.Order != null)
                            return Order.Order - other.Order.Order;
                        return -1;
                    }
                    else if (other.Order != null)
                        return 1;

                    // By name
                    return string.Compare(Info.Name, other.Info.Name, StringComparison.InvariantCulture);
                }

                return 0;
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return Info.Name;
            }
        }

        /// <inheritdoc />
        public override void Initialize(LayoutElementsContainer layout)
        {
            if (Values == null)
                return;

            // TODO: for structures get all public fields
            // TODO: for objects get all public properties
            // TODO: support attribues
            // TODO: spawn custom editors for every editable thing
            // TODO; use shared properties/fields across all selected objects values

            // Faster path for the same objects selected
            if (HasDiffrentTypes == false)
            {
                var type = Values[0].GetType();

                //layout.Button("Type " + type.Name);

                if (type.IsArray)
                {
                    layout.Button("Array: " + type.Name);
                }
                else if (type.IsValueType)
                {
                    layout.Button("ValueType: " + type.Name);
                }
                else if (type.IsClass)
                {
                    layout.Button("Type " + type.Name);
                    layout.Space(10);

                    // TODO: promote children to other base class like CustomEditorContainer ?
                    
                    // Process the properties
                    var properties = type.GetProperties();
                    var propertyItems = new List<PropertyItemInfo>(properties.Length);
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var p = properties[i];
                        var getter = p.GetMethod;

                        // Skip hidden properties and only set properties
                        if (getter == null || !getter.IsPublic || p.GetIndexParameters().GetLength(0) != 0)
                        {
                            continue;
                        }

                        var attributes = p.GetCustomAttributes(true);
                        if (attributes.Any(x => x is HideInEditorAttribute))
                        {
                            continue;
                        }

                        PropertyItemInfo item;
                        item.Info = p;
                        item.Order = (EditorOrderAttribute)attributes.FirstOrDefault(x => x is EditorOrderAttribute);
                        item.Display = (EditorDisplayAttribute)attributes.FirstOrDefault(x => x is EditorDisplayAttribute);
                        // TODO: support custom editor type via CustomClassEditorAttribute

                        propertyItems.Add(item);
                    }

                    // Sort items
                    propertyItems.Sort();

                    // Add items
                    GroupElement lastGroup = null;
                    for (int i = 0; i < propertyItems.Count; i++)
                    {
                        var item = propertyItems[i];

                        // Check if use group
                        LayoutElementsContainer itemLayout;
                        if (item.UseGroup)
                        {
                            if (lastGroup == null || lastGroup.Panel.Name != item.Display.Group)
                                lastGroup = layout.Group(item.Display.Group);
                            itemLayout = lastGroup;
                        }
                        else
                        {
                            lastGroup = null;
                            itemLayout = layout;
                        }

                        // Peek values
                        ValueContainer itemValues;
                        try
                        {
                            itemValues = new ValueContainer(Values.Count);
                            for (int j = 0; j < Values.Count; j++)
                                itemValues.Add(item.Info.GetValue(Values[j]));
                        }
                        catch (Exception ex)
                        {
                            Editor.LogWarning("Failed to get object values " + ex.Message);
                            Editor.LogWarning(type.FullName + '.' + item);
                            Editor.LogWarning(ex.StackTrace);
                            return;
                        }

                        // Spawn child editor
                        itemLayout.Button(item.DisplayName + " order: " + (item.Order != null ? item.Order.Order.ToString() : "?"));
                        //Debug.Log("Child item " + item);
                        //var child = itemLayout.Object(itemValues);
                        //children.Add(child);
                    }
                }
                else
                {
                    layout.Button("No class type: " + type.Name);
                }
            }
            else
            {
                layout.Button("More than object selected");
            }
        }

        /// <inheritdoc />
        public override void Refresh()
        {
            for (int i = 0; i < children.Count; i++)
                children[i].Refresh();
        }
    }
}