// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;

namespace IxMilia.Pdf
{
    public enum PdfFontType1Type
    {
        TimesRoman,
        Helvetica,
        Courier,
        Symbol,
        TimesBold,
        HelveticaBold,
        CourierBold,
        ZapfDingbats,
        TimesItalic,
        HelvaticaOblique,
        CourierOblique,
        TimesBoldItalic,
        HelveticaBoldOblique,
        CourierBoldOblique
    }

    internal static class PdfFontType1TypeExtensions
    {
        public static string ToBaseFont(this PdfFontType1Type fontType)
        {
            switch (fontType)
            {
                case PdfFontType1Type.TimesRoman:
                    return "Times-Roman";
                case PdfFontType1Type.Helvetica:
                    return "Helvetica";
                case PdfFontType1Type.Courier:
                    return "Courier";
                case PdfFontType1Type.Symbol:
                    return "Symbol";
                case PdfFontType1Type.TimesBold:
                    return "Times-Bold";
                case PdfFontType1Type.HelveticaBold:
                    return "Helvetica-Bold";
                case PdfFontType1Type.CourierBold:
                    return "Courier-Bold";
                case PdfFontType1Type.ZapfDingbats:
                    return "ZapfDingbats";
                case PdfFontType1Type.TimesItalic:
                    return "Times-Italic";
                case PdfFontType1Type.HelvaticaOblique:
                    return "Helvatica-Oblique";
                case PdfFontType1Type.CourierOblique:
                    return "Courier-Oblique";
                case PdfFontType1Type.TimesBoldItalic:
                    return "Times-BoldItalic";
                case PdfFontType1Type.HelveticaBoldOblique:
                    return "Helvetica-BoldOblique";
                case PdfFontType1Type.CourierBoldOblique:
                    return "Courier-BoldOblique";
                default:
                    throw new ArgumentException(nameof(fontType));
            }
        }
    }
}