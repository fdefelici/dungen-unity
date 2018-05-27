using System;
using System.Collections;
using DungeonGeneration.Generator.Domain;
using System.Collections.Generic;

namespace DungeonGeneration.Generator.Pickers {
    public class CellInRangePicker {
        private IPickerStrategy _strategy;
        private Cell _min;
        private Cell _max;

        public CellInRangePicker(IPickerStrategy aStrategy) {
            _strategy = aStrategy;
        }

        public Cell drawBetween(Cell min, Cell max) {
            return drawBetweenWithExclusion(min, max);
        }

        public Cell drawBetweenWithExclusion(Cell min, Cell max, params Cell[] excluded) {
            if (min.hasNegativeIndexes() && max.hasNegativeIndexes()) return min;
            if (min.hasNegativeIndexes()) min = min.toNearestPositiveCell();
            if (max.hasNegativeIndexes()) max = max.toNearestPositiveCell();

            HashSet<Cell> uniqueExclusion = new HashSet<Cell>();
            foreach(Cell each in excluded) {
                Cell toAdd = each;
                if (each.hasNegativeIndexes()) toAdd = toAdd.toNearestPositiveCell();
                uniqueExclusion.Add(toAdd);
            }

            List<Cell> cleanedExclusions = new List<Cell>(uniqueExclusion);
            //cleanedExclusions.Remove(min);
            //cleanedExclusions.Remove(max);

            int distance = min.distance(max) - cleanedExclusions.Count;
            if (distance <= 0) return min;

            int selectedCellPosition = _strategy.drawBetween(0, distance-1);

            Cell[] cells = min.cells(max);
            List<Cell> asList = new List<Cell>(cells);
            foreach(Cell each in cleanedExclusions) {
                asList.Remove(each);
            }
            return asList[selectedCellPosition];
        }
    }
}