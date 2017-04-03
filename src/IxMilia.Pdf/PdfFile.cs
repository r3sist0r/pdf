﻿// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using IxMilia.Pdf.Extensions;

namespace IxMilia.Pdf
{
    public class PdfFile
    {
        private PdfCatalog _catalog = new PdfCatalog();
        private List<int> _offsets = new List<int>();

        public IList<PdfPage> Pages => _catalog.Pages.Pages;

        public void Save(Stream stream)
        {
            _offsets.Clear();
            var writtenObjects = new HashSet<PdfObject>();
            AssignIds();
            BeforeWrite(_catalog);
            stream.WriteLine("%PDF-1.6");
            WriteObject(_catalog, stream, writtenObjects);

            var xrefCount = _offsets.Count + 1; // to account for the required zero-id object
            var xrefLoc = stream.Position;
            stream.WriteLine("xref");
            stream.WriteLine($"0 {xrefCount}");
            stream.WriteLine($"0000000000 {ushort.MaxValue} f"); // said required zero-id free object
            for (var i = 0; i < _offsets.Count; i++)
            {
                stream.WriteLine($"{_offsets[i].ToString().PadLeft(10, '0')} {(0).ToString().PadLeft(5, '0')} n");
            }

            stream.WriteLine($"trailer <</Size {xrefCount} /Root {_catalog.Id.AsObjectReference()}>>");
            stream.WriteLine("startxref");
            stream.Write(xrefLoc.ToString());
            stream.WriteLine("");
            stream.Write("%%EOF");
        }

        private void WriteObject(PdfObject obj, Stream stream, HashSet<PdfObject> writtenObjects)
        {
            if (writtenObjects.Add(obj))
            {
                _offsets.Add((int)stream.Position);
                obj.WriteTo(stream);
                foreach (var child in obj.GetChildren())
                {
                    child.Parent = obj;
                    WriteObject(child, stream, writtenObjects);
                }
            }
        }

        private void BeforeWrite(PdfObject obj)
        {
            obj.BeforeWrite();
            foreach (var child in obj.GetChildren())
            {
                BeforeWrite(child);
            }
        }

        private void AssignIds()
        {
            var id = 1;
            AssignIds(_catalog, ref id);
        }

        private static void AssignIds(PdfObject obj, ref int id)
        {
            obj.Id = id++;
            foreach (var child in obj.GetChildren())
            {
                AssignIds(child, ref id);
            }
        }
    }
}