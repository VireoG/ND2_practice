﻿using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Knots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson2_Homework.Services
{
    public class ReplacementPistons : IServices
    {
        public decimal PricePerPistol { get; set; }
        public decimal CostOfService { get; set; }

        public decimal Cost(List<IKnots> knots)
        {
            var selected = from knot in knots
                           where knot.Type == "Engine"
                           select knot;

            Engine engine = (Engine)selected.ElementAt(0);

            if (engine == null)
                throw new Exception("Insufficient data");

            if (engine.EngineType != "Electrical" || engine.EngineType != "Rotor")
                CostOfService = engine.ValueOfPistols * PricePerPistol;

            return CostOfService;
        }      
    }
}

