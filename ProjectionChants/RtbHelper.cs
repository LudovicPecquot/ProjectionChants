using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.IO;

namespace ProjectionChants
{
    static class RtbHelper
    {
        static public String saveRtb(RichTextBox rtb)
        {
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            MemoryStream ms = new MemoryStream();
            tr.Save(ms, DataFormats.Rtf);
            byte[] b = new byte[ms.Length];
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(b, 0, b.Length);
            string s = Encoding.ASCII.GetString(b);
            return s;
        }

        static public void loadRtb(RichTextBox rtb, String s)
        {
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            if (s == "")
            {
                tr.Text = "";
                return;
            }
            MemoryStream ms = new MemoryStream();
            byte[] b = new byte[s.Length];
            b = Encoding.ASCII.GetBytes(s);
            ms.Write(b, 0, b.Length);
            tr.Load(ms, DataFormats.Rtf);
        }


    }
}
