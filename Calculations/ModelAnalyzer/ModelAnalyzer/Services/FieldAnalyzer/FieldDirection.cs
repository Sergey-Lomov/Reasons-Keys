﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldDirection
    {
        internal int x, y, z;

        static FieldDirection top = new FieldDirection(0, 1, -1);
        static FieldDirection topRight = new FieldDirection(1, 0, -1);
        static FieldDirection topLeft = new FieldDirection(-1, 1, 0);
        static FieldDirection bottom = new FieldDirection(0, -1, 1);
        static FieldDirection bottomRight = new FieldDirection(1, -1, 0);
        static FieldDirection bottomLeft = new FieldDirection(-1, 0, 1);

        static private List<FieldDirection> availableDirections = new List<FieldDirection> {
            bottom, bottomLeft, topLeft, top, topRight, bottomRight };

        private FieldDirection(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        static public FieldDirection FromEventRelationPosition(int eventRelationPosition)
        {
            return availableDirections[eventRelationPosition % availableDirections.Count];
        }
    }
}