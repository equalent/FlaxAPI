// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.

using FlaxEngine;

namespace FlaxEditor.Viewport.Cameras
{
    /// <summary>
    /// The interface for the editor viewport camera controllers. Handles the input logic updates and the preview rendering viewport.
    /// </summary>
    public interface IViewportCamera
    {
        /// <summary>
        /// Updates the camera.
        /// </summary>
        /// <param name="deltaTime">The delta time (in seconds).</param>
        void Update(float deltaTime);

        /// <summary>
        /// Updates the view.
        /// </summary>
        /// <param name="dt">The delta time (in seconds).</param>
        /// <param name="moveDelta">The move delta (scaled).</param>
        /// <param name="mouseDelta">The mouse delta (scaled).</param>
        void UpdateView(float dt, ref Vector3 moveDelta, ref Vector2 mouseDelta);
    }
}
