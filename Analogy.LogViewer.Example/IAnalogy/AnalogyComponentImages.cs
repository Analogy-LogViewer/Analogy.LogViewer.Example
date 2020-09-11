using System;
using System.Collections.Generic;
using System.Drawing;
using Analogy.Interfaces;
using Analogy.LogViewer.Example.Properties;

namespace Analogy.LogViewer.Example
{
    public class AnalogyComponentImages : IAnalogyComponentImages
    {
        public Image GetLargeImage(Guid analogyComponentId)
        {
            if (analogyComponentId == PrimaryFactory.Id)
                return Resources.Analogy_image_32x32;
            return null;
        }

        public Image GetSmallImage(Guid analogyComponentId)
        {
            if (analogyComponentId == PrimaryFactory.Id)
                return Resources.Analogy_image_16x16;
            return null;
        }

        public Image GetOnlineConnectedLargeImage(Guid analogyComponentId) => null;

        public Image GetOnlineConnectedSmallImage(Guid analogyComponentId) => null;

        public Image GetOnlineDisconnectedLargeImage(Guid analogyComponentId) => null;

        public Image GetOnlineDisconnectedSmallImage(Guid analogyComponentId) => null;
    }
}

