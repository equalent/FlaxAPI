// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.
// This code was generated by a tool. Changes to this file may cause
// incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Runtime.CompilerServices;

namespace FlaxEngine
{
    /// <summary>
    /// Static class for simple message dialogs.
    /// </summary>
    public static partial class MessageBox
    {
        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="parent">Parant window or null if not used.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the Buttons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the Icon values that specifies which icon to display in the message box.</param>
        /// <returns>One of the DialogResult values</returns>
#if UNIT_TEST_COMPILANT
        [Obsolete("Unit tests, don't support methods calls.")]
#endif
        [UnmanagedCall]
        public static DialogResult Show(Window parent, string text, string caption, Buttons buttons, Icon icon)
        {
#if UNIT_TEST_COMPILANT
            throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
            return Internal_Show(Object.GetUnmanagedPtr(parent), text, caption, buttons, icon);
#endif
        }

        /// <summary>
        /// Displays a standard dialog box that prompts the user to open a file(s).
        /// </summary>
        /// <param name="parent">Parant window or null if not used.</param>
        /// <param name="initialDirectory">The initial directory to show.</param>
        /// <param name="filter">The files filter text. Eg. 'All files (*.*)\0*.*\0'.</param>
        /// <param name="multiselect">Enable or disable support to select more than one file.</param>
        /// <param name="title">The dialog window title.</param>
        /// <returns>Selected file path(s) or null if cancelled.</returns>
#if UNIT_TEST_COMPILANT
        [Obsolete("Unit tests, don't support methods calls.")]
#endif
        [UnmanagedCall]
        public static string[] OpenFileDialog(Window parent, string initialDirectory, string filter, bool multiselect, string title)
        {
#if UNIT_TEST_COMPILANT
            throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
            return Internal_OpenFileDialog(Object.GetUnmanagedPtr(parent), initialDirectory, filter, multiselect, title);
#endif
        }

        #region Internal Calls

#if !UNIT_TEST_COMPILANT
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern DialogResult Internal_Show(IntPtr parent, string text, string caption, Buttons buttons, Icon icon);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string[] Internal_OpenFileDialog(IntPtr parent, string initialDirectory, string filter, bool multiselect, string title);
#endif

        #endregion
    }
}
