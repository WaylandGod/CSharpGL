﻿using System;

namespace CSharpGL
{
    /// <summary>
    /// 将VBO上传到GPU后，就得到VBO的指针。CPU内存中的VBO数据就可以释放掉了。
    /// VBO's pointer got from Buffer's GetBufferPtr() method.
    /// </summary>
    public abstract partial class BufferPtr : IDisposable
    {
        /// <summary>
        /// 用glGenBuffers()得到的VBO的Id。
        /// <para>Id got from glGenBuffers();</para>
        /// </summary>
        public uint BufferId { get; private set; }

        /// <summary>
        /// 此VBO含有多个个元素？
        /// <para>How many elements?</para>
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// 此VBO中的数据在内存中占用多少个字节？
        /// <para>How many bytes in this buffer?</para>
        /// </summary>
        public int ByteLength { get; private set; }

        /// <summary>
        /// Target that this buffer should bind to.
        /// </summary>
        public BufferTarget Target { get; private set; }

        /// <summary>
        ///
        /// </summary>
        internal static OpenGL.glBindBuffer glBindBuffer;

        /// <summary>
        ///
        /// </summary>
        internal static OpenGL.glDeleteBuffers glDeleteBuffers;

        /// <summary>
        ///
        /// </summary>
        internal static OpenGL.glMapBuffer glMapBuffer;

        /// <summary>
        ///
        /// </summary>
        internal static OpenGL.glUnmapBuffer glUnmapBuffer;

        /// <summary>
        ///
        /// </summary>
        internal static OpenGL.glMapBufferRange glMapBufferRange;

        /// <summary>
        /// 将VBO上传到GPU后，就得到VBO的指针。CPU内存中的VBO数据就可以释放掉了。
        /// VBO's pointer got from Buffer's GetBufferPtr() method. It's totally OK to free memory of unmanaged array stored in this buffer object now.
        /// </summary>
        /// <param name="target">Target that this buffer should bind to.</param>
        /// <param name="bufferId">用glGenBuffers()得到的VBO的Id。<para>Id got from glGenBuffers();</para></param>
        /// <param name="length">此VBO含有多个个元素？<para>How many elements?</para></param>
        /// <param name="byteLength">此VBO中的数据在内存中占用多少个字节？<para>How many bytes in this buffer?</para></param>
        internal BufferPtr(BufferTarget target, uint bufferId, int length, int byteLength)
        {
            if (glBindBuffer == null)
            {
                glBindBuffer = OpenGL.GetDelegateFor<OpenGL.glBindBuffer>();
                glDeleteBuffers = OpenGL.GetDelegateFor<OpenGL.glDeleteBuffers>();
                glMapBuffer = OpenGL.GetDelegateFor<OpenGL.glMapBuffer>();
                glMapBufferRange = OpenGL.GetDelegateFor<OpenGL.glMapBufferRange>();
                glUnmapBuffer = OpenGL.GetDelegateFor<OpenGL.glUnmapBuffer>();
            }

            this.Target = target;
            this.BufferId = bufferId;
            this.Length = length;
            this.ByteLength = byteLength;
        }

        /// <summary>
        ///Bind this buffer.
        /// </summary>
        public virtual void Bind()
        {
            glBindBuffer((uint)this.Target, this.BufferId);
        }

        /// <summary>
        /// Unind this buffer.
        /// </summary>
        public virtual void Unbind()
        {
            glBindBuffer((uint)this.Target, 0);
        }
    }
}