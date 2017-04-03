// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using IxMilia.Pdf.Extensions;

namespace IxMilia.Pdf
{
    public abstract class PdfObject
    {
        internal int Id { get; set; }
        
        public PdfObject Parent { get; internal set; }

        protected abstract byte[] GetContent();

        public virtual IEnumerable<PdfObject> GetChildren()
        {
            yield break;
        }

        internal void WriteTo(Stream stream)
        {
            stream.WriteLine($"{Id} 0 obj");
            stream.WriteBytes(GetContent());
            stream.WriteLine("endobj");
        }
    }
}
