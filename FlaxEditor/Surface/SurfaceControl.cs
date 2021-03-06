// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.

using FlaxEngine;
using FlaxEngine.GUI;

namespace FlaxEditor.Surface
{
    /// <summary>
    /// Base class for Visject Surface components like nodes or comments. Used to unify various logic parts across different surface elements.
    /// </summary>
    /// <seealso cref="FlaxEngine.GUI.ContainerControl" />
    public abstract class SurfaceControl : ContainerControl
    {
        /// <summary>
        /// The mouse position in local control space. Updates by auto.
        /// </summary>
        protected Vector2 _mousePosition;

        /// <summary>
        /// The is selected flag for element.
        /// </summary>
        protected bool _isSelected;

        /// <summary>
        /// The surface.
        /// </summary>
        public readonly VisjectSurface Surface;

        /// <summary>
        /// Gets a value indicating whether this control is selected.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            internal set { _isSelected = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SurfaceControl"/> class.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="width">The initial width.</param>
        /// <param name="height">The initial height.</param>
        protected SurfaceControl(VisjectSurface surface, float width, float height)
        : base(0, 0, width, height)
        {
            ClipChildren = false;

            Surface = surface;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SurfaceControl"/> class.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="bounds">The initial bounds.</param>
        protected SurfaceControl(VisjectSurface surface, ref Rectangle bounds)
        : base(bounds)
        {
            ClipChildren = false;

            Surface = surface;
        }

        /// <summary>
        /// Determines whether this element can be selected by the mouse at the specified location.
        /// </summary>
        /// <param name="location">The mouse location (in surface space).</param>
        /// <returns><c>true</c> if this instance can be selected by mouse at the specified location; otherwise, <c>false</c>.</returns>
        public abstract bool CanSelect(ref Vector2 location);

        /// <summary>
        /// Determines whether selection rectangle is intersecting with the surface control area that can be selected.
        /// </summary>
        /// <param name="selectionRect">The selection rectangle (in surface space, not the control space).</param>
        /// <returns><c>true</c> if the selection rectangle is intersecting with the selectable parts of the control ; otherwise, <c>false</c>.</returns>
        public virtual bool IsSelectionIntersecting(ref Rectangle selectionRect)
        {
            return Bounds.Intersects(ref selectionRect);
        }

        /// <summary>
        /// Called when control gets loaded and added to surface.
        /// </summary>
        public virtual void OnLoaded()
        {
        }

        /// <summary>
        /// Called when surface gets loaded and nodes boxes are connected.
        /// </summary>
        public virtual void OnSurfaceLoaded()
        {
            UpdateRectangles();
        }

        /// <summary>
        /// Updates the cached rectangles on control size change.
        /// </summary>
        protected abstract void UpdateRectangles();

        /// <inheritdoc />
        public override void OnMouseEnter(Vector2 location)
        {
            _mousePosition = location;

            base.OnMouseEnter(location);
        }

        /// <inheritdoc />
        public override void OnMouseMove(Vector2 location)
        {
            _mousePosition = location;

            base.OnMouseMove(location);
        }

        /// <inheritdoc />
        public override void OnMouseLeave()
        {
            _mousePosition = Vector2.Minimum;

            base.OnMouseLeave();
        }

        /// <inheritdoc />
        protected override void SetScaleInternal(ref Vector2 scale)
        {
            base.SetScaleInternal(ref scale);

            UpdateRectangles();
        }

        /// <inheritdoc />
        protected override void SetSizeInternal(ref Vector2 size)
        {
            base.SetSizeInternal(ref size);

            UpdateRectangles();
        }
    }
}
