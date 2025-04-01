using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabelStudio.Controls.DrawControlLibrary;
using LabelStudio.Controls.SingleControlLibrary;

namespace LabelStudio.Controls.DrawSerializeControlLibray
{
    public sealed class DrawPolygonSerializer : DrawGeometrySerializerBase
    {
        public override DrawGeometryBase Deserialize(DrawingCanvas drawingCanvas)
        {
            var draw = new PolygonDrawTool(drawingCanvas);
            draw.DeserializeFrom(this);
            return draw;
        }

        #region 属性



        #endregion
    }
}
