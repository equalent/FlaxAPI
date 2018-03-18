////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2018 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Xml;
using FlaxEditor.Content;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.GUI;
using FlaxEditor.GUI;
using FlaxEditor.Surface;
using FlaxEditor.Viewport.Previews;
using FlaxEngine;
using FlaxEngine.GUI;
// ReSharper disable MemberCanBePrivate.Local

namespace FlaxEditor.Windows.Assets
{
	/// <summary>
	/// Animation Graph window allows to view and edit <see cref="AnimationGraph"/> asset.
	/// Note: it uses ClonedAssetEditorWindowBase which is creating cloned asset to edit/preview.
	/// </summary>
	/// <seealso cref="AnimationGraph" />
	/// <seealso cref="FlaxEditor.Windows.Assets.AssetEditorWindow" />
	/// <seealso cref="FlaxEditor.Surface.IVisjectSurfaceOwner" />
	public sealed class AnimationGraphWindow : ClonedAssetEditorWindowBase<AnimationGraph>, IVisjectSurfaceOwner
	{
		internal static Guid BaseModelId = new Guid(1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

		/// <summary>
		/// The graph properties proxy object.
		/// </summary>
		private sealed class PropertiesProxy
		{
			[EditorOrder(10), EditorDisplay("General"), Tooltip("The base model used to preview the animation and prepare the graph (skeleton bones sstructure must match in instanced AnimationModel actors)")]
			public SkinnedModel BaseModel { get; set; }
			
			[EditorOrder(1000), EditorDisplay("Parameters"), CustomEditor(typeof(ParametersEditor))]
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public AnimationGraphWindow WindowReference { get; set; }

			/// <summary>
			/// Custom editor for editing graph parameters collection.
			/// </summary>
			/// <seealso cref="FlaxEditor.CustomEditors.CustomEditor" />
			public class ParametersEditor : CustomEditor
			{
				private int _parametersHash;

				private enum NewParameterType
				{
					Bool = (int)ParameterType.Bool,
					Inteager = (int)ParameterType.Inteager,
					Float = (int)ParameterType.Float,
					Vector2 = (int)ParameterType.Vector2,
					Vector3 = (int)ParameterType.Vector3,
					Vector4 = (int)ParameterType.Vector4,
					Color = (int)ParameterType.Color,
					Rotation = (int)ParameterType.Rotation,
					Transform = (int)ParameterType.Transform,
				}

				/// <inheritdoc />
				public override DisplayStyle Style => DisplayStyle.InlineIntoParent;

				/// <inheritdoc />
				public override void Initialize(LayoutElementsContainer layout)
				{
					var window = Values[0] as AnimationGraphWindow;
					var asset = window?.Asset;
					if (asset == null)
					{
						_parametersHash = -1;
						layout.Label("No parameters");
						return;
					}
					if (!asset.IsLoaded)
					{
						_parametersHash = -2;
						layout.Label("Loading...");
						return;
					}
					var parameters = window.PreviewModelActor.Parameters;
					if (parameters == null || parameters.Length == 0)
					{
						_parametersHash = -1;
						layout.Label("No parameters");
						return;
					}
					_parametersHash = window.PreviewModelActor._parametersHash;
					
					for (int i = 0; i < parameters.Length; i++)
					{
						var p = parameters[i];
						if (!p.IsPublic)
							continue;

						var pIndex = i;
						var pValue = p.Value;
						var pGuidType = false;
						Type pType;
						switch (p.Type)
						{
							case AnimationGraphParameterType.Asset:
								pType = typeof(Asset);
								pGuidType = true;
								break;
							default:
								pType = p.Value.GetType();
								break;
						}

						var propertyValue = new CustomValueContainer(
							pType,
							pValue,
							(instance, index) =>
							{
								// Get parameter
								var win = (AnimationGraphWindow)instance;
								return win.PreviewModelActor.Parameters[pIndex].Value;
							},
							(instance, index, value) =>
							{
								// Set parameter and surface parameter
								var win = (AnimationGraphWindow)instance;

								// Visject surface paramaters are only value type objects so convert value if need to (eg. instead of texture ref write texture id)
								var surfaceParam = value;
								if (pGuidType)
									surfaceParam = (value as FlaxEngine.Object)?.ID ?? Guid.Empty;

								win.PreviewModelActor.Parameters[pIndex].Value = value;
								win.Surface.Parameters[pIndex].Value = surfaceParam;
								win._paramValueChange = true;
							}
						);

						var propertyLabel = new ClickablePropertyNameLabel(p.Name);
						propertyLabel.MouseRightClick += (label, location) => ShowParameterMenu(pIndex, label, ref location);
						var property = layout.AddPropertyItem(propertyLabel);
						property.Object(propertyValue);
					}

					if (parameters.Length > 0)
						layout.Space(10);

					// Parameters creating
					var paramType = layout.Enum(typeof(NewParameterType));
					paramType.Value = (int)NewParameterType.Float;
					var newParam = layout.Button("Add parameter");
					newParam.Button.Clicked += () => AddParameter((ParameterType)paramType.Value);
				}

				/// <summary>
				/// Shows the parameter context menu.
				/// </summary>
				/// <param name="index">The index.</param>
				/// <param name="label">The label control.</param>
				/// <param name="targetLocation">The target location.</param>
				private void ShowParameterMenu(int index, Control label, ref Vector2 targetLocation)
				{
					var contextMenu = new ContextMenu();
					contextMenu.AddButton("Rename", () => StartParameterRenaming(index, label));
					contextMenu.AddButton("Delete", () => DeleteParameter(index));
					contextMenu.Show(label, targetLocation);
				}

				/// <summary>
				/// Adds the parameter.
				/// </summary>
				/// <param name="type">The type.</param>
				private void AddParameter(ParameterType type)
				{
					var window = Values[0] as AnimationGraphWindow;
					var asset = window?.Asset;
					if (asset == null || !asset.IsLoaded)
						return;

					var param = SurfaceParameter.Create(type);
					window.Surface.Parameters.Add(param);
					window.Surface.OnParamCreated(param);
				}

				/// <summary>
				/// Starts renaming parameter.
				/// </summary>
				/// <param name="index">The index.</param>
				/// <param name="label">The label control.</param>
				private void StartParameterRenaming(int index, Control label)
				{
					var win = (AnimationGraphWindow)Values[0];
					var animatedModel = win.PreviewModelActor;
					var parameter = animatedModel.Parameters[index];
					var dialog = RenamePopup.Show(label, new Rectangle(0, 0, label.Width - 2, label.Height), parameter.Name, false);
					dialog.Tag = index;
					dialog.Renamed += OnParameterRenamed;
				}

				private void OnParameterRenamed(RenamePopup renamePopup)
				{
					var index = (int)renamePopup.Tag;
					var newName = renamePopup.Text;

					var win = (AnimationGraphWindow)Values[0];
					var param = win.Surface.Parameters[index];
					param.Name = newName;
					win.Surface.OnParamRenamed(param);
				}

				/// <summary>
				/// Removes the parameter.
				/// </summary>
				/// <param name="index">The index.</param>
				private void DeleteParameter(int index)
				{
					var win = (AnimationGraphWindow)Values[0];
					var param = win.Surface.Parameters[index];
					win.Surface.Parameters.RemoveAt(index);
					win.Surface.OnParamDeleted(param);
				}

				/// <inheritdoc />
				public override void Refresh()
				{
					var window = Values[0] as AnimationGraphWindow;
					var asset = window?.Asset;
					int parametersHash = -1;
					if (asset)
					{
						if (asset.IsLoaded)
						{
							var parameters = window.PreviewModelActor.Parameters;// need to ask for params here to sync valid hash   
							parametersHash = window.PreviewModelActor._parametersHash;
						}
						else
						{
							parametersHash = -2;
						}
					}

					if (parametersHash != _parametersHash)
					{
						// Parameters has been modifed (loaded/unloaded/edited)
						RebuildLayout();
					}
				}
			}

			/// <summary>
			/// Gathers parameters from the graph.
			/// </summary>
			/// <param name="window">The graph window.</param>
			public void OnLoad(AnimationGraphWindow window)
			{
				var model = window.PreviewModelActor;
				BaseModel = model.GetParam(BaseModelId).Value as SkinnedModel;
				
				WindowReference = window;
			}

			/// <summary>
			/// Saves the properties to the graph.
			/// </summary>
			/// <param name="window">The graph window.</param>
			public void OnSave(AnimationGraphWindow window)
			{
				var model = window.PreviewModelActor;
				model.GetParam(BaseModelId).Value = BaseModel;
			}

			/// <summary>
			/// Clears temporary data.
			/// </summary>
			public void OnClean()
			{
				// Unlink
				WindowReference = null;
			}
		}

		private readonly SplitPanel _split1;
		private readonly SplitPanel _split2;
		private readonly AnimatedModelPreview _preview;
		private readonly VisjectSurface _surface;

		private readonly ToolStripButton _saveButton;
		private readonly PropertiesProxy _properties;
		private bool _isWaitingForSurfaceLoad;
		private bool _tmpAssetIsDirty;
		internal bool _paramValueChange;

		/// <summary>
		/// Gets the graph surface.
		/// </summary>
		public VisjectSurface Surface => _surface;

		/// <summary>
		/// Gets the animated model actor used for the animation preview.
		/// </summary>
		public AnimatedModel PreviewModelActor => _preview.PreviewModelActor;

		/// <inheritdoc />
		public AnimationGraphWindow(Editor editor, AssetItem item)
			: base(editor, item)
		{
			// Split Panel 1
			_split1 = new SplitPanel(Orientation.Horizontal, ScrollBars.None, ScrollBars.None)
			{
				DockStyle = DockStyle.Fill,
				SplitterValue = 0.7f,
				Parent = this
			};

			// Split Panel 2
			_split2 = new SplitPanel(Orientation.Vertical, ScrollBars.None, ScrollBars.Vertical)
			{
				DockStyle = DockStyle.Fill,
				SplitterValue = 0.4f,
				Parent = _split1.Panel2
			};

			// Animation preview
			_preview = new AnimatedModelPreview(true)
			{
				Parent = _split2.Panel1
			};

			// Graph properties editor
			var propertiesEditor = new CustomEditorPresenter(null);
			propertiesEditor.Panel.Parent = _split2.Panel2;
			_properties = new PropertiesProxy();
			propertiesEditor.Select(_properties);
			propertiesEditor.Modified += OnGraphPropertyEdited;

			// Surface
			_surface = new VisjectSurface(this, SurfaceType.AnimationGraph)
			{
				Parent = _split1.Panel1,
				Enabled = false
			};

			// Toolstrip
			_saveButton = (ToolStripButton)_toolstrip.AddButton(Editor.UI.GetIcon("Save32"), Save).LinkTooltip("Save");
			_toolstrip.AddSeparator();
			_toolstrip.AddButton(editor.UI.GetIcon("PageScale32"), _surface.ShowWholeGraph).LinkTooltip("Show whole graph");
		}

		private void OnGraphPropertyEdited()
		{
			_surface.MarkAsEdited(!_paramValueChange);
			_paramValueChange = false;
		}

		/// <summary>
		/// Refreshes temporary asset to see changes live when editing the surface.
		/// </summary>
		/// <returns>True if cannot refresh it, otherwise false.</returns>
		public bool RefreshTempAsset()
		{
			// Ensure that asset is loaded
			if (_asset == null || !_asset.IsLoaded)
			{
				// Error
				return true;
			}
			if (_isWaitingForSurfaceLoad)
				return true;

			// Check if surface has been edited
			if (_surface.IsEdited)
			{
				// Save surface
				var data = _surface.Save();
				if (data == null)
				{
					// Error
					Editor.LogError("Failed to save animation graph surface");
					return true;
				}

				// Sync edited parameters
				_properties.OnSave(this);

				// Save data to the temporary asset
				if (_asset.SaveSurface(data))
				{
					// Error
					_surface.MarkAsEdited();
					Editor.LogError("Failed to save animation graph surface data");
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Gets or sets the main graph node.
		/// </summary>
		private Surface.Archetypes.Animation.SurfaceNodeAnimOutput MainNode
		{
			get
			{
				var mainNode = _surface.FindNode(1, 1) as Surface.Archetypes.Animation.SurfaceNodeAnimOutput;
				if (mainNode == null)
				{
					// Error
					Debug.LogError("Failed to find main graph node.");
				}
				return mainNode;
			}
		}

		/// <summary>
		/// Scrolls the view to the main graph node.
		/// </summary>
		public void ScrollViewToMain()
		{
			// Find main node
			var mainNode = MainNode;
			if (mainNode == null)
				return;

			// Change scale and position
			_surface.ViewScale = 0.5f;
			_surface.ViewCenterPosition = mainNode.Center;
		}

		/// <inheritdoc />
		public override void Save()
		{
			// Check if don't need to push any new changes to the orginal asset
			if (!IsEdited)
				return;

			// Just in case refresh data
			if (RefreshTempAsset())
			{
				// Error
				return;
			}

			// Update original asset so user can see changes in the scene
			if (SaveToOriginal())
			{
				// Error
				return;
			}
			
			// Clear flag
			ClearEditedFlag();

			// Update
			OnSurfaceEditedChanged();
			_item.RefreshThumbnail();
		}

		/// <inheritdoc />
		protected override void UpdateToolstrip()
		{
			_saveButton.Enabled = IsEdited;

			base.UpdateToolstrip();
		}

		/// <inheritdoc />
		protected override void UnlinkItem()
		{
			_properties.OnClean();
			PreviewModelActor.AnimationGraph = null;
			_isWaitingForSurfaceLoad = false;

			base.UnlinkItem();
		}

		/// <inheritdoc />
		protected override void OnAssetLinked()
		{
			PreviewModelActor.AnimationGraph = _asset;
			_isWaitingForSurfaceLoad = true;

			base.OnAssetLinked();
		}

		/// <inheritdoc />
		public void OnSurfaceSave()
		{
			Save();
		}

		/// <inheritdoc />
		public void OnSurfaceEditedChanged()
		{
			if (_surface.IsEdited)
				MarkAsEdited();
		}

		/// <inheritdoc />
		public void OnSurfaceGraphEdited()
		{
			// Mark as dirty
			_tmpAssetIsDirty = true;
		}

		/// <inheritdoc />
		public Texture GetSurfaceBackground()
		{
			return Editor.UI.VisjectSurfaceBackground;
		}

		/// <inheritdoc />
		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);

			// Check if temporary asset need to be updated
			if (_tmpAssetIsDirty)
			{
				// Clear flag
				_tmpAssetIsDirty = false;

				// Update
				RefreshTempAsset();
			}

			// Check if need to load surface
			if (_isWaitingForSurfaceLoad && _asset.IsLoaded)
			{
				// Clear flag
				_isWaitingForSurfaceLoad = false;

				// Init properties and parameters proxy
				_properties.OnLoad(this);

				// Load surface data from the asset
				byte[] data = _asset.LoadSurface();
				if (data == null)
				{
					// Error
					Debug.LogError("Failed to load animation graph surface data.");
					Close();
					return;
				}

				// Load surface graph
				if (_surface.Load(data))
				{
					// Error
					Debug.LogError("Failed to load animation graph surface.");
					Close();
					return;
				}

				// Setup
				_surface.Enabled = true;
				ClearEditedFlag();
			}
		}

		/// <inheritdoc />
		public override bool UseLayoutData => true;

		/// <inheritdoc />
		public override void OnLayoutSerialize(XmlWriter writer)
		{
			writer.WriteAttributeString("Split1", _split1.SplitterValue.ToString());
			writer.WriteAttributeString("Split2", _split2.SplitterValue.ToString());
		}

		/// <inheritdoc />
		public override void OnLayoutDeserialize(XmlElement node)
		{
			float value1;

			if (float.TryParse(node.GetAttribute("Split1"), out value1))
				_split1.SplitterValue = value1;
			if (float.TryParse(node.GetAttribute("Split2"), out value1))
				_split2.SplitterValue = value1;
		}

		/// <inheritdoc />
		public override void OnLayoutDeserialize()
		{
			_split1.SplitterValue = 0.7f;
			_split2.SplitterValue = 0.4f;
		}
	}
}