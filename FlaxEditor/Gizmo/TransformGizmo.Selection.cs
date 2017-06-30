////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2017 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using FlaxEngine;

namespace FlaxEditor.Gizmo
{
    public partial class TransformGizmo
    {
        /// <summary>
        /// Checks if given actor is selected.
        /// </summary>
        /// <param name="actor">Actor to check</param>
        /// <returns>True if is selected, otherwise false.</returns>
        public bool IsActorSelected(Actor actor)
        {
            return _selection.Contains(actor);
        }

        /// <summary>
        /// Tries to select any actor.
        /// </summary>
        /// <param name="addRemove">True if use add/remove mode.</param>
        public void PickActors(bool addRemove)
        {
            // Check if gismo is disaled
            if (!IsActive)
                return;

            // Ensure player is not moving objects
            if (_activeAxis != Axis.None)
                return;
            
            // End current action
            EndTransforming();

            // Cache 'before' state
            var before = new List<ITransformable>(_selection);

            // Try to pick object
            /*var mosueRay = _parent->GetMouseRay();
            var hit = RaycastScene(mosueRay);

            // Process test result
            if (!addRemove && _selection.Count > 0)
            {
                _selection.Clear();

            }
            if (hit != null)
            {
                bool isSelected = _selection.Contains(hit);
                if (addRemove)
                {
                    if (isSelected)
                        _selection.Remove(hit);
                    else
                        _selection.Add(hit);
                }
                else if (!isSelected)
                {
                    _selection.Add(hit);
                }
            }*/

            /*// Check if selection has been changed
            var ur = _parent->GetUndoRedo();
            if (before != _selection)
            {
                // Change selection
                if (ur->IsDisabled())
                {
                    auto actors = _selection;
                    select(actors, INVALID_INDEX);
                }
                else
                {
                    ur->AddAction(new SelectionChanged(this, before, _selection, _subObjectIndex, INVALID_INDEX))->Perform();
                }
            }
            else
            {
                // Check if selected is only single object
                if (_selection.Count == 1)
                {
                    // Try to get sub object
                    int32 subObject = _selection[0]->IntersectsSubObjectEditor(mosueRay);

                    // Check if has been changed
                    if (_subObjectIndex != subObject)
                    {
                        // Change selection
                        if (ur->IsDisabled())
                        {
                            auto actors = _selection;
                            select(actors, subObject);
                        }
                        else
                        {
                            ur->AddAction(new SelectionChanged(this, before, _selection, _subObjectIndex, subObject))->Perform();
                        }
                    }
                }
            }*/

            // Clear deltas
            _tDelta = Vector3.Zero;
            _lastIntersectionPosition = Vector3.Zero;
        }

        /// <inheritdoc />
        public override void OnSelectionChanged(List<object> newSelection)
        {
            // ... 
        }

        /// <summary>
        /// Selects actor.
        /// </summary>
        /// <param name="actor">Actor</param>
        public void Select(Actor actor)
        {
            
        }

        /// <summary>
        /// Selects actors.
        /// </summary>
        /// <param name="a">Actors</param>
        public void Select(List<Actor> a)
        {
            
        }

        /// <summary>
        /// Removes actor from the selection
        /// </summary>
        /// <param name="actor">Actor to deselect.</param>
        public void Deselect(Actor actor)
        {
            
        }

        /// <summary>
        /// Removes actors from the selection.
        /// </summary>
        /// <param name="actors">Actors to deselect.</param>
        public void Deselect(IEnumerable<Actor> actors)
        {
            
        }

        /// <summary>
        /// Deselect all actors.
        /// </summary>
        public void Deselect()
        {
            
        }
    }
}
