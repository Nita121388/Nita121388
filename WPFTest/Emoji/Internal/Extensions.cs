using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPFTest.Emoji.Internal
{
    internal static class Extensions
    {
        /// <summary>
        /// Advance a TextPointer to the nth character
        /// </summary>
        public static TextPointer GetPositionAtCharOffset(this TextPointer p, int offset)
        {
            var fallback = offset > 0 ? p.DocumentEnd : p.DocumentStart;
            while (offset != 0 && p != null)
            {
                var dir = offset > 0 ? LogicalDirection.Forward : LogicalDirection.Backward;
                if (p.GetPointerContext(dir) == TextPointerContext.Text)
                {
                    var text = p.GetTextInRun(dir);
                    if (text.Length >= Math.Abs(offset))
                        return p.GetPositionAtOffset(offset);
                    offset -= Math.Sign(offset) * text.Length;
                }
                p = p.GetNextContextPosition(dir);
            }
            return p ?? fallback;
        }
    }
}
