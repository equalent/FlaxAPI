////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2017 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FlaxEngine;
// ReSharper disable InconsistentNaming

namespace FlaxEditor.Content.Import
{
    /// <summary>
    /// Texture format types.
    /// </summary>
    public enum TextureFormatType : byte
    {
        /// <summary>
        /// The unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The color with RGB channels.
        /// </summary>
        ColorRGB = 1,

        /// <summary>
        /// The color with RGBA channels.
        /// </summary>
        ColorRGBA = 2,

        /// <summary>
        /// The normal map (packed and compressed).
        /// </summary>
        NormalMap = 3,

        /// <summary>
        /// The gray scale (R channel).
        /// </summary>
        GrayScale = 4,

        /// <summary>
        /// The HDR color (RGBA channels).
        /// </summary>
        HdrRGBA = 5,

        /// <summary>
        /// The HDR color (RGB channels).
        /// </summary>
        HdrRGB = 6
    }

    /// <summary>
    /// Proxy object to present texture import settings in <see cref="ImportFilesDialog"/>.
    /// </summary>
    public class TextureImportSettings
    {
        /// <summary>
        /// A custom version of <see cref="TextureFormatType"/> for GUI.
        /// </summary>
        public enum CustomTextureFormatType
        {
            /// <summary>
            /// The color with RGB channels.
            /// </summary>
            ColorRGB = 1,

            /// <summary>
            /// The color with RGBA channels.
            /// </summary>
            ColorRGBA = 2,

            /// <summary>
            /// The normal map (packed and compressed).
            /// </summary>
            NormalMap = 3,

            /// <summary>
            /// The gray scale (R channel).
            /// </summary>
            GrayScale = 4,

            /// <summary>
            /// The HDR color (RGBA channels).
            /// </summary>
            HdrRGBA = 5,

            /// <summary>
            /// The HDR color (RGB channels).
            /// </summary>
            HdrRGB = 6
        }

        /// <summary>
        /// A custom set of max texture import sizes.
        /// </summary>
        public enum CustomMaxSizeType
        {
            /// <summary>
            /// The 32.
            /// </summary>
            _32 = 32,

            /// <summary>
            /// The 64.
            /// </summary>
            _64 = 64,

            /// <summary>
            /// The 128.
            /// </summary>
            _128 = 128,

            /// <summary>
            /// The 256.
            /// </summary>
            _256 = 256,

            /// <summary>
            /// The 512.
            /// </summary>
            _512 = 512,

            /// <summary>
            /// The 1024.
            /// </summary>
            _1024 = 1024,

            /// <summary>
            /// The 2048.
            /// </summary>
            _2048 = 2048,

            /// <summary>
            /// The 4096.
            /// </summary>
            _4096 = 4096,

            /// <summary>
            /// The 8192.
            /// </summary>
            _8192 = 8192
        }

        /// <summary>
        /// Converts the maximum size to enum.
        /// </summary>
        /// <param name="f">The max size.</param>
        /// <returns>The converted enum.</returns>
        public static CustomMaxSizeType ConvertMaxSize(int f)
        {
            if (!Mathf.IsPowerOfTwo(f))
                f = Mathf.NextPowerOfTwo(f);

            FieldInfo[] fields = typeof(CustomMaxSizeType).GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                if (field.Name.Equals("value__"))
                    continue;

                if (f == (int)field.GetRawConstantValue())
                    return (CustomMaxSizeType)f;
            }

            return CustomMaxSizeType._8192;
        }

        /// <summary>
        /// The sprite info.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SpriteInfo
        {
            /// <summary>
            /// The sprite area.
            /// </summary>
            public Rectangle Area;

            /// <summary>
            /// The sprite name.
            /// </summary>
            public string Name;

            /// <summary>
            /// Initializes a new instance of the <see cref="SpriteInfo"/> struct.
            /// </summary>
            /// <param name="area">The area.</param>
            /// <param name="name">The name.</param>
            public SpriteInfo(Rectangle area, string name)
            {
                Area = area;
                Name = name;
            }
        }

        /// <summary>
        /// Texture format type
        /// </summary>
        [EditorOrder(0), Tooltip("Texture import format type")]
        public CustomTextureFormatType Type { get; set; } = CustomTextureFormatType.ColorRGB;

        /// <summary>
        /// True if texture should be improted as a texture atlas resource
        /// </summary>
        [EditorOrder(10), Tooltip("True if texture should be improted as a texture atlas (with sprites)")]
        public bool IsAtlas { get; set; }

        /// <summary>
        /// True if disable dynamic texture streaming
        /// </summary>
        [EditorOrder(20), Tooltip("True if disable dynamic texture streaming")]
        public bool NeverStream { get; set; }

        /// <summary>
        /// Enables/disables texture data compression.
        /// </summary>
        [EditorOrder(30), Tooltip("True if comrpess texture data")]
        public bool Compress { get; set; } = true;

        /// <summary>
        /// True if texture channels have independent data
        /// </summary>
        [EditorOrder(40), Tooltip("True if texture channels have independent data (for compression methods)")]
        public bool IndependentChannels { get; set; }

        /// <summary>
        /// True if texture contains sRGB format and engine should keep that data format
        /// </summary>
        [EditorOrder(50), Tooltip("True if texture contains colors in sRGB format data")]
        public bool IsSRGB { get; set; }

        /// <summary>
        /// True if generate mip maps chain for the texture.
        /// </summary>
        [EditorOrder(60), Tooltip("True if generate mip maps chain for the texture")]
        public bool GenerateMipMaps { get; set; } = true;

        /// <summary>
        /// The import texture scale.
        /// </summary>
        [EditorOrder(70), Tooltip("Texture scale. Default is 1.")]
        public float Scale { get; set; } = 1.0f;

        /// <summary>
        /// Maximum size of the texture (for both width and height).
        /// Higher resolution textures will be resized during importing process.
        /// </summary>
        [EditorOrder(80), Tooltip("Maximum texture size (will be resized if need to)")]
        public CustomMaxSizeType MaxSize { get; set; } = CustomMaxSizeType._8192;

        /// <summary>
        /// The sprites. Used to keep created sprites on sprite atlas reimport.
        /// </summary>
        [HideInEditor]
        public List<SpriteInfo> Sprites = new List<SpriteInfo>();
    }

    /// <summary>
    /// Texture asset import entry.
    /// </summary>
    /// <seealso cref="FlaxEditor.Content.Import.AssetFileEntry" />
    public class TextureFileEntry : AssetFileEntry
    {
        private TextureImportSettings _settings = new TextureImportSettings();

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureFileEntry"/> class.
        /// </summary>
        /// <param name="url">The source file url.</param>
        /// <param name="resultUrl">The result file url.</param>
        public TextureFileEntry(string url, string resultUrl)
            : base(url, resultUrl)
        {
            // Try to restore target asset texture import options (usefull for fast reimport)
            InternalOptions options;
            if (Internal_GetTextureImportOptions(resultUrl, out options))
            {
                // Restore settings
                _settings.Type = (TextureImportSettings.CustomTextureFormatType)options.Type;
                _settings.IsAtlas = options.IsAtlas;
                _settings.NeverStream = options.NeverStream;
                _settings.Compress = options.Compress;
                _settings.IndependentChannels = options.IndependentChannels;
                _settings.IsSRGB = options.IsSRGB;
                _settings.GenerateMipMaps = options.GenerateMipMaps;
                _settings.Scale = options.Scale;
                _settings.MaxSize = TextureImportSettings.ConvertMaxSize(options.MaxSize);
                if (options.SpriteAreas != null)
                {
                    int spritesCount = options.SpriteAreas.Length;
                    _settings.Sprites.Capacity = spritesCount;
                    for (int i = 0; i < spritesCount; i++)
                    {
                        _settings.Sprites.Add(new TextureImportSettings.SpriteInfo(options.SpriteAreas[i], options.SpriteNames[i]));
                    }
                }
            }

            // Try to guess format type based on file name
            TextureFormatType formatToUse = TextureFormatType.ColorRGB;
            var shortName = System.IO.Path.GetFileNameWithoutExtension(url);
            string snl = shortName.ToLower();
            if (_settings.Type != TextureImportSettings.CustomTextureFormatType.ColorRGB)
            {
                // Skip checking
            }
            else if (snl.EndsWith("_n")
                     || snl.Contains("nrm")
                     || snl.Contains("nm")
                     || snl.Contains("normal")
                     || snl.EndsWith("normals"))
            {
                // Normal map
                formatToUse = TextureFormatType.NormalMap;
            }
            else if (snl.EndsWith("_d")
                     || snl.Contains("diffuse")
                     || snl.Contains("diff")
                     || snl.Contains("color")
                     || snl.Contains("basecolor")
                     || snl.Contains("albedo"))
            {
                // Albedo or diffuse map
                formatToUse = TextureFormatType.ColorRGB;
            }
            else if (snl.EndsWith("ao")
                     || snl.EndsWith("ambientocclusion")
                     || snl.EndsWith("gloss")
                     || snl.EndsWith("_r")
                     || snl.EndsWith("_displ")
                     || snl.EndsWith("roughness")
                     || snl.EndsWith("metalness")
                     || snl.EndsWith("displacement")
                     || snl.EndsWith("spec")
                     || snl.EndsWith("specular")
                     || snl.EndsWith("occlusion")
                     || snl.EndsWith("height")
                     || snl.EndsWith("heights")
                     || snl.EndsWith("cavity")
                     || snl.EndsWith("metalic")
                     || snl.EndsWith("metallic"))
            {
                // Glossiness, metalness, ambient occlusion, displacement, height, cavity or specular
                formatToUse = TextureFormatType.GrayScale;
            }
            _settings.Type = (TextureImportSettings.CustomTextureFormatType)formatToUse;
        }

        /// <inheritdoc />
        public override bool HasSettings => true;

        /// <inheritdoc />
        public override object Settings => _settings;

        [StructLayout(LayoutKind.Sequential)]
        internal struct InternalOptions
        {
            public TextureFormatType Type;
            public bool IsAtlas;
            public bool NeverStream;
            public bool Compress;
            public bool IndependentChannels;
            public bool IsSRGB;
            public bool GenerateMipMaps;
            public float Scale;
            public int MaxSize;
            public Rectangle[] SpriteAreas;
            public string[] SpriteNames;
        }

        #region Internal Calls

#if !UNIT_TEST_COMPILANT
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern bool Internal_GetTextureImportOptions(string path, out InternalOptions result);
#endif

        #endregion
    }
}
