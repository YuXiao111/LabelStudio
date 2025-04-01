using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LabelStudio.Controls.DrawControlLibrary;
using LabelStudio.Controls.SingleControlLibrary;

namespace LabelStudio.Controls.DrawSerializeControlLibray
{
    public sealed class DrawLineSerializer : DrawGeometrySerializerBase
    {
        public override DrawGeometryBase Deserialize(DrawingCanvas drawingCanvas)
        {
            var draw = new LineDrawTool(drawingCanvas);
            draw.DeserializeFrom(this);
            return draw;
        }

        #region 属性

        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        #endregion
    }
}
