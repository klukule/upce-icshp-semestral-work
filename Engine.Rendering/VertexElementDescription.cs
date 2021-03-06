﻿using System;

namespace Engine.Rendering
{
    /// <summary>
    /// Describes a single element of a vertex.
    /// </summary>
    public struct VertexElementDescription : IEquatable<VertexElementDescription>
    {
        /// <summary>
        /// The name of the element.
        /// </summary>
        public string Name;

        /// <summary>
        /// The semantic type of the element.
        /// </summary>
        public VertexElementSemantic Semantic;

        /// <summary>
        /// The format of the element.
        /// </summary>
        public VertexElementFormat Format;

        /// <summary>
        /// Constructs a new VertexElementDescription describing a per-vertex element.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="semantic">The semantic type of the element.</param>
        /// <param name="format">The format of the element.</param>
        public VertexElementDescription(string name, VertexElementSemantic semantic, VertexElementFormat format)
            : this(name, format, semantic)
        {
        }

        /// <summary>
        /// Constructs a new VertexElementDescription.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="semantic">The semantic type of the element.</param>
        /// <param name="format">The format of the element.</param>
        public VertexElementDescription(
            string name,
            VertexElementFormat format,
            VertexElementSemantic semantic)
        {
            Name = name;
            Format = format;
            Semantic = semantic;
        }

        /// <summary>
        /// Element-wise equality.
        /// </summary>
        /// <param name="other">The instance to compare to.</param>
        /// <returns>True if all elements are equal; false otherswise.</returns>
        public bool Equals(VertexElementDescription other)
        {
            return Name.Equals(other.Name)
                && Format == other.Format
                && Semantic == other.Semantic;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return HashHelper.Combine(
                Name.GetHashCode(),
                Format.GetHashCode(),
                Semantic.GetHashCode());
        }
    }
}